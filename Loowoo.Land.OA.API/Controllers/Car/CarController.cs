using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    /// <summary>
    /// 车辆
    /// </summary>
    public class CarController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存创建车辆
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:21:48
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Car car)
        {
            TaskName = "添加车辆";
            if (car == null || string.IsNullOrEmpty(car.Number) || string.IsNullOrEmpty(car.Name))
            {
                return BadRequest($"{TaskName}:未获取车辆相关信息，请核对车辆名称、车牌");
            }
            try
            {
                var id = Core.CarManager.Save(car);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存车辆失败");
                }
                return Ok();

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}:发生错误");
        }

        /// <summary>
        /// 作用：编辑车辆
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:39:45
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] Car car)
        {
            TaskName = "车辆编辑";
            if (car == null || car.ID == 0 || string.IsNullOrEmpty(car.Name) || string.IsNullOrEmpty(car.Number))
            {
                return BadRequest($"{TaskName}:未获取车辆管信息，请核对车辆ID、车辆名称、车牌号");
            }
            try
            {
                if (Core.CarManager.Edit(car))
                {
                    return Ok();
                }
                return NotFound();

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}:发生错误");
        }

        /// <summary>
        /// 作用：删除车辆
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:45:59
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.CarManager.Delete(id);

            } catch(Exception ex)
            {
                LogWriter.WriteException(ex, "车辆删除");
            }
        }

        /// <summary>
        /// 作用：获取车辆信息
        /// 作者：汪建龙
        /// 编写时间:2017年2月17日10:49:58
        /// </summary>
        /// <param name="id">车辆ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var car = Core.CarManager.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        /// <summary>
        /// 作用：获取所有车辆信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:51:38
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Car> GetList()
        {
            return Core.CarManager.Search();
        }
    }
}
