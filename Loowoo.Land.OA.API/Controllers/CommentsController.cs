using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class CommentsController : ApiController
    {
        [Route("blogposts/{postId}/comments")]
        public async Task<IHttpActionResult> Get(int postId)
        {
            var comments = new Comment[] { new Comment {
            PostId = postId,
            Body = "Coding changes the world1" } };
            return Ok<Comment[]>(comments);
        }
    }

    public class Comment
    {
        public int PostId { get; set; }
        public string Body { get; set; }
    }
}
