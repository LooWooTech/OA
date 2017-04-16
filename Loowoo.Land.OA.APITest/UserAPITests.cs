using Loowoo.Land.OA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.APITest
{
    [TestClass]
    public class UserAPITests:APITestBase
    {
        public override string GetBaseAddress()
        {
            return "http://localhost:61709";
        }
        [TestMethod]
        public void AccuracyTest()
        {
            var result = InvokeGetRequest<User>("/api/user/login?name=wjl&&password=123456");

            
        }

        public void ExceptionTest()
        {

        }

        public void StressTest()
        {

        }
    }
}
