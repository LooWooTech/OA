using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    /// <summary>
    /// 组管理
    /// </summary>
    public class GroupController : ControllerBase
    {
        [HttpGet]
        public object List()
        {
            return Core.GroupManager.GetList();
        }

        [HttpPost]
        public void Save([FromBody] Group group)
        {
            Core.GroupManager.Save(group);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.GroupManager.Delete(id);
        }
    }
}
