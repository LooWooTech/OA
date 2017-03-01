using Loowoo.Common;
using Loowoo.Land.OA.Base;
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
    public class MissiveController : LoginControllerBase
    {
        /// <summary>
        /// 作用：生成公文信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日14:34:39
        /// </summary>
        /// <param name="missive"></param>
        /// <param name="fileIds"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Missive missive, int[] fileIds)
        {
            TaskName = "保存公文";
            if (missive == null
                || string.IsNullOrEmpty(missive.Number)
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


            var form = Core.FormManager.Get("公文");
            if (form == null)
            {
                return BadRequest($"{TaskName}:未查询到公文表单信息");
            }

            UpdateFileRelation(fileIds, missive.ID, form.ID);

            if (!SaveFlowData(form.ID, missive.ID))
            {
                return BadRequest($"{TaskName}:生成流程记录失败");
            }

            return Ok(missive);
        }

        /// <summary>
        /// 作用：删除公文
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日16:35:14
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.MissiveManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest("删除公文：删除失败，未找到需要删除的公文信息");
        }
        /// <summary>
        /// 作用：获取一个公文实体
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日16:46:52
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.MissiveManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            model.UnderTaker = Core.UserManager.Get(model.UserID);
            model.Category = Core.CategoryManager.Get(model.CategoryID);
            return Ok(model);
        }

        /// <summary>
        /// 作用：获取发文拟稿列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日12:40:32
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SendList(int page = 1, int rows = 20)
        {
            TaskName = "发文拟稿列表";
            if (CurrentUser == null)
            {
                return BadRequest($"{TaskName}:未获取当前用户信息");
            }
            var parameter = new MissiveParameter
            {
                UserID = CurrentUser.ID,
                Page = new PageParameter(page, rows)
            };
            //获取当前用户相关的发文
            var list = Core.MissiveManager.Search(parameter);
            var form = Core.FormManager.Get("发文");
            if (form != null)
            {
                //获取别人提交给当前用户的
                var uf = Core.UserFormManager.GetList(CurrentUser.ID, form.ID);
                var receive = Core.MissiveManager.GetList(uf.Select(e => e.InfoID).ToArray());
                list.AddRange(receive);
                list = list.DistinctBy(e => e.ID).ToList();
            }
            list = list.OrderByDescending(e => e.CreateTime).SetPage(parameter.Page).ToList();
            list = Core.MissiveManager.ReBody(list);
            var table = new PagingResult<Missive>
            {
                List = list,
                Page = parameter.Page
            };
            return Ok(table);
        }

    }
}
