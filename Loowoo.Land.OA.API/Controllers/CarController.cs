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
                UserId = 0,
                Page = new PageParameter(page, rows)
            };
            return Core.CarManager.GetApplies(parameter).Select(e => new
            {
                RealName = e.User.RealName,
                e.Car,
                e.CreateTime,
                e.ScheduleBeginTime,
                e.ScheduleEndTime,
                e.RealEndTime,
                e.Reason,
                e.Result,
                e.UpdateTime
            });
        }

        [HttpPost]
        public void Apply([FromBody]CarApply data, int toUserId)
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
            var hasApplied = Core.FormInfoManager.HasApplied(CurrentUser.ID, car.UpdateTime, car.ID);
            if (hasApplied)
            {
                throw new Exception("你已经申请过了，不要重复申请");
            }

            //Core.FormInfoManager.Save(data);

            //var apply = data.Json.ToObject<CarApply>();

            //Core.FlowDataManager.Submit(data.ID, CurrentUser.ID, toUserId, true, apply.Reason);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.CarManager.Delete(id);
        }
    }
}