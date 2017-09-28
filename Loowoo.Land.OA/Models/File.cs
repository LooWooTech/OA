using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Land.OA.Models
{
    [Table("file")]
    public class File
    {
        public File()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string FileName { get; set; }

        [NotMapped]
        public string FileExt
        {
            get { return System.IO.Path.GetExtension(FileName); }
        }

        public bool IsWordFile
        {
            get
            {
                if (string.IsNullOrEmpty(FileExt)) return false;
                return FileExt.EndsWith("doc") || FileExt.EndsWith("docx");
            }
        }

        public string SavePath { get; set; }
        /// <summary>
        /// 最小单位
        /// </summary>
        public long Size { get; set; }

        [NotMapped]
        public string DisplaySize
        {
            get
            {
                if (Size == 0) return "";
                var k = 1024;
                var arr = new[] { "B", "K", "M", "G", "P", "B" };
                var i = (int)Math.Log(Size, k);
                var size = Size / Math.Pow(k, i);
                return (int)size + arr[i];

            }
        }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int InfoId { get; set; }

        public int ParentId { get; set; }

        public bool Inline { get; set; }

        private static string _uploadDir = AppSettings.Get("UploadPath") ?? "upload_files/";
        /// <summary>
        /// 站点绝对路径
        /// </summary>
        [NotMapped]
        public string AbsolutelyPath
        {
            get { return GetAbsolutelyPath(SavePath); }
        }
        /// <summary>
        /// 物理路径
        /// </summary>
        [NotMapped]
        public string PhysicalPath
        {
            get { return GetPhysicalSavePath(SavePath); }
        }

        [NotMapped]
        public string ContentType
        {
            get
            {
                if (!string.IsNullOrEmpty(FileExt))
                {
                    var ext = FileExt.Substring(1).ToLower();
                    foreach (var t in ContentTypes.Replace("\r", "").Split('\n'))
                    {
                        var kv = t.Split(',');
                        if (kv[0] == ext)
                        {
                            return kv[1].Trim(' ');
                        }
                    }
                }
                return "application/octet-stream";
            }
        }

        public static string GetPhysicalSavePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;
            return System.IO.Path.Combine(Environment.CurrentDirectory, GetAbsolutelyPath(fileName));
        }

        public static string GetAbsolutelyPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;
            return System.IO.Path.Combine(_uploadDir, fileName);
        }

        public static File Upload(HttpPostedFile file)
        {
            var dir = System.IO.Path.Combine(Environment.CurrentDirectory, _uploadDir);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            var fileExt = System.IO.Path.GetExtension(file.FileName);
            var fileName = DateTime.Now.Ticks + file.ContentLength + fileExt;
            file.SaveAs(GetPhysicalSavePath(fileName));
            return new File
            {
                FileName = file.FileName,
                Size = file.ContentLength,
                SavePath = fileName
            };
        }

        #region content types
        private static readonly string ContentTypes = @"ez,application/andrew-inset
hqx,application/mac-binhex40
cpt,application/mac-compactpro
doc,application/msword
class,application/octet-stream
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
        #endregion
    }

    public enum FileType
    {
        [Description("文档")]
        Document,
        [Description("图片")]
        Image,
        [Description("视频")]
        Video,
        [Description("其他")]
        Other
    }
}
