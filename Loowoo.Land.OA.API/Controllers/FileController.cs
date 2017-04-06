using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FileController : ControllerBase
    {
        private string _uploadDir = "upload_files/";

        [HttpGet]
        public HttpResponseMessage Index(int id)
        {
            var file = Core.FileManager.GetModel(id);
            var filePath = Path.Combine(_uploadDir, file.SavePath);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [HttpPost]
        public IHttpActionResult Upload(string name = null, int id = 0, int infoId = 0)
        {
            TaskName = "文件上传";
            var files = HttpContext.Current.Request.Files;
            if (files.Count == 0)
            {
                return BadRequest("没有上传文件");
            }
            var inputFile = string.IsNullOrWhiteSpace(name) ? files[0] : files[name];
            if (inputFile == null)
            {
                return BadRequest($"{TaskName}:未找到文件{name}相关信息");
            }
            if (!System.IO.Directory.Exists(_uploadDir))
            {
                System.IO.Directory.CreateDirectory(_uploadDir);
            }
            var fileExt = Path.GetExtension(inputFile.FileName);
            var fileName = (inputFile.FileName + inputFile.ContentLength).MD5() + fileExt;
            var savePath = _uploadDir + fileName;
            inputFile.SaveAs(Path.Combine(Environment.CurrentDirectory, _uploadDir + fileName));
            var file = new Loowoo.Land.OA.Models.File
            {
                FileName = inputFile.FileName,
                Size = inputFile.ContentLength,
                SavePath = savePath,
                InfoId = infoId,
                ID = id
            };
            Core.FileManager.Save(file);

            return Ok(file);
        }

        public void UpdateRelation(int[] fileIds, int infoId)
        {
            Core.FileManager.Relation(fileIds, infoId);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Core.FileManager.Delete(id);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult List(int? infoId = null, int? formId = null, int? page = null, int? rows = null, FileType? type = null)
        {
            var parameter = new FileParameter
            {
                InfoId = infoId,
                FormId = formId,
                Type = type,
                Page = new Loowoo.Common.PageParameter(page, rows)
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
