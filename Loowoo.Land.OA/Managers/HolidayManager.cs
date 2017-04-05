using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class HolidayManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存节假日时间
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日13:57:26
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public int Save(Holiday holiday)
        {
            DB.Holidays.Add(holiday);
            DB.SaveChanges();
            return holiday.ID;
        }

        /// <summary>
        /// 作用：编辑节假日时间信息
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日13:57:53
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public bool Edit(Holiday holiday)
        {
            var model = DB.Holidays.Find(holiday.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(holiday);
            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：删除节假日时间信息
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日13:58:13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var model = DB.Holidays.Find(id);
            if (model == null)
            {
                return false;
            }
            DB.Holidays.Remove(model);
            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：验证节假日时间是否与其他节假日时间冲突
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日13:58:46
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool Exist(DateTime startTime,DateTime endTime)
        {
            return DB.Holidays.Any(e => (e.StartTime <= startTime && e.EndTime >= startTime) || (e.StartTime <= endTime && e.EndTime >= endTime));
        }
        /// <summary>
        /// 作用：验证当前节假日在某一年的是否重名
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日14:13:46
        /// </summary>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool Exist(string name,int year)
        {
            return DB.Holidays.Any(e => e.StartTime.Year == year && e.Name.ToLower() == name.ToLower());
        }
        public List<Holiday> GetList()
        {
            return DB.Holidays.OrderBy(e => e.StartTime).ToList();
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
            var list = DB.Holidays.Where(e => e.StartTime > date).ToList();
            for (; date < endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    var sunday = date.AddDays(1);
                    var entity = list.FirstOrDefault(e => e.StartTime == date || e.StartTime == sunday || e.EndTime == date || e.EndTime == sunday);
                    if (entity == null)
                    {
                        var weekofyear = GetWeekOfYear(date);
                        var name = date + "年 第" + weekofyear + "周";
                        DB.Holidays.Add(new Holiday { Name = name, StartTime = date, EndTime = date.AddDays(1) });
                    }
                }
            }
            DB.SaveChanges();
        }

    }
}
