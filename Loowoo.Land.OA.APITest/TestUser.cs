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
    public class TestUser
    {
        protected WebApiTest _tool { get; set; }
        public TestUser()
        {
            _tool = new WebApiTest();
        }
        [TestMethod]
        public async System.Threading.Tasks.Task Test()
        {
            PostTest();
            await AccuracyTest();
            await ExceptionTest();
            await StressTest();
        }
        public void PostTest()
        {
            var admin = new User
            {
                Username = "管理员",
                Password = "admin",
                Name = "Admin",
                Role=Security.UserRole.Administrator
            };
            _tool.Post("/api/user/register", admin.ToJson());
            _tool.Post("/api/user/register", new User { Username = "汪建龙", Password = "123456", Name = "wjl" }.ToJson());
            _tool.Post("/api/user/register", new User { Username = "唐尧", Password = "123456", Name = "ty" }.ToJson());
            _tool.Post("/api/user/register", new User { Username = "赵斯思", Password = "123456", Name = "zss" }.ToJson());
            _tool.Post("/api/user/register", new User { Username = "郑良军", Password = "123456", Name = "zlj" }.ToJson());

            
        }

    
        public  async System.Threading.Tasks.Task AccuracyTest()
        {
   
            await _tool.Test($"/api/user/login?name=wjl&&password=123456");
            await _tool.Test("/api/user/list");
            //var page = 1;
            //var rows = 20;
            //await _tool.Test($"/api/user/getlist?page={page}&&rows={rows}");
            //await _tool.Test($"/api/user/get?id=1");
            //await _tool.Test($"/api/user/delete?id=2");

        }

        public async System.Threading.Tasks.Task ExceptionTest()
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

        public async System.Threading.Tasks.Task StressTest()
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
