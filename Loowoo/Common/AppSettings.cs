using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public class AppSettings
    {
        private AppSettings() { }

        public readonly static AppSettings Current = new AppSettings();

        public static string Get(string key)
        {
            return Current[key];
        }

        public string this[string key]
        {
            get
            {
#if DEBUG
                var key1 = "debug_" + key;
#else
                var key1 = "release_" + key;
#endif
                if (ConfigurationManager.AppSettings.AllKeys.Contains(key1))
                {
                    return ConfigurationManager.AppSettings[key1];
                }
                else if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                {
                    return ConfigurationManager.AppSettings[key];
                }

                return null;
            }
        }

    }
}
