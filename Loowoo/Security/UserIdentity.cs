using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Security
{
    public class UserIdentity : System.Security.Principal.IIdentity
    {
        public static UserIdentity Anonymouse
        {
            get
            {
                return new UserIdentity();
            }
        }

        public int ID { get; set; }

        public string AuthenticationType
        {
            get { return "token"; }
        }

        public UserRole Role { get; set; }

        public bool IsAuthenticated
        {
            get { return ID > 0; }
        }

        public string Name { get; set; }
    }
}
