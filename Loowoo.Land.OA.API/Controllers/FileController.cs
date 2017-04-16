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
            var contentType = GetContentType(file.FileName);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = file.FileName,
            };
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
            inputFile.SaveAs(Path.Combine(Environment.CurrentDirectory, _uploadDir + fileName));
            var file = new OA.Models.File
            {
                FileName = inputFile.FileName,
                Size = inputFile.ContentLength,
                SavePath = fileName,
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

        [HttpGet]
        public IHttpActionResult ConvertToPdf(int id)
        {
            var file = Core.FileManager.GetModel(id);
            var ext = Path.GetExtension(file.SavePath);
            if (ext.EndsWith("doc") || ext.EndsWith("docx"))
            {
                var pdfFile = new OA.Models.File
                {
                    FileName = file.FileName + ".pdf",
                    InfoId = file.InfoId,
                    SavePath = file.SavePath + ".pdf",
                    Size = file.Size
                };
                var docPath = Path.Combine(_uploadDir, file.SavePath);
                var doc = new Aspose.Words.Document(docPath);
                var pdfPath = docPath + ".pdf";
                var pdf = doc.Save(pdfPath, new Aspose.Words.Saving.PdfSaveOptions
                {
                    JpegQuality = 100,
                    UseHighQualityRendering = true,
                    ZoomBehavior = Aspose.Words.Saving.PdfZoomBehavior.FitWidth,
                    SaveFormat = Aspose.Words.SaveFormat.Pdf
                });
                Core.FileManager.Save(pdfFile);
                return Ok(pdfFile);
            }
            else
            {
                return BadRequest("不支持该文件格式");
            }
        }

        public static string GetContentType(string fileName)
        {
            var types = @"ez,application/andrew-inset
hqx,application/mac-binhex40
cpt,application/mac-compactpro
doc,application/msword
bin,application/octet-stream
dms,application/octet-stream
lha,application/octet-stream
lzh,application/octet-stream
exe,application/octet-stream
class,application/octet-stream
so,application/octet-stream
dll,application/octet-stream
oda,application/oda
pdf,application/pdf
ai,application/postscript
eps,application/postscript
ps,application/postscript
smi,application/smil
smil,application/smil
mif,application/vnd.mif
xls,application/vnd.ms-excel
ppt,application/vnd.ms-powerpoint
wbxml,application/vnd.wap.wbxml
wmlc,application/vnd.wap.wmlc
wmlsc,application/vnd.wap.wmlscriptc
bcpio,application/x-bcpio
vcd,application/x-cdlink
pgn,application/x-chess-pgn
cpio,application/x-cpio
csh,application/x-csh
dcr,application/x-director
dir,application/x-director
dxr,application/x-director
dvi,application/x-dvi
spl,application/x-futuresplash
gtar,application/x-gtar
hdf,application/x-hdf
js,application/x-javascript
skp,application/x-koan
skd,application/x-koan
skt,application/x-koan
skm,application/x-koan
latex,application/x-latex
nc,application/x-netcdf
cdf,application/x-netcdf
sh,application/x-sh
shar,application/x-shar
swf,application/x-shockwave-flash
sit,application/x-stuffit
sv4cpio,application/x-sv4cpio
sv4crc,application/x-sv4crc
tar,application/x-tar
tcl,application/x-tcl
tex,application/x-tex
texinfo,application/x-texinfo
texi,application/x-texinfo
t,application/x-troff
tr,application/x-troff
roff,application/x-troff
man,application/x-troff-man
me,application/x-troff-me
ms,application/x-troff-ms
ustar,application/x-ustar
src,application/x-wais-source
xhtml,application/xhtml+xml
xht,application/xhtml+xml
zip,application/zip
au,audio/basic
snd,audio/basic
mid,audio/midi
midi,audio/midi
kar,audio/midi
mpga,audio/mpeg
mp2,audio/mpeg
mp3,audio/mpeg
aif,audio/x-aiff
aiff,audio/x-aiff
aifc,audio/x-aiff
m3u,audio/x-mpegurl
ram,audio/x-pn-realaudio
rm,audio/x-pn-realaudio
rpm,audio/x-pn-realaudio-plugin
ra,audio/x-realaudio
wav,audio/x-wav
pdb,chemical/x-pdb
xyz,chemical/x-xyz
bmp,image/bmp
gif,image/gif
ief,image/ief
jpeg,image/jpeg
jpg,image/jpeg
jpe,image/jpeg
png,image/png
tiff,image/tiff
tif,image/tiff
djvu,image/vnd.djvu
djv,image/vnd.djvu
wbmp,image/vnd.wap.wbmp
ras,image/x-cmu-raster
pnm,image/x-portable-anymap
pbm,image/x-portable-bitmap
pgm,image/x-portable-graymap
ppm,image/x-portable-pixmap
rgb,image/x-rgb
xbm,image/x-xbitmap
xpm,image/x-xpixmap
xwd,image/x-xwindowdump
igs,model/iges
iges,model/iges
msh,model/mesh
mesh,model/mesh
silo,model/mesh
wrl,model/vrml
vrml,model/vrml
css,text/css
html,text/html
htm,text/html
asc,text/plain
txt,text/plain
rtx,text/richtext
rtf,text/rtf
sgml,text/sgml
sgm,text/sgml
tsv,text/tab-separated-values
wml,text/vnd.wap.wml
wmls,text/vnd.wap.wmlscript
etx,text/x-setext
xsl,text/xml
xml,text/xml
mpeg,video/mpeg
mpg,video/mpeg
mpe,video/mpeg
qt,video/quicktime
mov,video/quicktime
mxu,video/vnd.mpegurl
avi,video/x-msvideo
movie,video/x-sgi-movie
ice,x-conference/x-cooltalk";

            var ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext))
            {
                return "application/octet-stream";
            }
            ext = ext.Substring(1).ToLower();
            foreach (var t in types.Replace("\r", "").Split('\n'))
            {
                var kv = t.Split(',');
                if (kv[0] == ext)
                {
                    return kv[1].Trim(' ');
                }
            }
            return "application/octet-stream";
        }
    }
}
