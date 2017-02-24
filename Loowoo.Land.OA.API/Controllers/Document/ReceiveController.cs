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
    /// 收文管理
    /// </summary>
    public class ReceiveController : LoginControllerBase
    {
        /// <summary>
        /// 作用：收文登记
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日19:07:11
        /// </summary>
        /// <param name="recDoc"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] ReceiveDocument recDoc)
        {
            if (recDoc == null || string.IsNullOrEmpty(recDoc.Number) || string.IsNullOrEmpty(recDoc.Title))
            {
                return BadRequest("收文登记：请核对收文内容，收文对象不正确、收文编号不能为空、收文标题不能为空");
            }
            if (recDoc.UID == 0)
            {
                return BadRequest("收文登记：承办人ID不能为0");
            }
            var user = Core.UserManager.Get(recDoc.UID);
            if (user == null)
            {
                return BadRequest("收文登记：系统中未找到承办人");
            }
            var id = 0;
            try
            {
                id = Core.Receive_DocumentManager.Save(recDoc);
                if (id <= 0)
                {
                    return BadRequest("收文登记失败");
                }

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex,"收文登记");
                return BadRequest($"收文登记发生错误,{ex.InnerException.InnerException.Message}");
            }
            //if(SaveFlow(new Flow { Name = recDoc.Title, InfoID = id, InfoType = 0 }))
            //{
            //    return Ok();
            //}
            return BadRequest("收文登记成功，但是FLOW信息录入失败");
        }

        /// <summary>
        /// 作用：对收文进行编辑修改处理操作
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="recDoc"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] ReceiveDocument recDoc)
        {
            if (recDoc == null || recDoc.ID == 0)
            {
                return BadRequest("收文编辑：请核对收文内容，存在的错误：收文信息为空、无法获取收文ID、收文ID不正确");
            }
            try
            {
                Core.Receive_DocumentManager.Edit(recDoc);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex,"收文编辑修改");
                return BadRequest("收文编辑修改失败！");
            }
            return Ok();
        }

        /// <summary>
        /// 作用：收文查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日19:30:56
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ReceiveDocument> Search(int page,int rows)
        {
            var parameter = new ReceiveParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows)
            };
            var list = Core.Receive_DocumentManager.Search(parameter);
            return list;
        }

        /// <summary>
        /// 作用：归档收文
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日09:16:19
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        public void Filing(int id,Filing filing)
        {
            try
            {
                Core.Receive_DocumentManager.Filing(id, filing);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "收文归档");
            }
        }

        /// <summary>
        /// 作用：删除收文
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日09:42:52
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.Receive_DocumentManager.Delete(id);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "收文删除");
            }
        }
        /// <summary>
        /// 作用：通过ID获取收文信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日13:57:48
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID参数错误");
            }
            var receDoc = Core.Receive_DocumentManager.Get(id);
            if (receDoc == null)
            {
                return NotFound();
            }
            //receDoc.Flow = Core.FlowManager.Get(receDoc.ID, 0);
            //if (receDoc.Flow != null)
            //{
            //    receDoc.Flow.Steps = Core.FlowStepManager.GetByFlowID(receDoc.Flow.ID);
            //}
            return Ok(receDoc);
        }

    }
}
