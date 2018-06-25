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
        public IQueryable<UserInfo> GetUserInfoList(FormInfoParameter parameter)
        {
            return GetUserInfoList<UserInfo>(parameter).OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public IQueryable<T> GetUserInfoList<T>(FormInfoParameter parameter) where T : UserInfo
        {
            var query = DB.Set<T>().AsQueryable();
            if (parameter.PostUserId > 0)
            {
                query = query.Where(e => e.PostUserId == parameter.PostUserId);
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.FormId == parameter.FormId);
            }
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                query = query.Where(e => e.Title.Contains(parameter.SearchKey));
            }
            if (parameter.FlowStatus != null && parameter.FlowStatus.Length > 0)
            {
                query = query.Where(e => parameter.FlowStatus.Contains(e.FlowStatus));
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.CreateTime <= parameter.EndTime.Value);
            }
            if (parameter.Starred.HasValue)
            {
                query = query.Where(e => e.Starred == parameter.Starred.Value);
            }
            if (parameter.Trash.HasValue)
            {
                query = query.Where(e => e.Trash == parameter.Trash.Value);
            }
            if (parameter.Read.HasValue)
            {
                query = query.Where(e => e.Read == parameter.Read.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.FlowData != null && e.FlowData.Completed == parameter.Completed.Value);
            }
            if (parameter.UserId > 0)
            {
                return query.Where(e => e.UserId == parameter.UserId);
            }
            return query;
        }

        public bool HasRight(int infoId, int userId)
        {
            return DB.UserFormInfos.Any(e => e.InfoId == infoId && e.UserId == userId && !e.Info.Deleted);
        }

        public int[] GetUserIds(int infoId)
        {
            return DB.UserFormInfos.Where(e => e.InfoId == infoId && !e.Info.Deleted).Select(e => e.UserId).ToArray();
        }

        public UserFormInfo GetModel(int infoId, int userId)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId && !e.Info.Deleted);
        }

        public void Remove(int infoId, int userId)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId);
            DB.UserFormInfos.Remove(entity);
            DB.SaveChanges();
        }

        public void Save(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId);
            if (entity != null)
            {
                entity.FlowStatus = model.FlowStatus;
            }
            else
            {
                DB.UserFormInfos.Add(model);
            }
            DB.SaveChanges();
        }

        public void UpdateStar(int id, int userId, bool isStar = true)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Starred = isStar;
                DB.SaveChanges();
            }
        }

        public void UpdateTrash(int id, int userId, bool deleted = true)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Trash = deleted;
                DB.SaveChanges();
            }
        }

        public void UpdateFlowStatusOnCompleteFreeFlow(int infoId, FlowData flowData, FreeFlowData freeflowData)
        {
            //当结束自由转发之后，如果该用户不在主流程，那么则待办箱的件将转移到已办件
            var mainFlowUserIds = flowData.Nodes.Where(e => e.Result == null).Select(e => e.UserId).ToArray();
            var freeFlowUserIds = freeflowData.Nodes.Select(e => e.UserId).ToArray();
            var needUpdateStatusUserId = freeFlowUserIds.Where(id => !mainFlowUserIds.Contains(id)).ToArray();
            if (needUpdateStatusUserId.Length > 0)
            {
                var list = DB.UserFormInfos.Where(e => e.InfoId == infoId && needUpdateStatusUserId.Contains(e.UserId));
                foreach (var item in list)
                {
                    if (item.FlowStatus == FlowStatus.Doing)
                    {
                        item.FlowStatus = FlowStatus.Done;
                    }
                }
                DB.SaveChanges();
            }
        }

        public void Read(int id, int userId)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Read = true;
                DB.SaveChanges();
            }
        }

        public void ReadAll(int userId)
        {
            var list = DB.UserFormInfos.Where(e => e.UserId == userId);
            foreach (var item in list)
            {
                item.Read = true;
            }
            DB.SaveChanges();
        }

        public void Delete(int id, int userId)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Deleted = true;
                DB.SaveChanges();
            }
        }

        public UserFormInfo GetModel(int id)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.ID == id);
        }
    }
}
