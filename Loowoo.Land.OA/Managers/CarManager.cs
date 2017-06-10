using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class CarManager : ManagerBase
    {
        public IEnumerable<Car> GetList()
        {
            return DB.Cars.Where(e => !e.Deleted);
        }

        public void Save(Car model)
        {
            model.Number = model.Number.ToUpper();
            if (DB.Cars.Any(e => e.Number == model.Number && (e.ID == 0 || e.ID != model.ID)))
            {
                throw new Exception("该车牌号已被使用");
            }

            DB.Cars.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public Car Get(int id)
        {
            return DB.Cars.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            DB.SaveChanges();
        }

        /// <summary>
        /// 获取申请记录
        /// </summary>
        public IEnumerable<CarApply> GetApplies(CarApplyParameter parameter)
        {
            var query = DB.CarApplies.AsQueryable();
            if (parameter.CarId > 0)
            {
                query = query.Where(e => e.CarId == parameter.CarId);
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
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public CarApply GetCarApply(int id)
        {
            return DB.CarApplies.FirstOrDefault(e => e.ID == id);
        }

        public void SaveApply(CarApply data)
        {
            var entity = DB.CarApplies.FirstOrDefault(e => e.CarId == data.CarId
           && e.Result == null
           && e.UserId == data.UserId
            );
            if (entity == null)
            {
                DB.CarApplies.Add(data);
            }
        }

        public void Apply(CarApply data)
        {
            var car = Get(data.CarId);
            var info = new FormInfo
            {
                ExtendId = data.CarId,
                Title = "申请用车：" + car.Name + "（" + car.Number + "）",
                FormId = (int)FormType.Car,
                PostUserId = data.UserId,
            };
            info.Form = Core.FormManager.GetModel(FormType.Car);
            Core.FormInfoManager.Save(info);
            data.ID = info.ID;
            SaveApply(data);
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