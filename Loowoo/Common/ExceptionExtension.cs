using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Common
{
    public static class ExceptionExtension
    {
        public static int GetHttpStatusCode(this Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (ex is HttpException)
            {
                statusCode = (ex as HttpException).GetHttpCode();
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            return statusCode;
        }

        public static Exception GetInnerException(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerException.GetInnerException();
            }
            return ex;
        }
    }
}
