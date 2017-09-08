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
            var query = DB.UserFormInfos.AsQueryable();
            if (parameter.ApprovalUserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.ApprovalUserId);
                switch (parameter.Status)
                {
                    case CheckStatus.All:
                        break;
                    case CheckStatus.Uncheck:
                        query = query.Where(e => e.Status == FlowStatus.Doing);
                        break;
                    case CheckStatus.Checked:
                        query = query.Where(e => e.Status != FlowStatus.Doing);
                        break;
                }
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime > parameter.BeginTime);
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.Info.PostUserId == parameter.UserId);

            }

            var formInfoIds = query.GroupBy(e => e.InfoId).Select(g => g.Key).OrderByDescending(id => id).SetPage(parameter.Page).ToArray();

            var list = DB.FormInfoExtend1s.Where(e => formInfoIds.Contains(e.ID));
            if (parameter.InfoId > 0)
            {
                list = list.Where(e => e.InfoId == parameter.InfoId);
            }
            if (parameter.UserId > 0)
            {
                list = list.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.Result.HasValue)
            {
                list = list.Where(e => e.Result == parameter.Result.Value);
            }
            if (parameter.Category.HasValue)
            {
                list = list.Where(e => e.Category == parameter.Category.Value);
            }
            if (parameter.UserId > 0)
            {
                switch (parameter.Status)
                {
                    case CheckStatus.All:
                        break;
                    case CheckStatus.Uncheck:
                        list = list.Where(e => e.Result == null);
                        break;
                    case CheckStatus.Checked:
                        list = list.Where(e => e.Result.HasValue);
                        break;
                }
            }
            return list.OrderByDescending(e => e.ID).SetPage(parameter.Page);
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
            return DB.FormInfoExtend1s.Any(e => e.InfoId == data.InfoId
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
                Status = FlowStatus.Done,
            });

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = data.ID,
                UserId = data.ApprovalUserId,
                Status = FlowStatus.Doing,
            });
        }
    }
}
