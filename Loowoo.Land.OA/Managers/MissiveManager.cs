using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class MissiveManager : ManagerBase
    {
        public IEnumerable<Missive> GetList(FormInfoParameter parameter)
        {
            var query = DB.Missives.AsQueryable();
            parameter.InfoIds = Core.UserFormInfoManager.GetUserInfoIds(parameter);
            query = query.Where(e => parameter.InfoIds.Contains(e.ID));
            return query.OrderByDescending(e => e.ID);
        }

        public void Save(Missive data)
        {
            var entity = DB.Missives.FirstOrDefault(e => e.ID == data.ID);
            if (entity == null)
            {
                DB.Missives.Add(data);
            }
            else
            {
                DB.Entry(entity).CurrentValues.SetValues(data);
            }
            DB.SaveChanges();
        }

        public Missive GetModel(int id)
        {
            return DB.Missives.FirstOrDefault(e => e.ID == id);
        }

        public MissiveRedTitle GetRedTitle(int id)
        {
            return DB.MissiveRedTitles.Find(id);
        }

        public IEnumerable<MissiveRedTitle> GetRedTitles()
        {
            return DB.MissiveRedTitles;
        }

        public void SaveRedTitle(MissiveRedTitle model)
        {
            if (DB.MissiveRedTitles.Any(e => e.Name == model.Name && e.ID != model.ID))
            {
                throw new Exception("该文件字已经添加过");
            }
            DB.MissiveRedTitles.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public void AddRedTitle(Missive model, MissiveRedTitle redTitle)
        {
            if (redTitle.TemplateId == 0 || redTitle.Template == null)
            {
                return;
            }
            if (model.ContentId > 0 && model.Content == null)
            {
                model.Content = Core.FileManager.GetModel(model.ContentId);
            }
            var fileDoc = WordHelper.CreateDoc(model.Content.PhysicalPath);
            var redTitleDoc = WordHelper.CreateDoc(redTitle.Template.PhysicalPath);
            var doc = new XWPFDocument(redTitleDoc.Package);
            doc.CopyElements(fileDoc);
            doc.ReplaceContent("{文件字号}", model.WJ_ZH);
            doc.SaveAs(model.Content.AbsolutelyPath);
        }

        public void AddMissiveServiceLog(int id)
        {
            var missive = GetModel(id);
            var formType = missive.Info.Form.FormType;
            if (formType != FormType.SendMissive)
            {
                throw new Exception("无法上报该件");
            }

            missive.NotReport = false;

            var log = DB.MissiveServiceLogs.FirstOrDefault(e => e.MissiveId == id);
            if (log != null)
            {
                //如果之前上报失败，则重新上报
                if (log.Result == false)
                {
                    log.Result = null;
                }
            }
            else
            {
                log = new MissiveServiceLog
                {
                    MissiveId = id,
                    Type = formType,
                };

                DB.MissiveServiceLogs.Add(log);
            }
            DB.SaveChanges();
        }

        public void UpdateMissiveServiceLog(MissiveServiceLog model)
        {
            var entity = DB.MissiveServiceLogs.FirstOrDefault(e=>e.ID == model.ID);
            entity.Result = model.Result;
            entity.UpdateTime = DateTime.Now;
            DB.SaveChanges();
        }

        public MissiveServiceLog GetLastNeedReportMissiveServiceLog()
        {
            return DB.MissiveServiceLogs.Where(e => e.Result == null && e.Type == FormType.SendMissive).OrderByDescending(e => e.ID).FirstOrDefault();
        }


    }
}
