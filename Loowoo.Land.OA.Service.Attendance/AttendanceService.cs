using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Attendance
{
    public class AttendanceService
    {
        public void Start()
        {
            //根据当前时间 遍历打卡记录

            //在打卡时间结束之前，调用接口打卡（调用接口的目的是同步到市里OA系统）
            //成果和失败 都做记录

            //过了打卡时间，则根据实际打卡时间 自己计算

        }
    }
}
