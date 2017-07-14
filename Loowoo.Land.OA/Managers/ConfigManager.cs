using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class ConfigManager : ManagerBase
    {
        public Config GetModel(string key)
        {
            return DB.Configs.FirstOrDefault(e => e.Key == key.ToLower());
        }

        public IEnumerable<Config> GetList()
        {
            return DB.Configs;
        }

        private Config GetModel(string key, string value)
        {
            var model = GetModel(key);
            if (model == null)
            {
                model = new Config { Key = key, Value = value };
                DB.Configs.Add(model);
            }
            return model;
        }

        public string GetValue(string key, string defaultValue = null)
        {
            var model = GetModel(key, defaultValue);
            return model.Value;
        }

        //public void SaveBatch(List<Config> list)
        //{
        //    foreach(var item in list)
        //    {
        //        if (string.IsNullOrEmpty(item.Key)) continue;
        //        item.Key = item.Key.ToLower();
        //        var entity = DB.Configs.FirstOrDefault(e => e.Key == item.Key);
        //        if(entity == null)
        //        {
        //            DB.Configs.Add(item);
        //        }
        //    }
        //    DB.SaveChanges();
        //}

        public void Delete(string key)
        {
            var entity = DB.Configs.FirstOrDefault(e => e.Key == key.ToLower());
            DB.Configs.Remove(entity);
            DB.SaveChanges();
        }

        public void SetValue(string key, string value)
        {
            var model = GetModel(key, value);
            model.Value = value;
            DB.SaveChanges();
        }
    }
}
