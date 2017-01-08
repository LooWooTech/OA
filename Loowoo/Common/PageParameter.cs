using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Common
{
    public class PageParameter
    {
        public PageParameter() : this(1, 20)
        {
        }

        public PageParameter(int page, int limit)
        {
            PageIndex = page < 1 ? 1 : page;
            PageSize = limit < 1 ? 20 : limit;
            LinkFormat = "?page={0}";
        }

        public string LinkFormat { get; set; }

        public string GetPageLink(int pageIndex, NameValueCollection queryString)
        {
            if (string.IsNullOrEmpty(LinkFormat))
            {
                LinkFormat = "&page={0}&rows={1}";
            }
            var link = string.Format(LinkFormat, pageIndex, PageSize);
            foreach (var key in queryString.AllKeys)
            {
                if (!"page|rows".Contains(key.ToLower()))
                {
                    link += "&" + key + "=" + HttpUtility.UrlEncode(queryString[key]);
                }
            }
            return link;
        }

        [JsonProperty("total")]
        public int RecordCount { get; set; }

        [JsonProperty("page")]
        public int PageIndex { get; set; }

        [JsonProperty("rows")]
        public int PageSize { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount
        {
            get
            {
                return RecordCount / PageSize + (RecordCount % PageSize > 0 ? 1 : 0);
            }
        }
    }
}
