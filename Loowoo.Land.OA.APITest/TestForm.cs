using Loowoo.Land.OA.Models;
using Loowoo.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.APITest
{
    [TestClass]
    public class TestForm
    {
        private WebApiTest _tool { get; set; }
        public TestForm()
        {
            _tool = new WebApiTest();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Test()
        {
            PostTest();
        }

        public void PostTest()
        {
            var form = new Form
            {
                Name = "公文"
            };
            _tool.Post("/api/Form/Save", form.ToJson());
            _tool.Post("/api/Form/Save", new Form { Name = "车辆" }.ToJson());
            _tool.Post("/api/Form/Save", new Form { Name = "会议" }.ToJson());
        }

        public async System.Threading.Tasks.Task AccuracyTest()
        {
            await _tool.Test("/api/Form/GetList");
            await _tool.Test("/api/Form/Get?id=1");
        }
    }
}
