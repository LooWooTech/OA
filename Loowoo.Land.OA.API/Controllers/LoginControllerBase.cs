using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
   // [RequestAuthorize(Role =Security.UserRole.User)]
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
        /// 作用：生成FLOW 审批信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日15:41:59
        /// </summary>
        /// <param name="flow"></param>
        protected bool SaveFlow(Flow flow)
        {
            try
            {
                var id = Core.FlowManager.Save(flow);
                if (id > 0)
                {
                    return true;
                }

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, $"{TaskName}-生成审批请求");
            }
            return false;
        } 
    }
}
