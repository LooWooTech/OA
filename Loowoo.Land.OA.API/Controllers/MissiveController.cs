using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class MissiveController : ControllerBase
    {
        [HttpGet]
        public object Model(int id)
        {
            return Core.MissiveManager.GetModel(id);
        }

        [HttpGet]
        public object List(int formId, int postUserId = 0, string searchKey = null, bool? completed = null, FlowStatus? status = null, int page = 1, int rows = 20)
        {
            var parameter = new FormInfoParameter
            {
                FormId = formId,
                Status = status,
                Page = new PageParameter(page, rows),
                UserId = CurrentUser.ID,
                PostUserId = postUserId,
                Completed = completed,
                SearchKey = searchKey,
            };
            var list = Core.UserFormInfoManager.GetList(parameter);

            var datas = Core.MissiveManager.GetList(parameter);

            return new PagingResult
            {
                List = datas.Select(e => new
                {
                    e.ID,
                    e.WJ_BT,
                    e.WJ_ZH,
                    JJ_DJ = e.JJ_DJ.GetDescription(),
                    e.DJR,
                    e.ZRR,
                    e.ZS_JG,
                    e.CS_JG,
                    e.GK_FB,
                    ZW_GK = e.ZW_GK.GetDescription(),
                    e.QX_RQ,
                    e.FW_RQ,
                    MJ = e.WJ_MJ.GetDescription(),
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
                data.Info.Form = Core.FormManager.GetModel(formId);
                Core.FlowDataManager.CreateFlowData(data.Info);
            }
            data.ID = data.Info.ID;
            Core.MissiveManager.Save(data);
            if (data.WordId > 0)
            {
                data.Word = Core.FileManager.GetModel(data.WordId);
                data.Word.InfoId = data.ID;
            }

            Core.FeedManager.Save(new Feed
            {
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