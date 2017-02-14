using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.APITest
{
    [TestClass]
    public class TestUser
    {
        private WebApiTest _tool { get; set; }
        
        public TestUser()
        {
            _tool = new WebApiTest();
        }
        [TestMethod]
        public async Task Test()
        {
            await AccuracyTest();
            await ExceptionTest();
            await StressTest();
        }

    
        public async Task AccuracyTest()
        {
            var page = 1;
            var rows = 20;
            await _tool.Test($"/api/user/login?name=wjl&&password=123456");
            await _tool.Test($"/api/user/getlist?page={page}&&rows={rows}");
            await _tool.Test($"/api/user/get?id=1");
            await _tool.Test($"/api/user/delete?id=2");

            await _tool.Test($"/api/document/getlist?page={page}&&rows={rows}");
            await _tool.Test($"/api/document/get?id=1");

        }

        public async Task ExceptionTest()
        {
            await _tool.Test($"/api/user/login");
            await _tool.Test($"/api/user/login?name=ty&&password=123");
            await _tool.Test($"/api/user/login?name=wjl&&password=");
            await _tool.Test($"/api/user/login?name=&&password=123456");
            await _tool.Test($"/api/user/getlist");
            await _tool.Test($"/api/user/get?id=-9");
            await _tool.Test($"/api/user/get?id=7");
            await _tool.Test($"/api/user/delete?id=-9");
            await _tool.Test($"/api/user/delete?id=7");
            await _tool.Test($"/api/document/getlist");
            await _tool.Test($"/api/document/delete?id=0");
            await _tool.Test($"/api/document/delete?id=10");

        }

        public async Task StressTest()
        {
            for (var i = 0; i < 100; i++)
            {
                await _tool.Test($"/api/user/login?name=wjl&&password=123456");
                await _tool.Test($"/api/user/get?id=1");
                await _tool.Test($"/api/document/getlist?page=1&&rows=20");
            }

        }

    }
}
