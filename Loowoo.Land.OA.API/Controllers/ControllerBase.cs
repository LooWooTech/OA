using Loowoo.Common;
using Loowoo.Security;
using Loowoo.Land.OA.API.Managers;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.Services;

namespace Loowoo.Land.OA.API.Controllers
{
    
    public class ControllerBase : ApiController
    {
        protected ManagerCore Core = ManagerCore.Instance;

        protected LogWriter LogWriter = LogWriter.Instance;
        protected string TaskName { get; set; }

        private User _currentUser { get; set; }
        protected User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    var authorization = Request.Headers.Authorization;
                    if (authorization != null && authorization.Parameter != null)
                    {
                        var strTicket = FormsAuthentication.Decrypt(authorization.Parameter).UserData;
                        var array = strTicket.Split('&');
                        if (array.Length == 4)
                        {
                            _currentUser = new User
                            {
                                ID = int.Parse(array[0]),
                                Name = array[1]
                            };
                            UserRole role = UserRole.Guest;
                            if (Enum.TryParse(array[2], out role))
                            {
                                _currentUser.Role = role;
                            }
                            if (!string.IsNullOrEmpty(array[3]))
                            {
                                _currentUser.DepartmentId = int.Parse(array[3]);
                            }
                        }
                    }
                }

                return _currentUser;
            }
        }
        
        protected void ThrowException(string error,string reason)
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(error),
                ReasonPhrase = reason
            });
        }
    }
}
