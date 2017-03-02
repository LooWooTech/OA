using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class LoginControllerBase : ControllerBase
    {
        /// <summary>
        /// 作用：保存动态信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日10:36:51
        /// </summary>
        /// <param name="feed">动态信息</param>
        protected void Dynamical(Feed feed)
        {
            try
            {
                Core.FeedManager.Save(feed);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, $"{TaskName}-生成动态");
            }
        }

        





        /// <summary>
        /// 作用：生成FlowData
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日14:30:13
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="infoId"></param>
        /// <returns></returns>
        protected bool SaveFlowData(int formId,int infoId)
        {
            var flowData = Core.FlowDataManager.Get(formId, infoId);
            if (flowData == null)
            {
                var form = Core.FormManager.Get(formId);
                if (form == null)
                {
                    ThrowException(string.Format("系统中不存在ID为{0}的表单", formId), "未找到表单");
                }
                var flow = Core.FlowManager.Get(form.FLowID);
                if (flow == null)
                {
                    //没有模板就没有
                    //flow = new Flow { Name = string.Format("{0}自定义模板", CurrentUser != null ? CurrentUser.Name : "") };
                    //if (Core.FlowManager.Save(flow) <= 0)
                    //{
                    //    ThrowException(string.Format("{0}自定义模板失败", CurrentUser != null ? CurrentUser.Name : ""), "生成模板");
                    //}
                }

                flowData = new FlowData { InfoId = infoId, FormId = formId, FlowId = flow == null ? 0 : flow.ID };
                return Core.FlowDataManager.Save(flowData) > 0;
            }
            return true;
        }

        /// <summary>
        /// 作用：更新文件附件信息列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日09:31:14
        /// </summary>
        /// <param name="fileIds"></param>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        protected void UpdateFileRelation(int[] fileIds,int infoId,int formId)
        {
            Core.FileManager.Relation(fileIds, infoId, formId);

            //if (Core.FileRelationManager.Change(fileIds, infoId, formId))
            //{
            //    Core.FileRelationManager.Remove(infoId, formId);
            //    if (fileIds != null && fileIds.Count() > 0)
            //    {
            //        Core.FileRelationManager.AddRange(fileIds.Select(e => new FileRelation { InfoID = infoId, FormID=formId, FileID = e }).ToList());
            //    }
            //}
          
        }
        /// <summary>
        /// 作用：发布动态信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:03:55
        /// </summary>
        /// <param name="feed"></param>
        protected int SaveFeed(Feed feed)
        {
            return Core.FeedManager.Save(feed);
        }
        /// <summary>
        /// 作用：发布短消息
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:10:48
        /// </summary>
        /// <param name="message"></param>
        protected void SaveMessage(Message message)
        {
            Core.MessageManager.Save(message);
        }
        /// <summary>
        /// 作用：
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:33:41
        /// </summary>
        /// <param name="userForm"></param>
        protected void SaveUserForm(UserForm userForm)
        {
            Core.UserFormManager.Save(userForm);
        }

    }
}
