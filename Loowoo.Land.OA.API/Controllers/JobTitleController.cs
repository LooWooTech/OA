using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class JobTitleController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<JobTitle> List()
        {
            return Core.JobTitleManager.GetList();
        }

        [HttpPost]
        public void Save(JobTitle model)
        {
            Core.JobTitleManager.Save(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.JobTitleManager.Delete(id);
        }
    }
}