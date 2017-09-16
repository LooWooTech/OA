using Loowoo.Common;
using Loowoo.Land.OA.Models;
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
        private static readonly string[] LsgColumns = "序号,帐号,工号,姓名,工资,岗位津贴,工龄工资,加班,补贴,学历,冷饮费,车贴,应发工资,养老保险,救助金5元/月,失业保险0.5%,补缴公积金,扣除,公积金,扣款合计,应发,个调税,实发工资".Split(',').ToArray();
        private static readonly string[] BdcColumns = "序号,姓名,公积金,补缴个人部分,个人扣款合计,养老,失业保险,医疗,工伤,生育,公积金,补缴单位部分,单位扣款合计,合计,扣税工资,扣税,扣除差额,补上月多扣部分,实发数".Split(',').ToArray();

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

        public List<Salary> GetImportData(int year, int month, SalaryDataType type, string absolutelyPath)
        {
            var result = new List<Salary>();
            var desc = GetSalaryDataDescriptor(type);
            var excel = ExcelHelper.GetWorkbook(absolutelyPath);
            var sheet = excel.GetSheetAt(0);
            var data = sheet.ReadData(desc.StartRow);
            for (var rownum = data.Min(e => e.Row); rownum <= data.Max(e => e.Row); rownum++)
            {
                var model = new Salary { Month = month, Year = year, Data = new Document() };
                var nameColumn = desc.Columns.FirstOrDefault(e => e.Name == "姓名");
                foreach (var val in data.Where(e => e.Row == rownum))
                {
                    var column = desc.Columns.FirstOrDefault(e => e.Order == val.Column);
                    model.Data[column.Name] = val.Value;
                }
                result.Add(model);
            }

            return result;
        }

    }
}
