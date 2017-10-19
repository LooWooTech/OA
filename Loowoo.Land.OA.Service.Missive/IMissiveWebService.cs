using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Missive
{
    public interface IMissiveWebService
    {
        bool Report(MissiveServiceLog log);
    }
}
