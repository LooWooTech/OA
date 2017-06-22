using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using NPOI.OpenXml4Net.OPC;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FileManager : ManagerBase
    {
        private string _uploadDir = AppSettings.Get("UploadPath") ?? "upload_files";

        public void Save(File file)
        {
            if (file.ID > 0)
            {
                file.UpdateTime = DateTime.Now;
            }
            DB.Files.AddOrUpdate(file);
            DB.SaveChanges();
        }

        public File GetModel(int id)
        {
            return DB.Files.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            var entity = DB.Files.FirstOrDefault(e => e.ID == id);
            if (entity != null)
            {
                DB.Files.Remove(entity);
                var children = DB.Files.Where(e => e.ParentId == id);
                DB.Files.RemoveRange(children);
                DB.SaveChanges();
            }
        }

        public void Relation(int[] fileIds, int infoId)
        {
            var entities = DB.Files.Where(e => fileIds.Contains(e.ID));
            foreach (var entity in entities)
            {
                entity.InfoId = infoId;
            }
            DB.SaveChanges();
        }

        public IEnumerable<File> GetList(FileParameter parameter)
        {
            var query = DB.Files.AsQueryable();
            if (parameter.InfoId.HasValue)
            {
                query = query.Where(e => e.InfoId == parameter.InfoId.Value);
            }
            if (parameter.ParentId.HasValue)
            {
                query = query.Where(e => e.ParentId == parameter.ParentId);
            }
            if (parameter.Inline.HasValue)
            {
                query = query.Where(e => e.Inline == parameter.Inline.Value);
            }
            if (parameter.Type.HasValue)
            {
                string[] fileExt = null;
                switch (parameter.Type.Value)
                {

                }
                query = query.Where(e => e.FileName.EndsWith(parameter.Type.Value.ToString()));
            }
            query = query.OrderBy(e => e.UpdateTime).SetPage(parameter.Page);
            return query;
        }

        public bool TryConvertToPdf(string docPath, string pdfPath)
        {
            try
            {
                var doc = new Aspose.Words.Document(docPath);
                doc.Save(pdfPath, new Aspose.Words.Saving.PdfSaveOptions
                {
                    JpegQuality = 100,
                    UseHighQualityRendering = true,
                    ZoomBehavior = Aspose.Words.Saving.PdfZoomBehavior.FitWidth,
                    SaveFormat = Aspose.Words.SaveFormat.Pdf
                });
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteLog(ex.ToJson(), "ex");
                return false;
            }
            return true;
        }

        public void AddRedTitle(int contentId, MissiveRedTitle redTitle)
        {
            var file = Core.FileManager.GetModel(contentId);
            var fileDoc = WordHelper.CreateDoc(file.PhysicalSavePath);
            var redTitleDoc = WordHelper.CreateDoc(redTitle.Template.PhysicalSavePath);
            var doc = new XWPFDocument(redTitleDoc.Package);
            doc.CopyElements(fileDoc);
            doc.SaveAs(file.ServerSavePath);
        }
    }
}