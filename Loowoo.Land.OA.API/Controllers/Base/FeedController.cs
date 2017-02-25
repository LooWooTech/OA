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
    public class FeedController : LoginControllerBase
    {
        /// <summary>
        /// 作用：获取动态列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日15:02:08
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="infoType"></param>
        /// <param name="userId"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult List(int page,int rows,int infoType,int userId,DateTime beginTime)
        {
            var parameter = new FeedParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                InfoType = infoType,
                UserId = userId,
                BeginTime = beginTime
            };
            var list = Core.FeedManager.Search(parameter);
            var table = new PagingResult<Feed>
            {
                List = list.ToArray(),
                Page = parameter.Page
            };
            return Ok(table);
        }
    }
}
