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
        public IEnumerable<UserFormExtend1> GetList(Extend1Parameter parameter)
        {
            var query = Core.UserFormInfoManager.GetUserInfoList<UserFormExtend1>(parameter);
            if (parameter.ExtendInfoId > 0)
            {
                query = query.Where(e => e.ExtendInfoId == parameter.ExtendInfoId);
            }
            if (parameter.Result.HasValue)
            {
                query = query.Where(e => e.Result == parameter.Result);
            }
            if (parameter.ApprovalUserId > 0)
            {
                query = query.Where(e => e.ApprovalUserId == parameter.ApprovalUserId);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public FormInfoExtend1 GetModel(int id)
        {
            return DB.FormInfoExtend1s.FirstOrDefault(e => e.ID == id);
        }

        public void Save(FormInfoExtend1 data)
        {
            DB.FormInfoExtend1s.AddOrUpdate(data);
            DB.SaveChanges();
        }

        public bool HasApply(FormInfoExtend1 data)
        {
            return DB.FormInfoExtend1s.Any(e => e.ExtendInfoId == data.ExtendInfoId
            && e.Result == null
            && e.UserId == data.UserId
            );
        }

        public void Apply(FormInfo info, FormInfoExtend1 data)
        {
            if (info.ID == 0)
            {
                throw new Exception("请先保存formInfo");
            }
            data.ID = info.ID;
            Save(data);
            //创建流程
            var flowData = Core.FlowDataManager.CreateFlowData(info);

            var nodeData = flowData.GetFirstNodeData();
            Core.FlowNodeDataManager.Submit(nodeData);

            var nextNodeData = Core.FlowNodeDataManager.CreateNextNodeData(flowData, data.ApprovalUserId);

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = data.ID,
                UserId = data.UserId,
                FlowStatus = FlowStatus.Done,
            });

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = data.ID,
                UserId = data.ApprovalUserId,
                FlowStatus = FlowStatus.Doing,
            });
        }
    }
}
