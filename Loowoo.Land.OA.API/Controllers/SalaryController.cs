using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class SalaryController : ControllerBase
    {
        [HttpGet]
        public int[] GetYears()
        {
            return Core.SalaryManager.GetYears();
        }

        [HttpGet]
        public object SalaryDatas(int year = 0, int salaryId = 0, string searchKey = null, int userId = 0, int page = 1, int rows = 15)
        {
            var hasViewAllRight = CurrentUser.HasRight("Form.Salary.View");
            if (userId == 0)
            {
                if (!hasViewAllRight)
                {
                    userId = CurrentUser.ID;
                }
            }
            else if (userId != CurrentUser.ID)
            {
                if (!hasViewAllRight)
                {
                    throw new Exception("无法查看他人工资");
                }
            }
            var parameter = new SalaryParameter
            {
                SalaryId = salaryId,
                Year = year,
                SearchKey = searchKey,
                UserId = userId,
                Page = new PageParameter(page, rows)
            };
            var list = Core.SalaryManager.GetSalaryDatas(parameter).Select(e => new
            {
                e.ID,
                e.UserId,
                e.UserName,
                e.Salary.Title,
                e.Salary.Year,
                e.Data
            });
            return new { List = list, parameter.Page };
        }

        [HttpGet]
        public object Salaries(int year = 0, string searchKey = null, int page = 1, int rows = 15)
        {
            var parameter = new SalaryParameter
            {
                Year = year,
                SearchKey = searchKey,
                Page = new PageParameter(page, rows)
            };
            var list = Core.SalaryManager.GetList(parameter);
            return new { List = list, parameter.Page };
        }

        [HttpPost]
        public File Upload(string name = null)
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count == 0)
            {
                throw new Exception("没有上传文件");
            }
            var inputFile = string.IsNullOrWhiteSpace(name) ? files[0] : files[name];
            if (inputFile == null)
            {
                throw new Exception("未找到指定的name");
            }

            return File.Upload(inputFile);
        }

        [HttpPost]
        public object Import(int year, string title, string file)
        {
            if (!CurrentUser.HasRight("Form.Salary.Edit"))
            {
                throw new Exception("没有权限导入工资单");
            }
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("没有选择Excel文件");
            }
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("没有填写导入名称");
            }
            var model = new Salary
            {
                Title = title,
                Year = year,
                FilePath = file,
            };
            Core.SalaryManager.Save(model);

            var failRows = Core.SalaryManager.ImportData(model);
            if (failRows != null && failRows.Count > 0)
            {
                return failRows;
            }
            return null;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            if (!CurrentUser.HasRight("Form.Salary.Edit"))
            {
                throw new Exception("没有权限删除工资单");
            }
            var model = Core.SalaryManager.GetModel(id);
            if (model != null)
            {
                Core.SalaryManager.Delete(model);
            }
        }
    }
}