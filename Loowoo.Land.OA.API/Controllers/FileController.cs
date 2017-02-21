using Loowoo.Land.OA.API.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FileController : LoginControllerBase
    {
        [HttpPut]
        public async Task<IHttpActionResult> Upload(string name)
        {
            TaskName = "文件上传";
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"{TaskName}:文件名称不能为空");
            }
            var file = await FileHelper.Upload2(Request,name);
            if (file == null)
            {
                return BadRequest($"{TaskName}:未找到文件{name}相关信息");
            }
            var id = Core.FileManager.Save(file);
            if (id <= 0)
            {
                return BadRequest($"{TaskName}:文件记录生成失败");
            }
            return Ok(file);
        }

    }
}
