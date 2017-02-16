using Loowoo.Common;
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
    public class TestDocument
    {
        private WebApiTest _tool { get; set; }
        public TestDocument()
        {
            _tool = new WebApiTest();
        }

        [TestMethod]
        public void Test()
        {
            PostTest();
        }

        public void PostTest()
        {
            var receive = new ReceiveDocument
            {
                ID = 1,
                Number="20170216111111111111",
                Title="测试公文",
                ConfidentialLevel=0,
                UID=1,
                Category="测试种类1",
                Emergency=1,
                ReceiveWord="20170301",
                Keywords=""
            };
            var poststr = receive.ToJson();

            //Console.WriteLine(poststr);
            //_tool.Post("/api/receive/save", poststr);
            //receive.ID = 1;
            //receive.Number = "2017021612";
            //poststr = receive.ToJson();
            _tool.Put("/api/receive/edit", poststr);

        }
    }
}
