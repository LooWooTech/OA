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
                    RealName = e.User.RealName,
                    e.Car,
                    e.CreateTime,
                    e.ScheduleBeginTime,
                    e.ScheduleEndTime,
                    e.RealEndTime,
                    e.Reason,
                    e.Result,
                    e.UpdateTime
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

            Core.CarManager.Apply(data);
            Core.FeedManager.Save(new Feed
            {
                Action = UserAction.Apply,
                Title = CurrentUser.RealName + "申请用车：" + data.Car.Name + "（" + data.Car.Number + "）",
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
    }
}