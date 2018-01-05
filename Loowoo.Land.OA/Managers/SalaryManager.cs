using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class SalaryManager : ManagerBase
    {
        public int[] GetYears(int userId)
        {
            return DB.Salaries.Where(e => e.UserId == userId).GroupBy(e => e.Year).Select(g => g.Key).ToArray();
        }

        public IEnumerable<Salary> GetList(SalaryParameter parameter)
        {
            var query = DB.Salaries.AsQueryable();
            if (parameter.Year > 0)
            {
                query = query.Where(e => e.Year == parameter.Year);
            }
            if (parameter.Month > 0)
            {
                query = query.Where(e => e.Month == parameter.Month);
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(Salary model)
        {
            //TODO：如果姓名不存在，则创建一个新用户？
            if (model.UserId == 0) return;

            var entity = DB.Salaries.FirstOrDefault(e => e.Year == model.Year && e.Month == model.Month && e.UserId == model.UserId);
            if (entity == null)
            {
                model.Json = model.Data.ToJson();
                DB.Salaries.Add(model);
            }
            else
            {
                entity.Json = model.Data.ToJson();
            }
            DB.SaveChanges();
        }

        private static readonly string[] _columns = new[] { "部门,员工类型", "序号,帐号,工号,姓名", "序号,参保时间,姓名" };

        private int FindHeader(ISheet sheet, int rowIndex = 0)
        {
            do
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null)
                {
                    break;
                }
                var headTitles = new List<string>();
                foreach (var cell in row)
                {
                    if (cell.CellType != CellType.String)
                    {
                        break;
                    }
                    headTitles.Add(cell.StringCellValue);
                    var titles = string.Join(",", headTitles);
                    //如果符合
                    if (_columns.Any(c => c == titles))
                    {
                        return rowIndex;
                    }
                }
                rowIndex++;
            } while (true);
            return -1;
        }

        private SalaryHeader BuildHeader(ISheet sheet, int rowIndex)
        {
            var result = new SalaryHeader { StartRow = rowIndex };
            var row = sheet.GetRow(rowIndex);
            if (row == null) return null;
            if (row.Cells.All(e => e.CellType == CellType.Blank) || row.Cells.All(e => e.CellType != CellType.String))
            {
                return null;
            }
            var columns = sheet.ReadRowData(rowIndex).AsEnumerable();
            var firstColumn = columns.FirstOrDefault(e => e.Rowspan > 1);
            if (firstColumn != null)
            {
                for (var i = 1; i < firstColumn.Rowspan; i++)
                {
                    columns = columns.Concat(sheet.ReadRowData(rowIndex + i));
                }
            }

            result.Columns = columns.Where(e => e.Colspan == 1 && e.Value != null).Select(e => new SalaryColumn
            {
                Column = e.Column,
                Row = e.Row,
                ColumnSpan = e.Colspan,
                RowSpan = e.Rowspan,
                Name = e.Value.ToString()
            }).ToList();
            return result;
        }

        private Document ReadData(ISheet sheet, SalaryHeader header, int rowIndex)
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null) return null;

            if (row.Cells.All(e => e.CellType == CellType.Blank))
            {
                return null;
            }
            if (row.Cells.Any(e => e.IsMergedCell)) return null;

            var values = sheet.ReadRowData(rowIndex);
            if (values == null) return null;

            Document data = new Document();
            foreach (var col in header.Columns)
            {
                if (col.ColumnSpan > 1)
                {
                    continue;
                }
                var cell = values.FirstOrDefault(e => e.Column == col.Column);
                if (cell == null) continue;

                if (col.Name == "序号" && cell.Value.ToString() == "合计")
                {
                    return null;
                }
                data[col.Name] = cell.Value;
            }
            return data;
        }

        public List<int> ImportData(int year, int month, string filePath, SalaryDataType type = 0)
        {
            if (string.IsNullOrEmpty(filePath)) return null;

            var excel = ExcelHelper.GetWorkbook(filePath);
            var sheet = excel.GetSheetAt(0);

            var currentRowIndex = 0;
            var failList = new List<int>();
            do
            {
                var headerRow = FindHeader(sheet, currentRowIndex);
                if (headerRow == -1)
                {
                    break;
                }
                var header = BuildHeader(sheet, headerRow);
                if (header == null)
                {
                    break;
                }

                for (var rowIndex = header.StartRow + header.RowHeight; rowIndex < 250; rowIndex++)
                {
                    currentRowIndex = rowIndex;
                    var rowData = ReadData(sheet, header, rowIndex);
                    if (rowData == null)
                    {
                        currentRowIndex++;
                        break;
                    }
                    var model = new Salary { Month = month, Year = year,Json = rowData.ToJson() };
                    var userRealName = rowData["姓名"]?.ToString();
                    if (!string.IsNullOrEmpty(userRealName))
                    {
                        var user = DB.Users.FirstOrDefault(e => e.Username == userRealName);
                        if (user != null)
                        {
                            model.UserId = user.ID;
                        }
                    }
                    if (model.UserId == 0)
                    {
                        failList.Add(rowIndex + 1);
                    }
                    Save(model);
                }

            } while (true);
            return failList;
        }
    }
}
