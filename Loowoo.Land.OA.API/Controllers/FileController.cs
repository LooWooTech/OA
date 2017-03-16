using Loowoo.Land.OA.API.Common;
using Loowoo.Land.OA.Base;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
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
    /// <summary>
    /// 文件管理
    /// </summary>
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 作用：文件上传 返回文件类
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日17:26:57
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 作用：删除文件
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日17:32:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            TaskName = "删除文件";
            if (Core.FileManager.Delete(id))
            {
                return Ok();
            }

            return BadRequest($"{TaskName}:该文件不存在或已经删除");
        }

        /// <summary>
        /// 作用：获取符合条件的文件列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日10:10:17
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult List(int? infoId=null,int?formId=null,int?page=null,int?rows=null,FileType? type = null)
        {
            var parameter = new FileParameter
            {
                InfoId = infoId,
                FormId = formId,
                Type = type,
                Page=new Loowoo.Common.PageParameter(page,rows)
            };
            var list = Core.FileManager.Search(parameter);
            var table = new PagingResult<OA.Models.File>
            {
                List = list,
                Page = parameter.Page
            };
            return Ok(table);
        }

    }
}
