using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class SmsController : ControllerBase
    {
        [HttpGet]
        public void Send(int infoId, string userId = null)
        {
            var userIds = userId.ToIntArray();
            if (userIds == null || userIds.Length == 0)
            {
                return;
            }
            var users = Core.UserManager.GetList(new Parameters.UserParameter { UserIds = userIds });
            var info = Core.FormInfoManager.GetModel(infoId);

            var title = info.Title;
            var content = $"您有待处理的{info.Form.FormType.GetDescription()}：{info.Title}，请及时处理。发送人：" + CurrentUser.RealName;

            //10个号码为一批
            var pageSize = 10;
            for (var i = 0; i < userIds.Length / pageSize + 1; i++)
            {
                var mobiles = string.Join(",", users.Skip(i * pageSize).Take(pageSize).Select(e => e.Mobile));
                Core.SmsManager.Create(new OA.Models.Sms
                {
                    Content = content,
                    Numbers = mobiles,
                });
            }
        }
    }
}