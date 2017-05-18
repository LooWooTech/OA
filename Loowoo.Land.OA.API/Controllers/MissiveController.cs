using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class MissiveController : ControllerBase
    {
        [HttpGet]
        public object Get(int id)
        {
            return Core.MissiveManager.GetModel(id);
        }

        [HttpGet]
        public object List(int formId, int postUserId = 0, string searchKey = null, bool? completed = null, FlowStatus? status = null, int page = 1, int rows = 20)
        {
            var parameter = new UserFormInfoParameter
            {
                FormId = formId,
                Status = status,
                Page = new PageParameter(page, rows),
                UserId = CurrentUser.ID,
                PostUserId = postUserId,
                Completed = completed
            };
            var list = Core.UserFormInfoManager.GetList(parameter);

            var infoIds = list.Select(e => e.InfoId).ToArray();
            var datas = Core.MissiveManager.GetList(new Parameters.MissiveParameter
            {
                Ids = infoIds,
                SearchKey = searchKey,
            });
            return new PagingResult
            {
                List = datas.Select(e => new
                {
                    e.ID,
                    e.WJ_BT,
                    e.WH,
                    e.FW_RQ,
                    MJ = e.MJ.GetDescription(),
                    e.Info.FormId,
                    e.Info.CreateTime,
                    e.Info.UpdateTime,
                    e.Info.FlowStep,
                    e.Info.FlowDataId,
                }),
                Page = parameter.Page
            };
        }

        [HttpPost]
        public void Save(int formId, [FromBody]Missive data)
        {
            var isAdd = data.ID == 0;
            //判断id，如果不存在则创建forminfo
            if (data.ID == 0)
            {
                data.Info = new FormInfo
                {
                    FormId = formId,
                    PostUserId = CurrentUser.ID,
                    Title = data.WJ_BT
                };
                Core.FormInfoManager.Save(data.Info);
            }
            else
            {
                data.Info = Core.FormInfoManager.GetModel(data.ID);
                data.Info.Title = data.WJ_BT;
            }
            if (data.Info.FlowDataId == 0)
            {
                var form = Core.FormManager.GetModel(formId);
                Core.FlowDataManager.Create(form.FLowId, data.Info);
            }
            data.ID = data.Info.ID;
            Core.MissiveManager.Save(data);

            Core.FeedManager.Save(new Feed
            {
                FormId = formId,
                InfoId = data.ID,
                Title = data.WJ_BT,
                Description = data.ZTC,
                FromUserId = CurrentUser.ID,
                Action = isAdd ? UserAction.Create : UserAction.Update,
            });
        }

        [HttpGet]
        public void DeleteWord(int id)
        {
            var model = Core.MissiveManager.GetModel(id);
            model.WordId = 0;
            Core.MissiveManager.Save(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}