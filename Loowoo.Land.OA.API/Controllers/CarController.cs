using Loowoo.Common;
using Loowoo.Land.OA.Models;
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
            return Core.CarManager.GetList();
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


        [HttpPost]
        public void Apply(int carId, [FromBody]FormInfo data)
        {
            var car = Core.CarManager.Get(carId);
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

            data.PostUserId = CurrentUser.ID;
            Core.FormInfoManager.Save(data);
            var form = Core.FormManager.GetModel(FormType.Car);
            Core.FlowDataManager.Create(form.FLowId, data);

        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.CarManager.Delete(id);
        }
    }
}