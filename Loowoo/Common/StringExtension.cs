using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public static class StringExtension
    {
        public static int[] ToIntArray(this string str, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val)).ToArray();
        }
    }
}
