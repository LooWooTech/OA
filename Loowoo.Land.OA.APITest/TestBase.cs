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
    public class TestBase
    {
        protected WebApiTest _tool { get; set; }
        public TestBase()
        {
            _tool = new WebApiTest();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Test()
        {
            PostTest();
            await AccouracyTest();
            await ExceptionTest();
        }

        public void PostTest()
        {
            #region 部门
            _tool.Post("/api/department/save", new Department { Name = "办公室" }.ToJson());
            _tool.Post("/api/department/save", new Department { Name = "规划耕保科" }.ToJson());
            _tool.Post("/api/department/save", new Department { Name = "利用科" }.ToJson());
            _tool.Post("/api/department/save", new Department { Name = "瞎逼逼" }.ToJson());
            #endregion

            #region  组

            _tool.Post("/api/group/save", new Group { Name = "组1", Type = GroupType.User }.ToJson());
            _tool.Post("/api/group/save", new Group { Name = "组2", Type = GroupType.User }.ToJson());
            _tool.Post("/api/group/save", new Group { Name = "组3", Type = GroupType.System }.ToJson());
            
            #endregion

            #region 表单
            _tool.Post("/api/form/save", new Form { Name = "公文", FLowId = 1 }.ToJson());
            _tool.Post("/api/form/save", new Form { Name = "车辆", FLowId = 1 }.ToJson());
            _tool.Post("/api/form/save", new Form { Name = "会议", FLowId = 1 }.ToJson());
            #endregion

            #region 种类
            _tool.Post("/api/category/save", new Category { Name = "种类1", FormID = 1, Sort = 0, Type = 1 }.ToJson());
            _tool.Post("/api/category/save", new Category { Name = "种类2", FormID = 1, Sort = 0, Type = 1 }.ToJson());

            #endregion

            #region  订阅类型
            _tool.Post("/api/subscription/save", new Subscription { Name = "订阅1" }.ToJson());
            _tool.Post("/api/subscription/save", new Subscription { Name = "订阅2" }.ToJson());
            _tool.Post("/api/subscription/save", new Subscription { Name = "订阅3" }.ToJson());
            #endregion

            #region Flow

            _tool.Post("/api/flow/save", new Flow { Name = "模板1" }.ToJson());
            _tool.Post("/api/flow/save", new Flow { Name = "模板2" }.ToJson());
            _tool.Post("/api/flow/save", new Flow { Name = "模板3" }.ToJson());
            #endregion

        }

        public async System.Threading.Tasks.Task AccouracyTest()
        {
            await _tool.Test("/api/department/model?id=1");
            await _tool.Test("/api/department/list");
            await _tool.Test("/api/department/delete?id=4");


            await _tool.Test("/api/category/model?id=1");
            await _tool.Test("/api/category/list?formid=1");


            await _tool.Test("/api/subscription/model?id=1");
            await _tool.Test("/api/subscription/model?id=2");
            await _tool.Test("/api/subscription/list");

            await _tool.Test("/api/flow/model?id=1");
            await _tool.Test("/api/flow/model?id=2");
            
        }
        public async System.Threading.Tasks.Task ExceptionTest()
        {
            await _tool.Test("/api/deparment/model");
            await _tool.Test("/api/department/delete");

            await _tool.Test("/api/category/model?id=9");
            await _tool.Test("/api/category/list");
        }

    }
}
