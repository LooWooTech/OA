using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class GoodsManager : ManagerBase
    {
        public GoodsApply GetApply(int applyId)
        {
            return DB.GoodsApplies.FirstOrDefault(e => e.ID == applyId);
        }

        public Goods GetModel(int id)
        {
            return DB.Goods.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Goods> GetList(GoodsParameter parameter)
        {
            var query = DB.Goods.AsQueryable();
            if (parameter.CategoryId > 0)
            {
                query = query.Where(e => e.CategoryId == parameter.CategoryId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Name.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Delete(int id)
        {
            var model = GetModel(id);
            DB.Goods.Remove(model);
            DB.SaveChanges();
        }

        public void Save(Goods model)
        {
            if (model.ID > 0)
            {
                var entity = DB.Goods.FirstOrDefault(e => e.ID == model.ID);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.PictureId = model.PictureId;
                entity.Status = model.Status;
            }
            else
            {
                DB.Goods.Add(model);
            }
            DB.SaveChanges();
        }

        public IEnumerable<GoodsApply> GetApplyList(GoodsApplyParameter parameter)
        {
            var query = DB.GoodsApplies.Where(e => !e.Info.Deleted);
            if (parameter.ApplyUserId > 0)
            {
                query = query.Where(e => e.ApplyUserId == parameter.ApplyUserId);
            }
            if (parameter.ApprovalUserId > 0)
            {
                query = query.Where(e => e.ApprovalUserId == parameter.ApprovalUserId);
            }
            if (parameter.GoodsId > 0)
            {
                query = query.Where(e => e.GoodsId == parameter.GoodsId);
            }
            if(parameter.Status.HasValue)
            {
                if(parameter.Status == CheckStatus.Checked)
                {
                    query = query.Where(e => e.Result.HasValue);
                }
                else
                {
                    query = query.Where(e => !e.Result.HasValue);
                }
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void SaveRegister(GoodsRegister model)
        {
            DB.GoodsRegisters.AddOrUpdate(model);
            var goods = GetModel(model.GoodsId);
            goods.Number += model.Number;
            DB.SaveChanges();
        }

        public GoodsApply GetLastApply(int goodsId, int userId)
        {
            return DB.GoodsApplies.Where(e => e.GoodsId == goodsId && e.ApplyUserId == userId).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public void SaveApply(GoodsApply model)
        {
            DB.GoodsApplies.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public void Approval(GoodsApply apply)
        {
            var info = Core.FormInfoManager.GetModel(apply.ID);
            var flowNodeData = info.FlowData.GetUserLastNodeData(apply.ApprovalUserId);
            flowNodeData.Result = apply.Result;
            Core.FlowNodeDataManager.Submit(flowNodeData);
            Core.FlowDataManager.Complete(info);
            if (apply.Result == false)
            {
                var goods = GetModel(apply.GoodsId);
                goods.Number += apply.Number;
                DB.SaveChanges();
            }
        }

        public void CancelApply(GoodsApply model)
        {
            var goods = GetModel(model.GoodsId);
            goods.Number += model.Number;

            var info = DB.FormInfos.FirstOrDefault(e => e.ID == model.ID);
            info.Deleted = true;
            info.FlowData.Completed = true;

            DB.SaveChanges();
        }
    }
}
