using Loowoo.Land.OA.API.Security;
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
        [AllowAnonymous]
        public IHttpActionResult Login(string name, string password)
        {
            TaskName = "用户登录";
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return BadRequest("登录名以及密码不能为空");
            }
            var user = Core.UserManager.Login(name, password);
            if (user == null)
            {
                return BadRequest($"{TaskName}:登录失败，请核对用户名以及密码");
            }
            user.Token = AuthorizeHelper.GetToken(new UserIdentity
            {
                ID = user.ID,
                Name = user.Name,
                DepartmentId = user.DepartmentId,
                Role = user.Role,
                Username = user.Name
            });

            return Ok(user);
        }

        [HttpGet]
        public object List(int departmentId = 0, int groupId = 0, string searchKey = null, int page = 1, int rows = 20)
        {
            var parameter = new UserParameter
            {
                DepartmentId = departmentId,
                GroupId = groupId,
                SearchKey = searchKey,
                Page = new Loowoo.Common.PageParameter(page, rows)
            };
            var list = Core.UserManager.Search(parameter);
            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Name,
                    e.Username,
                    e.Role,
                    e.DepartmentId,
                    DepartmentName = e.Department.Name,
                    Groups = e.UserGroups.Select(g => new
                    {
                        g.Group.Name,
                        g.Group.ID,
                    })
                }),
                Page = parameter.Page
            };
        }

        /// <summary>
        /// 作用：通过用户ID获取用户信息  参数ID不正确  返回BadRequest 用户不存在，返回NotFound()
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:38:38
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetModel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("用户ID参数不正确");
            }
            var user = Core.UserManager.Get(id);
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
            if (user == null
                || string.IsNullOrEmpty(user.Name)
                || string.IsNullOrEmpty(user.Username)
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

        /// <summary>
        /// 作用：用户编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日15:05:44
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] User user, [FromUri] string groupIds)
        {
            if (user == null
                || string.IsNullOrEmpty(user.Name)
                || string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("编辑用户参数错误");
            }
            Core.UserManager.Save(user);

            if (!string.IsNullOrEmpty(groupIds))
            {
                var ids = groupIds.Split(',').Select(str => int.Parse(str)).ToArray();
                Core.UserGroupManager.UpdateUserGroups(user.ID, ids);
            }
            return Ok();
        }
        [HttpDelete]
        public void Delete(int id)
        {
            Core.UserManager.Delete(id);
        }

    }
}
