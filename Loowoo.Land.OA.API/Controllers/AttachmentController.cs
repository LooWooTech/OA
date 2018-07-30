using Loowoo.Common;
using Loowoo.Land.OA.API.Security;
using Loowoo.Land.OA.Managers;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI;

namespace Loowoo.Land.OA.API.Controllers
{

    public class AttachmentController : Controller
    {
        protected ManagerCore Core => ManagerCore.Instance;

        protected UserIdentity Identity => (UserIdentity)Thread.CurrentPrincipal.Identity;

        public ActionResult Download(int id)
        {
            var file = Core.FileManager.GetModel(id);
            try
            {
                return File(file.PhysicalPath, file.ContentType);
            }
            catch (FileNotFoundException)
            {
                return Content("文件未找到");
            }
            catch
            {
                return Content("下载文件出现异常，请联系管理员");
            }
        }

        public ActionResult Preview(int id)
        {
            var missive = Core.MissiveManager.GetModel(id);
            var file = Core.FileManager.GetModel(missive.ContentId);
            if (file == null) throw new Exception("文件未找到");
            if (file.IsWordFile)
            {
                var canEdit = missive.Info.FlowData.GetLastNodeData()?.UserId == Identity.ID;
                var url = $"PageOffice://|{Request.Url.Scheme}://{Request.Url.Host}:{Request.Url.Port}{Url.Action("Doc", new { file.ID, edit = canEdit })}";
                return Redirect(url);
            }
            return File(file.PhysicalPath, file.ContentType);
        }

        public ActionResult Doc(int id, bool edit = false)
        {
            var file = Core.FileManager.GetModel(id);
            if (file == null) throw new Exception("文件未找到");

            //如果是word文档，则需要转为pdf 并替换原来的word文件
            if (!file.IsWordFile) throw new Exception("该文件不是Word文件");
            var page = new Page();
            string controlOutput = string.Empty;
            PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl
            {
                SaveFilePage = Url.Action("Savedoc", new { id }),
                ServerPage = "/pageoffice/server.aspx",
                Menubar = false,
                CustomToolbar = false,
                Caption = file.FileName,

            };

            pc.WebOpen(file.PhysicalPath, edit ? PageOffice.OpenModeType.docNormalEdit : PageOffice.OpenModeType.docReadOnly, Identity != null ? Identity.Name : "未知");
            page.Controls.Add(pc);
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Server.Execute(page, htw, false);
                    controlOutput = sb.ToString();
                }
            }
            ViewBag.EditorHtml = controlOutput;
            return View();
        }

        public ActionResult SaveDoc(int id)
        {
            var file = Core.FileManager.GetModel(id);
            if (file == null)
            {
                throw new Exception("文件未找到");
            }
            var fs = new PageOffice.FileSaver();
            fs.SaveToFile(file.AbsolutelyPath);
            fs.Close();
            return View();
        }

    }
}