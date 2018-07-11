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
                FlowStatus = status.HasValue ? new[] { status.Value } : null,
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
                    e.WJ_LY,
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

        [HttpGet]
        public object Transfer(int id)
        {
            var info = Core.FormInfoManager.GetModel(id);
            if (info.FlowData == null
                || !info.FlowData.Completed
                || info.FlowData.GetLastNodeData().UserId != Identity.ID
                )
            {
                throw new Exception("转发失败，请检查流程是否完结且具备转发权限");
            }
            var missive = Core.MissiveManager.GetModel(id);
            var model = new Missive
            {
                ContentId = missive.ContentId,
                DJR = missive.DJR,
                FW_RQ = missive.FW_RQ,
                GK_FB = missive.GK_FB,
                JB_RQ = missive.JB_RQ ?? DateTime.Today,
                JJ_DJ = missive.JJ_DJ,
                QX_RQ = missive.QX_RQ,
                WJ_BT = missive.WJ_BT,
                WJ_MJ = missive.WJ_MJ,
                WJ_ZH = missive.WJ_ZH,
                WJ_ZY = missive.WJ_ZY,
                ZRR = missive.ZRR,
                ZTC = missive.ZTC,
                ZW_GK = missive.ZW_GK,
                ZS_JG = missive.ZS_JG ?? AppSettings.Get("Government") ?? "舟山市国土资源局定海分局",
            };
            var formInfo = new FormInfo
            {
                FormId = (int)FormType.ReceiveMissive,
                Title = info.Title,
                PostUserId = Identity.ID,
            };
            Core.FormInfoManager.Save(formInfo);
            Core.FlowDataManager.CreateFlowData(formInfo);
            model.ID = formInfo.ID;
            Core.MissiveManager.Save(model);

            return formInfo;
        }
    }
}