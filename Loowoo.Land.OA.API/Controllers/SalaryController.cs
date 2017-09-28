using Loowoo.Common;
using Loowoo.Land.OA.Models;
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
        public object List(int year, int userId = 0)
        {
            if (userId == 0)
            {
                userId = CurrentUser.ID;
            }
            return Core.SalaryManager.GetList(year, userId);
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
        public void Import(int year, int month, string files)
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

            foreach (var filePath in files.Split(','))
            {
                Core.SalaryManager.ImportData(year, month, filePath);
            }
        }
    }
}