using Loowoo.Common;
using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class SealController : ControllerBase
    {
        [HttpGet]
        public object List()
        {
            var list = Core.SealManager.GetList();
            return list.ToList().Select(e => new
            {
                e.Name,
                e.Status,
                e.ID,
            });
        }

        [HttpPost]
        public void Save([FromBody]Seal data)
        {
            var model = Core.SealManager.Get(data.ID) ?? data;
            model.Name = data.Name;
            Core.SealManager.Save(model);
        }

        [HttpPost]
        public void Apply([FromBody]FormInfoExtend1 data)
        {
            var model = Core.SealManager.Get(data.InfoId);
            if (model == null)
            {
                throw new ArgumentException("参数不正确，没有找该图章");
            }
            if (model.Status != SealStatus.Unused)
            {
                throw new Exception("当前图章在使用中，无法申请");
            }
            if (data.ApprovalUserId == 0)
            {
                throw new Exception("没有选择审批人");
            }
            data.UserId = CurrentUser.ID;
            if (Core.FormInfoExtend1Manager.HasApply(data))
            {
                throw new Exception("你已经申请过该图章，还未通过审批");
            }
            var info = Core.SealManager.Apply(data);
            Core.FeedManager.Save(new Feed
            {
                Action = UserAction.Apply,
                Title = info.Title,
                InfoId = data.ID,
                Type = FeedType.Flow,
                ToUserId = data.ApprovalUserId,
                FromUserId = CurrentUser.ID,
            });
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.SealManager.Delete(id);
        }
    }
}