using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class UserFormInfoManager : ManagerBase
    {
        public IQueryable<UserFormInfo> GetList(FormInfoParameter parameter)
        {
            var query = DB.UserFormInfos.Where(e => !e.Info.Deleted);
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.PostUserId > 0)
            {
                query = query.Where(e => e.Info.PostUserId == parameter.PostUserId);
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.FormId == parameter.FormId);
            }
            if (parameter.CategoryId > 0)
            {
                query = query.Where(e => e.Info.CategoryId == parameter.CategoryId);
            }
            if (parameter.Status.HasValue)
            {
                query = query.Where(e => e.Status == parameter.Status.Value);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime <= parameter.EndTime.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.Info.FlowData != null && e.Info.FlowData.Completed == parameter.Completed.Value);
            }

            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public int[] GetUserIds(int infoId)
        {
            return DB.UserFormInfos.Where(e => e.InfoId == infoId && !e.Info.Deleted).Select(e => e.UserId).ToArray();
        }

        public UserFormInfo GetModel(int infoId, int userId)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId && !e.Info.Deleted);
        }

        public void Delete(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId && e.FormId == model.FormId);
            DB.UserFormInfos.Remove(entity);
            DB.SaveChanges();
        }

        //public void UpdateStatues(int infoId, int userId, FlowStatus status)
        //{
        //    var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId);
        //    entity.Status = status;
        //    DB.SaveChanges();
        //}

        //public void Add(UserFormInfo model)
        //{
        //    var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId);
        //    if (entity != null)
        //    {
        //        throw new Exception("记录已存在");
        //    }
        //    DB.UserFormInfos.Add(model);
        //    DB.SaveChanges();
        //}

        public void Save(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId && e.FormId == model.FormId);
            if (entity != null)
            {
                entity.Status = model.Status;
            }
            else
            {
                DB.UserFormInfos.Add(model);
            }
            DB.SaveChanges();
        }

        public void OnCancelFlowData(FormInfo info, FlowNodeData nodeData)
        {
            info.FlowStep = nodeData.FlowNodeName;

            Delete(new UserFormInfo
            {
                InfoId = info.ID,
                FormId = info.FormId,
                UserId = nodeData.UserId
            });

            Save(new UserFormInfo
            {
                InfoId = info.ID,
                FormId = info.FormId,
                Status = FlowStatus.Doing,
                UserId = nodeData.UserId,
                FlowNodeDataId = nodeData.ID
            });
        }

    }
}
