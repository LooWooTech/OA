using Loowoo.Common;
using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class CarController : ControllerBase
    {
        [HttpGet]
        public object List()
        {
            var list = Core.CarManager.GetList();
            return list.ToList().Select(e => new
            {
                e.Name,
                e.Number,
                e.PhotoId,
                e.Status,
                e.ID,
            });
        }

        [HttpPost]
        public void Save([FromBody]Car data)
        {
            var model = Core.CarManager.Get(data.ID) ?? data;
            model.Name = data.Name;
            model.Number = data.Number;
            model.Type = data.Type;

            Core.CarManager.Save(model);
        }

        [HttpGet]
        public object CarApplies(int carId = 0, int userId = 0, int page = 1, int rows = 20)
        {
            var parameter = new CarApplyParameter
            {
                CarId = carId,
                UserId = userId,
                Page = new PageParameter(page, rows)
            };
            return new PagingResult
            {
                List = Core.CarManager.GetApplies(parameter).Select(e => new
                {
                    e.ID,
                    e.UserId,
                    ApplyUser = e.User.RealName,
                    e.Car,
                    e.CreateTime,
                    e.ScheduleBeginTime,
                    e.ScheduleEndTime,
                    e.RealEndTime,
                    e.Reason,
                    e.Result,
                    e.UpdateTime,
                    e.ApprovalUserId,
                    ApprovalUser = e.ApprovalUser.RealName
                }),
                Page = parameter.Page,
            };
        }

        [HttpPost]
        public void Apply([FromBody]CarApply data)
        {
            var car = Core.CarManager.Get(data.CarId);
            if (car == null)
            {
                throw new ArgumentException("参数不正确，没有找该车");
            }
            if (car.Status != CarStatus.Unused)
            {
                throw new Exception("当前车辆在使用中，无法申请");
            }
            if (data.ApprovalUserId == 0)
            {
                throw new Exception("没有选择审批人");
            }
            data.UserId = CurrentUser.ID;
            if (Core.CarManager.HasApply(data))
            {
                throw new Exception("你已经申请过该车辆，还未通过审批");
            }
            Core.CarManager.Apply(data);
            Core.FeedManager.Save(new Feed
            {
                Action = UserAction.Apply,
                Title = CurrentUser.RealName + "申请用车：" + data.Car.Name + "（" + car.Number + "）",
                InfoId = data.ID,
                Type = FeedType.Info,
                ToUserId = data.ApprovalUserId,
                FromUserId = CurrentUser.ID,
            });
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.CarManager.Delete(id);
        }

        [HttpGet]
        public void Approval(int infoId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            //如果流程审批完成
            if (info.FlowData.Completed)
            {
                var model = Core.CarManager.GetCarApply(infoId);
                model.Car.Status = CarStatus.Using;
                model.Result = info.FlowData.GetLastNodeData().Result.Value;

                Core.CarManager.SaveApply(model);
            }

        }

        [HttpGet]
        public void Back(int infoId)
        {
            var model = Core.CarManager.GetCarApply(infoId);
            if (model == null)
            {
                throw new Exception("参数错误");
            }
            if (model.RealEndTime.HasValue)
            {
                throw new Exception("车辆已归还，归还日期：" + model.RealEndTime.Value.ToShortDateString());
            }
            if (model.UserId == CurrentUser.ID)
            {
                model.RealEndTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;

                Core.CarManager.SaveApply(model);
            }
            else
            {
                throw new Exception("你不能归还该车辆");
            }
        }
    }
}