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
        }
        public PageParameter(int? page,int? limit)
        {
            PageIndex = page.HasValue ? page.Value < 1 ? 1 : page.Value : 1;
            PageSize = limit.HasValue ? limit.Value < 1 ? 20 : limit.Value : 20;
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
