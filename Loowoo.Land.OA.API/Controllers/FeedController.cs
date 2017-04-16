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
    public class FeedController : ControllerBase
    {
        [HttpGet]
        public IHttpActionResult List(int formId, DateTime? beginTime = null, int page = 1, int rows = 20)
        {
            var parameter = new FeedParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                FormId = formId,
                UserId = CurrentUser.ID,
            };

            var list = Core.FeedManager.Search(parameter);
            var table = new PagingResult<Feed>
            {
                List = list,
                Page = parameter.Page
            };
            return Ok(table);
        }
    }
}
