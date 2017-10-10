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
        private static readonly string[] ZsgColumns = "部门,员工类型,员工号,姓名,身份证号,信用卡号,岗位工资,薪级工资,生活补贴,岗位津贴,工龄补贴,提租,公积金(补),住房补贴,医疗补贴,应发工资,公积金,住房贴,医疗保险,失业金,养老金,预扣养老金,实发工资,计税工资,所得税,实际发放数".Split(',').ToArray();
        private static readonly string[] LsgColumns = "序号,帐号,工号,姓名,工资,岗位津贴,工龄工资,加班,补贴,冷饮费,车贴,应发工资,养老保险,救助金5元/月,失业保险0.5%,补缴公积金,扣除,公积金,扣款合计,应发,个调税,实发工资".Split(',').ToArray();
        private static readonly string[] BdcColumns = "序号,姓名,公积金,补缴个人部分,个人扣款合计,养老,失业保险,医疗,工伤,生育,公积金,补缴单位部分,单位扣款合计,合计,扣税工资,扣税,扣除差额,补上月多扣部分,实发数".Split(',').ToArray();

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

        private List<SalaryColumn> GetColumns(string[] names)
        {
            var list = new List<SalaryColumn>();
            for (var i = 0; i < names.Length; i++)
            {
                list.Add(new SalaryColumn(i, names[i]));
            }
            return list;
        }

        private SalaryDataDescriptor GetSalaryDataDescriptor(SalaryDataType type)
        {
            switch (type)
            {
                case SalaryDataType.BDC:
                    return new SalaryDataDescriptor
                    {
                        Type = type,
                        StartRow = 4,
                        Columns = GetColumns(BdcColumns)
                    };
                case SalaryDataType.LSG:
                    return new SalaryDataDescriptor
                    {
                        Type = type,
                        StartRow = 3,
                        Columns = GetColumns(LsgColumns)
                    };
                case SalaryDataType.ZSG:
                    return new SalaryDataDescriptor
                    {
                        Type = type,
                        StartRow = 1,
                        Columns = GetColumns(ZsgColumns)
                    };
                default:
                    return null;
            }
        }

        public void Save(Salary model)
        {
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

        public void ImportData(int year, int month, string filePath, SalaryDataType type = 0)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            var excel = ExcelHelper.GetWorkbook(filePath);
            var sheet = excel.GetSheetAt(0);
            var firstRow = sheet.GetRow(0);
            if (firstRow == null)
            {
                throw new Exception("内容格式不正确");
            }
            var firstCell = firstRow.GetCell(0);

            if (type == 0)
            {
                if (firstCell.StringCellValue == "部门")
                {
                    type = SalaryDataType.ZSG;
                }
                else if (firstCell.StringCellValue.Contains("不动产"))
                {
                    type = SalaryDataType.BDC;
                }
                else
                {
                    type = SalaryDataType.LSG;
                }
            }
            if (type == 0)
            {
                throw new Exception("内容格式不正确");
            }

            var desc = GetSalaryDataDescriptor(type);
            var data = sheet.ReadData(desc.StartRow);
            for (var rownum = data.Min(e => e.Row); rownum <= data.Max(e => e.Row); rownum++)
            {
                var model = new Salary { Month = month, Year = year };
                var vals = data.Where(e => e.Row == rownum);
                if (vals.Count() != desc.Columns.Count)
                {
                    throw new Exception("文档格式不正确，列数不一致");
                }
                foreach (var val in vals)
                {
                    var column = desc.Columns.FirstOrDefault(e => e.Order == val.Column);
                    model.Data[column.Name] = val.Value;
                    if (column.Name == "姓名")
                    {
                        var name = val.Value?.ToString();
                        var user = DB.Users.FirstOrDefault(e => e.Username == name);
                        if (user != null)
                        {
                            model.UserId = user.ID;
                        }
                    }
                }
                Save(model);
            }
        }
    }
}
