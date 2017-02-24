using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Loowoo.Land.OA.API.Controllers
{
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 作用：用户登陆 登录名和密码参数不正确 返回BadRequest, 用户不存在 返回NotFound,存在返回用户信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:36:47
        /// </summary>
        /// <param name="name">登陆名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Login(string name,string password)
        {
            TaskName = "用户登录";
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return BadRequest("登录名以及密码不能为空");
            }
            var user = Core.UserManager.Login(name, password);
            if (user == null)
            {
                return NotFound();
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, name, DateTime.Now, DateTime.Now.AddHours(1), true, string.Format("{0}&{1}&{2}",user.ID, name, password), FormsAuthentication.FormsCookiePath);
            user.Ticket = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Session.Add(name, user);
            return Ok(user);
        }

        /// <summary>
        /// 作用：获取所有用户列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日18:29:09
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<User> List(int departmentId,int groupId,string searchKey)
        {
            var parameter = new UserParameter
            {
                DepartmentId = departmentId,
                GroupId = groupId,
                SearchKey = searchKey
            };
            var list = Core.UserManager.Search(parameter);
            return list;
        }

        /// <summary>
        /// 作用：通过用户ID获取用户信息  参数ID不正确  返回BadRequest 用户不存在，返回NotFound()
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:38:38
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("用户ID参数不正确");
            }
            var user = Core.UserManager.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// 作用：添加新用户
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:56:45
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Register([FromBody]User user)
        {
            if(user==null
                ||string.IsNullOrEmpty(user.Name)
                ||string.IsNullOrEmpty(user.Username)
                || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("注册用户信息不正确");
            }
            if (Core.UserManager.Exist(user.Name))
            {
                return BadRequest(string.Format("当前系统中已存在登录名：{0}，请更改登陆名", user.Name)); 
            }
            Core.UserManager.Register(user);
            return Ok();

        }
        ///// <summary>
        ///// 作用：获取用户列表
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月11日13:52:09
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="rows"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[RequestAuthorize(Role =Security.UserRole.Administrator)]
        //public List<User> GetList(int page, int rows)
        //{
        //    var parameter = new UserParameter
        //    {
        //        Page = new Common.PageParameter(page, rows)
        //    };
        //    var list = Core.UserManager.Search(parameter);
        //    return list;
        //}



        /// <summary>
        /// 作用：用户编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日15:05:44
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] User user)
        {
            if (user == null 
                || string.IsNullOrEmpty(user.Name) 
                || string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("编辑用户参数错误");
            }
            try
            {
                Core.UserManager.Edit(user);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex,"编辑用户");
                return BadRequest("编辑用户发生错误");
            }
            return Ok();
        }
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.UserManager.Delete(id);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex,"用户删除");
            }
        }

        //public void LogOut()
        //{

        //}

        
    }
}
