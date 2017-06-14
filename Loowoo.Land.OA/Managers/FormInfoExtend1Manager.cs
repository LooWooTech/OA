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
    public class FormInfoExtend1Manager : ManagerBase
    {
        /// <summary>
        /// 获取申请记录
        /// </summary>
        public IEnumerable<FormInfoExtend1> GetList(Extend1Parameter parameter)
        {
            var query = DB.FormInfoApplies.AsQueryable();
            if (parameter.InfoId > 0)
            {
                query = query.Where(e => e.InfoId == parameter.InfoId);
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime > parameter.BeginTime.Value);
            }
            if (parameter.Result.HasValue)
            {
                query = query.Where(e => e.Result == parameter.Result.Value);
            }
            switch (parameter.Status)
            {
                case CheckStatus.All:
                    break;
                case CheckStatus.Uncheck:
                    query = query.Where(e => !e.Result.HasValue);
                    break;
                case CheckStatus.Checked:
                    query = query.Where(e => e.Result.HasValue);
                    break;
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public FormInfoExtend1 Get(int id)
        {
            return DB.FormInfoApplies.FirstOrDefault(e => e.ID == id);
        }

        public void Save(FormInfoExtend1 data)
        {
            DB.FormInfoApplies.AddOrUpdate(data);
            DB.SaveChanges();
        }

        public bool HasApply(FormInfoExtend1 data)
        {
            return DB.FormInfoApplies.Any(e => e.InfoId == data.InfoId
            && e.Result == null
            && e.UserId == data.UserId
            );
        }

        public void Apply(FormInfo info, FormInfoExtend1 data)
        {
            if(info.ID == 0)
            {
                throw new Exception("请先保存formInfo");
            }
            data.ID = info.ID;
            Save(data);
            //创建流程
            var flowData = Core.FlowDataManager.CreateFlowData(info);

            var nodeData = flowData.GetFirstNodeData();
            var nextNodeData = Core.FlowDataManager.SubmitToUser(info.FlowData, data.ApprovalUserId);

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = data.ID,
                UserId = data.UserId,
                Status = FlowStatus.Done,
                FormId = info.FormId,
                FlowNodeDataId = nodeData.ID
            });

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                FlowNodeDataId = nextNodeData.ID,
                FormId = info.FormId,
                InfoId = data.ID,
                UserId = data.ApprovalUserId,
                Status = FlowStatus.Doing,
            });
        }
    }
}
