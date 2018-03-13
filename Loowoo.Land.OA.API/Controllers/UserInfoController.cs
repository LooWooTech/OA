using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public void Read(int id)
        {
            Core.UserFormInfoManager.Read(id, Identity.ID);
        }

        [HttpGet]
        public void ReadAll()
        {
            Core.UserFormInfoManager.ReadAll(Identity.ID);
        }

        [HttpGet]
        public void Star(int id)
        {
            Core.UserFormInfoManager.UpdateStar(id, Identity.ID, true);
        }

        [HttpGet]
        public void UnStar(int id)
        {
            Core.UserFormInfoManager.UpdateStar(id, Identity.ID, false);
        }

        [HttpDelete]
        public void Trash(int id)
        {
            Core.UserFormInfoManager.UpdateTrash(id, Identity.ID, true);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.UserFormInfoManager.Delete(id, Identity.ID);
        }

        [HttpGet]
        public void Recovery(int id)
        {
            Core.UserFormInfoManager.UpdateTrash(id, Identity.ID, false);
        }
    }
}