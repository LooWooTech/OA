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

        public void SetValue(string key, string value)
        {
            var model = GetModel(key, value);
            model.Value = value;
            DB.SaveChanges();
        }
    }
}
