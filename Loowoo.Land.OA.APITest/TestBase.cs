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
    public class TestBase
    {
        private WebApiTest _tool { get; set; }
        public TestBase()
        {
            _tool = new WebApiTest();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Test()
        {

        }

        public void PostTest()
        {
           // _tool.Post("/api/Confidential/save",new ConfidentialLevel { })
        }
    }
}
