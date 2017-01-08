using Loowoo.Caching;
using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Web
{
    public static class ValidateCodeHelper
    {
        private static string CacheKey = "validate_codes";
        private static string CookieName = "vcsid";

        private static ICacheService Cache;

        static ValidateCodeHelper()
        {
            Cache = Factory<ICacheService>.CreateInstance(AppSettings.Get("Cache") ?? "Loowoo.WebCaching.LocalCacheService");
        }

        public static void SaveValidateCode(this HttpContextBase context, string code)
        {
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieName, Guid.NewGuid().ToString());
            }
            context.Response.SetCookie(cookie);
            var sessionId = cookie.Value;
            Cache.HSet(CacheKey, sessionId, code);
        }

        public static void CheckValidateCode(this HttpContextBase context, string code)
        {
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
            {
                throw new Exception("请不要禁用cookie");
            }
            var sessionId = cookie.Value;
            var saveCode = Cache.HGet<string>(CacheKey, sessionId);
            if (code.ToLower() != saveCode.ToLower())
            {
                throw new Exception("验证码不正确");
            }
        }
    }
}
