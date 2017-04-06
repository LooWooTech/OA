using Loowoo.Land.OA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class UserFormInfoVM
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int InfoId { get; set; }
        public int FormId { get; set; }
        public int FlowNodeDataId { get; set; }
        public string FlowNodeName { get; set; }
        public FlowStatus Status { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int PostUserId { get; set; }
        public int FlowDataId { get; set; }
        //public object Data
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(Json)) return null;

        //        return JsonConvert.DeserializeObject(Json);
        //    }
        //    set
        //    {
        //        Json = JsonConvert.SerializeObject(value);
        //    }
        //}
    }
}