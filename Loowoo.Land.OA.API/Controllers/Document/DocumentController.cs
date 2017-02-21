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
    public class DocumentController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存公文拟稿或审批流程中更新拟稿内容
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日14:23:56
        /// </summary>
        /// <param name="missive"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Missive missive)
        {
            TaskName = "保存公文";
            if(missive==null
                ||string.IsNullOrEmpty(missive.Number)
                || string.IsNullOrEmpty(missive.Title))
            {
                return BadRequest($"{TaskName}:未获取公文相关信息、公文字号、公文标题不能为空");
            }
            var user = Core.UserManager.Get(missive.UserID);
            if (user == null)
            {
                return BadRequest($"{TaskName}:未找到承办人相关信息");
            }
            var born = Core.DepartmentManager.Get(missive.BornOrganID);
            if (born == null)
            {
                return BadRequest($"{TaskName}:未找到公文机关部门信息");
            }
            var to = Core.DepartmentManager.Get(missive.ToOrganID);
            if (to == null)
            {
                return BadRequest($"{TaskName}:未找到发往部门信息");
            }
            if (missive.ID > 0)
            {
                if (!Core.MissiveManager.Edit(missive))
                {
                    return BadRequest($"{TaskName}:更新公文拟稿失败，可能未找到系统拟稿");
                }
            }
            else
            {
                var id = Core.MissiveManager.Save(missive);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:新建公文失败");
                }
            }
            var flow = Core.FlowManager.Get(missive.ID, 1);
            if (flow == null)
            {
                flow = new Flow { Name = missive.Title, InfoID = missive.ID, InfoType = 1 };
                SaveFlow(flow);
            }
            missive.Flow = flow;
            return Ok(missive);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Missive> SendList(int page=1,int rows=20)
        {
            var parameter = new MissiveParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows)
            };
            var list = Core.MissiveManager.Search(parameter);
            return list;
        }

        ///// <summary>
        ///// 作用：获取公文列表
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月11日18:18:22
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="rows"></param>
        ///// <returns></returns>
        //public List<SendDocument> GetList(int page,int rows)
        //{
            
        //    var parameter = new DocumentParameter
        //    {
        //        Page = new Common.PageParameter(page, rows)
        //    };
        //    var list = Core.DocumentManager.Search(parameter);
        //    return list;
        //}

        ///// <summary>
        ///// 作用：创建公文  成功 返回OK；失败：BadRequest
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月11日18:18:47
        ///// </summary>
        ///// <param name="senddoc"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult Create([FromBody] SendDocument senddoc)
        //{
        //    if(senddoc==null
        //        ||string.IsNullOrEmpty(senddoc.Number)
        //        ||string.IsNullOrEmpty(senddoc.Title)
        //        || string.IsNullOrEmpty(senddoc.ToOrgan) 
        //        || string.IsNullOrEmpty(senddoc.CcOrgan))
        //    {
        //        return BadRequest("公文编号、标题、主送机关、抄送机关不能为空");
        //    }
        //    try
        //    {
        //        var id= Core.DocumentManager.Create(senddoc);

        //    }catch(Exception ex)
        //    {
        //        LogWriter.WriteException(ex);
        //        return BadRequest("创建公文失败");
        //    }
        //    return Ok();
        //}
        ///// <summary>
        ///// 作用：删除公文  成功：OK  失败：BadRequest
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月11日18:24:25
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult Delete(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest("参数不正确");
        //    }
        //    try
        //    {
        //        if (!Core.DocumentManager.Delete(id))
        //        {
        //            return NotFound();
        //        }
        //    }catch(Exception ex)
        //    {
        //        LogWriter.WriteException(ex);
        //        return BadRequest("删除失败");
        //    }
        //    return Ok();
        //}
        ///// <summary>
        ///// 作用：通过公文ID获取公文信息 
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月11日19:11:45
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult Get(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest("ID参数不正确");
        //    }
        //    var doc = Core.DocumentManager.Get(id);
        //    if (doc == null)
        //    {
        //        return NotFound();
        //    }
        //    doc.Flow = Core.FlowManager.Get(doc.ID);
        //    if (doc.Flow != null)
        //    {
        //        doc.Flow.Steps = Core.FlowStepManager.GetByFlowID(doc.Flow.ID);
        //    }
        //    return Ok(doc);
        //}
        //[HttpPost]
        //public IHttpActionResult Edit([FromBody] SendDocument sendDoc)
        //{
        //    if (sendDoc == null || string.IsNullOrEmpty(sendDoc.Number))
        //    {
        //        return BadRequest("收文")
        //    }
        //}
    }
}
