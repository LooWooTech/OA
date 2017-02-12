using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;

namespace Loowoo.Land.OA.APITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tool = new CommentsWebApiTest();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            tool.GetComments();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

  
    }
}
