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
        protected ManagerCore Core = ManagerCore.Instance;

        protected UserIdentity CurrentUser
        {
            get
            {
                return (UserIdentity)Thread.CurrentPrincipal.Identity;
            }
        }

        public ActionResult Link(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Get(int id)
        {
            var file = Core.FileManager.GetModel(id);
            if (file == null) throw new Exception("文件未找到");

            //如果是word文档，则需要转为pdf 并替换原来的word文件
            if (file.IsWordFile)
            {
                var page = new Page();
                string controlOutput = string.Empty;
                PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl
                {
                    SaveFilePage = "/Word/SaveDoc?id=" + id,
                    ServerPage = "/pageoffice/server.aspx",
                    Menubar = false,
                    CustomToolbar = false,
                    Caption = file.FileName,

                };
                pc.WebOpen(file.PhysicalSavePath, PageOffice.OpenModeType.docAdmin, CurrentUser != null ? CurrentUser.RealName : "未知");
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
                Response.Expires = -1;
                return File(file.PhysicalSavePath, file.ContentType);
            }
        }

        public ActionResult SaveDoc(int id)
        {
            var file = Core.FileManager.GetModel(id);
            if (file == null)
            {
                throw new Exception("文件未找到");
            }
            var fs = new PageOffice.FileSaver();
            fs.SaveToFile(file.ServerSavePath);
            fs.Close();
            return View();
        }

    }
}