using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class FreeFlowDataManager : ManagerBase
    {
        public void Add(FreeFlowNodeData model)
        {
            var user = Core.UserManager.GetModel(model.UserId);
            model.Signature = user.RealName;
            model.DepartmentName = user.Department.Name;

            DB.FreeFlowNodeDatas.Add(model);
            DB.SaveChanges();
        }

        public FreeFlowNodeData Update(FreeFlowNodeData model)
        {
            if (model.ID == 0)
            {
                var flowNodeData = Core.FlowNodeDataManager.GetModel(model.FlowNodeDataId);
                if (flowNodeData == null)
                {
                    throw new Exception("参数不正确");
                }
                //第一次提交
                if (flowNodeData.Nodes.Count == 0)
                {
                    Add(model);
                    Update(model);
                    return model;
                }
                else
                {
                    throw new Exception("流程数据异常，请联系管理员");
                }
            }
            var entity = DB.FreeFlowNodeDatas.FirstOrDefault(e => e.ID == model.ID);

            entity.Content = model.Content;
            entity.UpdateTime = DateTime.Now;
            DB.SaveChanges();
            return entity;
        }

        public FreeFlowNodeData GetModel(int parentId)
        {
            return DB.FreeFlowNodeDatas.FirstOrDefault(e => e.ID == parentId);
        }
    }
}
