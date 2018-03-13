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
        public object List(int formId, int postUserId = 0, string searchKey = null, bool? completed = null, FlowStatus? status = null, int page = 1, int rows = 10)
        {

            var parameter = new FormInfoParameter
            {
                FormId = formId,
                FlowStatus = status,
                Page = new PageParameter(page, rows),
                UserId = Identity.ID,
                PostUserId = postUserId,
                Completed = completed,
                SearchKey = searchKey,
            };

            var list = Core.MissiveManager.GetList(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.InfoId,
                    e.WJ_ZH,
                    e.Title,
                    e.QX_RQ,
                    e.WJ_MJ,
                    e.Reminded,
                    e.Starred,
                    MJ = e.WJ_MJ.GetDescription(),
                    e.FormId,
                    e.CreateTime,
                    e.UpdateTime,
                    e.FlowStep,
                    e.FlowDataId,
                    e.PostUserId,
                    e.Important,
                    Completed = e.FlowData == null ? false : e.FlowData.Completed,
                    e.Uid,
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
                    PostUserId = Identity.ID,
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

            var file = data.Content;
            data.Content = null;
            Core.MissiveManager.Save(data);
            if (data.ContentId > 0)
            {
                if (file != null && file.InfoId == 0)
                {
                    file.InfoId = data.ID;
                    Core.FileManager.Save(file);
                    //添加红头
                    if (data.RedTitleId > 0)
                    {
                        var redTitle = Core.MissiveManager.GetRedTitle(data.RedTitleId);
                        Core.MissiveManager.AddRedTitle(data, redTitle);
                    }
                }
            }

            var feed = new Feed
            {
                InfoId = data.ID,
                Title = data.WJ_BT,
                Description = data.ZTC,
                FromUserId = Identity.ID,
                Action = isAdd ? UserAction.Create : UserAction.Update,
            };
            Core.FeedManager.Save(feed);
        }

        [HttpGet]
        public void DeleteContent(int id)
        {
            var model = Core.MissiveManager.GetModel(id);
            model.ContentId = 0;
            Core.MissiveManager.Save(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }

        [HttpGet]
        public void UpdateImportant(int id)
        {
            var model = Core.MissiveManager.GetModel(id);
            model.Important = true;
            Core.MissiveManager.Save(model);
        }

        [HttpGet]
        public object RedTitles()
        {
            return Core.MissiveManager.GetRedTitles();
        }

        [HttpPost]
        public void SaveRedTitle(MissiveRedTitle model)
        {
            if (model.TemplateId == 0)
            {
                model.Template = null;
            }
            Core.MissiveManager.SaveRedTitle(model);
        }

        /// <summary>
        /// 上报到市里OA
        /// </summary>
        [HttpGet]
        public void Report(int id)
        {
            var info = Core.FormInfoManager.GetModel(id);
            if (info.Form.FormType == FormType.SendMissive
                && info.FlowData.Completed
                )
            {
                Core.MissiveManager.AddMissiveServiceLog(id);
            }
        }
    }
}