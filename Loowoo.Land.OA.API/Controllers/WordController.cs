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

    public class WordController : Controller
    {
        private string _uploadDir = AppSettings.Get("UploadPath") ?? "/upload_files/";

        protected ManagerCore Core = ManagerCore.Instance;

        protected UserIdentity CurrentUser
        {
            get
            {
                return (UserIdentity)Thread.CurrentPrincipal.Identity;
            }
        }

        public ActionResult Get(int id)
        {
            var file = Core.FileManager.GetModel(id);
            if (file == null) throw new Exception("文件未找到");
            var fileExt = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadDir, file.SavePath);
            //如果是word文档，则需要转为pdf 并替换原来的word文件
            if (fileExt.EndsWith("doc") || fileExt.EndsWith("docx"))
            {
                Session["CurrentWordFilePath"] = filePath;
                var page = new Page();
                string controlOutput = string.Empty;
                PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl
                {
                    SaveFilePage = "/Word/SaveDoc",
                    ServerPage = "/pageoffice/server.aspx",
                    Menubar = false,
                    CustomToolbar = false,
                    Caption = file.FileName
                };
                pc.WebOpen(filePath, PageOffice.OpenModeType.docAdmin, CurrentUser != null ? CurrentUser.RealName : "未知");
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
            else
            {
                return File(filePath, FileController.GetContentType(file.FileName));
            }
        }

        public ActionResult SaveDoc()
        {
            var filePath = Session["CurrentWordFilePath"] as string;
            if (filePath != null)
            {
                PageOffice.FileSaver fs = new PageOffice.FileSaver();
                filePath = Server.MapPath(filePath);
                fs.SaveToFile(filePath);
                fs.Close();
            }

            return View();
        }

    }
}