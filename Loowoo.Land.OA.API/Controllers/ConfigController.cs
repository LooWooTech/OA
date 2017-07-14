using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class ConfigController : ControllerBase
    {
        [HttpGet]
        public object List()
        {
            return Core.ConfigManager.GetList();
        }

        [HttpPost]
        public void Save(string key, string value)
        {
            Core.ConfigManager.SetValue(key, value);
        }

        //[HttpPost]
        //public void Save(string keys, string values)
        //{
        //    var allKeys = keys.Split(',');
        //    var allValues = values.Split(',');
        //    var list = new List<Config>();
        //    for (var i = 0; i < allKeys.Length; i++)
        //    {
        //        list.Add(new Config
        //        {
        //            Key = allKeys[i],
        //            Value = allValues[i]
        //        });
        //    }
        //    Core.ConfigManager.SaveBatch(list);
        //}

        [HttpDelete]
        public void Delete(string key)
        {
            Core.ConfigManager.Delete(key);
        }
    }
}