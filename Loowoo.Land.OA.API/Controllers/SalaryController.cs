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
        public int[] GetYears(int userId = 0)
        {
            if (userId == 0)
                userId = CurrentUser.ID;

            return Core.SalaryManager.GetYears(userId);
        }

        [HttpGet]
        public object List(int year, int month = 0, int userId = 0, int page = 1, int rows = 12)
        {
            var hasViewAllRight = CurrentUser.HasRight("Form.Salary.View");
            if (userId == 0)
            {
                if (!hasViewAllRight)
                {
                    userId = CurrentUser.ID;
                    month = 0;
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
                Year = year,
                Month = month,
                UserId = userId,
                Page = new PageParameter(page, rows)
            };
            var list = Core.SalaryManager.GetList(parameter).Select(e => new Models.SalaryViewModel(e));
            return new { List = list, Page = parameter.Page };
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
        public object Import(int year, int month, string files)
        {
            if (string.IsNullOrEmpty(files))
            {
                throw new Exception("没有选择Excel文件");
            }

            var date = new DateTime(year, month, 1);
            if (date > DateTime.Now)
            {
                throw new Exception("日期选择不正确");
            }

            if (!CurrentUser.HasRight("Form.Salary.Edit"))
            {
                throw new Exception("没有权限导入工资单");
            }

            var fails = new List<object>();
            foreach (var filePath in files.Split(','))
            {
                var failRows = Core.SalaryManager.ImportData(year, month, filePath);
                if (failRows != null && failRows.Count > 0)
                {
                    var fileName = filePath.Split('/').Last();
                    fails.Add(new { fileName, failRows });
                }
            }
            return fails;
        }
    }
}