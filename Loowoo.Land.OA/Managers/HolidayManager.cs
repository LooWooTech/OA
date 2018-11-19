using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class HolidayManager : ManagerBase
    {
        public void Save(Holiday model)
        {
            DB.Holidays.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var model = DB.Holidays.Find(id);
            if (model != null)
            {
                model.Deleted = true;
                DB.SaveChanges();
            }
        }

        public IEnumerable<Holiday> GetList(HolidayParameter parameter)
        {
            var query = DB.Holidays.Where(e => !e.Deleted);
            if (parameter.BeginDate.HasValue)
            {
                query = query.Where(e => e.BeginDate >= parameter.BeginDate);
            }
            if(parameter.EndDate.HasValue)
            {
                query = query.Where(e => e.EndDate <= parameter.EndDate);
            }
            var list = query.OrderByDescending(e => e.BeginDate).SetPage(parameter.Page).ToList();

            return list;
        }

        public static int GetWeekOfYear(DateTime todayTime)
        {
            var firstdayofweek = Convert.ToInt32(Convert.ToDateTime(todayTime.Year.ToString(CultureInfo.InvariantCulture) + "- " + "1-1 ").DayOfWeek);
            var days = todayTime.DayOfYear;
            var daysOutOneWeek = days - (7 - firstdayofweek);
            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            var weeks = daysOutOneWeek / 7;
            if (daysOutOneWeek % 7 != 0)
                weeks++;
            return weeks + 1;
        }

        public void GenerateWeek(int year)
        {
            var date = DateTime.Now.Date;
            if (year > DateTime.Now.Year)
            {
                date = new DateTime(year, 1, 1);
            }
            var endDate = new DateTime(year + 1, 1, 1);
            var list = DB.Holidays.Where(e => e.BeginDate > date).ToList();
            for (; date < endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    var sunday = date.AddDays(1);
                    var entity = list.FirstOrDefault(e => e.BeginDate == date || e.BeginDate == sunday || e.EndDate == date || e.EndDate == sunday);
                    if (entity == null)
                    {
                        var weekofyear = GetWeekOfYear(date);
                        var name = date.Year + "年 第" + weekofyear + "周";
                        DB.Holidays.Add(new Holiday { Name = name, BeginDate = date, EndDate = date.AddDays(1) });
                    }
                }
            }
            DB.SaveChanges();
        }

    }
}
