using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class AttendanceManager:ManagerBase
    {
        public IEnumerable<Attendance> Search(AttendanceParameter parameter)
        {
            var query = DB.Attendances.AsQueryable();
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserID == parameter.UserId.Value);
            }
        
            if (parameter.BeginDate.HasValue)
            {
                query = query.Where(e => e.Date >= parameter.BeginDate.Value);
            }
            if (parameter.EndDate.HasValue)
            {
                query = query.Where(e => e.Date <= parameter.EndDate.Value);
            }
            query = query.OrderBy(e => e.Date).SetPage(parameter.Page);
            return query;
        }
    }
}
