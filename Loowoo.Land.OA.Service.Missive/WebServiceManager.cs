using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Missive
{
    public class WebServiceManager
    {
        public static IMissiveWebService GetInstance()
        {
            return new JSWJWebService();
        }
    }
}
