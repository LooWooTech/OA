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
        public List<int> GetYears()
        {
            return DB.Salaries.GroupBy(e => e.Year).Select(g => g.Key).ToList();
        }

        public IEnumerable<Salary> GetList(SalaryParameter parameter)
        {
            var query = DB.Salaries.AsQueryable();
            if (parameter.Year > 0)
            {
                query = query.Where(e => e.Year == parameter.Year);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Title.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public IEnumerable<SalaryData> GetSalaryDatas(SalaryParameter parameter)
        {
            var query = DB.SalaryDatas.AsQueryable();
            if (parameter.Year > 0)
            {
                query = query.Where(e => e.Salary.Year == parameter.Year);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Salary.Title.Contains(parameter.SearchKey));
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.SalaryId > 0)
            {
                query = query.Where(e => e.SalaryId == parameter.SalaryId);
            }
            return query.OrderBy(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(Salary model)
        {
            var entity = DB.Salaries.FirstOrDefault(e => e.Year == model.Year && (e.FilePath == model.FilePath || e.Title == model.Title));
            if (entity != null)
            {
                entity.Title = model.Title;
                entity.FilePath = model.FilePath;
                model.ID = entity.ID;
            }
            else
            {
                DB.Salaries.Add(model);
            }
            DB.SaveChanges();
        }

        public void SaveData(SalaryData model)
        {
            var entity = DB.SalaryDatas.FirstOrDefault(e => e.UserId == model.UserId && e.SalaryId == model.SalaryId);
            if (entity != null)
            {
                DB.SalaryDatas.Remove(entity);
            }
            DB.SalaryDatas.AddOrUpdate(model);
            DB.SaveChanges();
        }

        private static readonly string[] _columns = AppSettings.Get("SalaryHeaders")?.Split('|');

        private int FindHeader(ISheet sheet, int rowIndex = 0)
        {
            do
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null)
                {
                    if (rowIndex < sheet.LastRowNum)
                    {
                        rowIndex++;
                        continue;
                    }
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

        public void Delete(Salary model)
        {
            var datas = DB.SalaryDatas.Where(e => e.SalaryId == model.ID);
            DB.SalaryDatas.RemoveRange(datas);
            DB.Salaries.Remove(model);
            DB.SaveChanges();
        }

        public Salary GetModel(int id)
        {
            return DB.Salaries.FirstOrDefault(e => e.ID == id);
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

                if (col.Name == "序号" && (cell.Value == null || cell.Value.ToString() == "合计"))
                {
                    return null;
                }
                data[col.Name] = cell.Value;
            }
            return data;
        }

        public List<int> ImportData(Salary salary)
        {
            var excel = ExcelHelper.GetWorkbook(salary.FilePath);
            var sheetIndex = excel.NumberOfSheets - 1;
            var sheet = excel.GetSheetAt(sheetIndex);

            var currentRowIndex = 0;
            var failList = new List<int>();
            do
            {
                var headerRow = FindHeader(sheet, currentRowIndex);
                if (headerRow == -1)
                {
                    sheetIndex--;
                    if (sheetIndex < 0) break;
                    sheet = excel.GetSheetAt(sheetIndex);
                    continue;
                }
                var header = BuildHeader(sheet, headerRow);
                if (header == null)
                {
                    break;
                }

                for (var rowIndex = header.StartRow + header.RowHeight; rowIndex < 500; rowIndex++)
                {
                    currentRowIndex = rowIndex;
                    var rowData = ReadData(sheet, header, rowIndex);
                    if (rowData == null)
                    {
                        currentRowIndex++;
                        break;
                    }
                    var data = new SalaryData { SalaryId = salary.ID, Json = rowData.ToJson() };
                    var userRealName = rowData["姓名"]?.ToString();
                    if (!string.IsNullOrEmpty(userRealName))
                    {
                        var user = DB.Users.FirstOrDefault(e => e.Username == userRealName);
                        if (user != null)
                        {
                            data.UserId = user.ID;
                        }
                        data.UserName = userRealName;
                    }
                    if (data.UserId == 0)
                    {
                        failList.Add(rowIndex + 1);
                    }
                    SaveData(data);
                }

            } while (true);
            return failList;
        }
    }
}
