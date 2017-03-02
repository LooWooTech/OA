using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FlowNodeController : LoginControllerBase
    {
        /// <summary>
        /// 作用：提交流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日14:57:13
        /// </summary>
        /// <param name="nodedata"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Submit([FromBody] FlowNodeData nodedata)
        {
            TaskName = "保存流程节点记录";
            if (nodedata == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点记录信息");
            }
            var user = Core.UserManager.Get(nodedata.UserId);
            if (user == null)
            {
                return BadRequest($"{TaskName}:未找到UserID{nodedata.UserId}的用户信息");
            }
            var flowdata = Core.FlowDataManager.Get(nodedata.FlowDataId);
            if (flowdata == null)
            {
                return BadRequest($"{TaskName}:未找到流程记录信息");
            }
            if (nodedata.ID > 0)
            {
                if (!Core.FlowNodeDataManager.Edit(nodedata))
                {
                    return BadRequest($"{TaskName}:未找到需要更新的流程节点记录信息");
                }
            }
            else
            {
                var id = Core.FlowNodeDataManager.Save(nodedata);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败！");
                }
            }
            var feedId = SaveFeed(new Feed { FormId = flowdata.FormId, InfoId = flowdata.InfoId, FromUserId = CurrentUser != null ? CurrentUser.ID : 0, ToUserId = nodedata.UserId });
            if (feedId > 0)
            {
                SaveMessage(new Message { Content = "提交审核", FormUserId = CurrentUser != null ? CurrentUser.ID : 0, ToUserId = nodedata.UserId, FeedId = feedId });
            }
            SaveUserForm(new UserForm { UserID = nodedata.UserId, FormID = flowdata.FormId, InfoID = flowdata.InfoId });
            return Ok(nodedata);
        }

        /// <summary>
        /// 作用：判断当前用户能否撤销当前已提交的结点
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CanCancel(int id)
        {
            var flowNodeData = Core.FlowNodeDataManager.Get(id);
            if (flowNodeData == null)
            {
                return NotFound();
            }
            return Ok();
        }
        /// <summary>
        /// 作用：撤销流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日16:35:54
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Cancel(int id)
        {
            if (Core.FlowNodeDataManager.Cancel(id))
            {
                var flownodedata = Core.FlowNodeDataManager.Get(id);
                return Ok();
            }
            return BadRequest("撤销流程：未找到撤销的流程节点记录信息");
        }
    }
}
