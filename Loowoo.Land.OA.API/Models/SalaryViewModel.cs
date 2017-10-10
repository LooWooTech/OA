using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class SalaryViewModel
    {
        public SalaryViewModel(Salary model)
        {
            ID = model.ID;
            Year = model.Year;
            Month = model.Month;
            UserId = model.UserId;
            UserName = model.User == null ? null : model.User.Username;
            Data = model.Data;
        }

        public int ID { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public Document Data { get; set; }
    }
}