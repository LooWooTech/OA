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
    /// <summary>
    /// 发文管理
    /// </summary>
    public class SendController : LoginControllerBase
    {
        /// <summary>
        /// 作用：通过ID获取发文信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:01:46
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID 参数不正确");
            }
            var send = Core.Send_DocumentManager.Get(id);
            if (send == null)
            {
                return NotFound();
            }
            send.Flow = Core.FlowManager.Get(send.ID, 1);
            if (send.Flow != null)
            {
                send.Flow.Steps = Core.FlowStepManager.GetByFlowID(send.Flow.ID);
            }
            return Ok(send);
        }
        /// <summary>
        /// 作用：发文
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:10:30
        /// </summary>
        /// <param name="senddoc"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] SendDocument senddoc)
        {
            if (senddoc == null)
            {
                return BadRequest("发文：请核对发文内容");
            }
            var id = 0;
            try
            {
               
                id = Core.Send_DocumentManager.Save(senddoc);
                if (id <= 0)
                {
                    return BadRequest("发文失败");
                }
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "发文登记");
                return BadRequest("发文发生错误");
            }

            var flowId = Core.FlowManager.Save(new Flow { Name = senddoc.Title, InfoID = id, InfoType = 1 });
            if (flowId > 0)
            {
                return Ok();
            }
            return BadRequest("发文信息成功保存，但是FLOW信息添加失败");
               
        }
        /// <summary>
        /// 作用：对发文进行编辑修改  ID作为修改查询依据
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:17:03
        /// </summary>
        /// <param name="senddoc"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] SendDocument senddoc)
        {
            if (senddoc == null || senddoc.ID == 1)
            {
                return BadRequest("发文编辑：请核对收文内容，存在的错误：发文信息为空，无法获取发文ID，发文ID不正确");
            }
            try
            {
                Core.Send_DocumentManager.Edit(senddoc);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "发文编辑修改");
                return BadRequest("发文编辑修改失败");
            }
            return Ok();
        }
        /// <summary>
        /// 作用：获取发文列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:35:01
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public List<SendDocument> Search(int page,int rows)
        {
            var parameter = new SendParameter
            {
                Page = new Common.PageParameter(page, rows)
            };
            var list = Core.Send_DocumentManager.Search(parameter);
            return list;
        }
        /// <summary>
        /// 作用：对发文进行归档处理
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:47:23
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filing"></param>
        [HttpPut]
        public void Filing(int id,Filing filing)
        {
            try
            {
                Core.Send_DocumentManager.Filing(id, filing);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "发文归档");
            }
        }
        /// <summary>
        /// 作用：发文删除
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日15:10:22
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.Send_DocumentManager.Delete(id);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "发文删除");
            }
        }
    }
}
