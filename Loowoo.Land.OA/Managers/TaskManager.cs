using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class TaskManager : ManagerBase
    {
        public IEnumerable<Task> GetList(FormInfoParameter parameter)
        {
            var query = DB.Tasks.AsQueryable();
            parameter.InfoIds = Core.UserFormInfoManager.GetUserInfoIds(parameter);
            query = query.Where(e => parameter.InfoIds.Contains(e.ID));
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Name.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(Task data)
        {
            var entity = DB.Tasks.FirstOrDefault(e => e.ID == data.ID);
            if (entity == null)
            {
                DB.Tasks.Add(data);
            }
            else
            {
                DB.Entry(entity).CurrentValues.SetValues(data);
            }
            DB.SaveChanges();
        }

        public Task GetModel(int id)
        {
            return DB.Tasks.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<SubTask> GetSubTaskList(int taskId, int? parentId = null)
        {
            var query = DB.SubTasks.Where(e => e.TaskId == taskId);
            if (parentId.HasValue)
                query = query.Where(e => e.ParentId == parentId.Value);
            return query;
        }

        public void SaveSubTask(SubTask model)
        {
            if (model.ID > 0)
            {
                var entity = DB.SubTasks.FirstOrDefault(e => e.ID == model.ID);
                if (entity.IsMaster != model.IsMaster)
                {
                    throw new Exception("不能切换主办协办属性");
                }
                DB.Entry(entity).CurrentValues.SetValues(model);
            }
            else
            {
                DB.SubTasks.Add(model);
            }
            DB.SaveChanges();
        }


        public IEnumerable<TaskTodo> GetTodoList(int subTaskId)
        {
            return DB.Todos.Where(e => e.SubTaskId == subTaskId);
        }

        public void SaveTodo(TaskTodo model)
        {
            DB.Todos.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public TaskTodo GetTodo(int id)
        {
            return DB.Todos.Find(id);
        }

        public void DeleteTodo(TaskTodo model)
        {
            DB.Todos.Remove(model);
            DB.SaveChanges();
        }

        public SubTask GetSubTask(int subTaskId)
        {
            return DB.SubTasks.FirstOrDefault(e => e.ID == subTaskId);
        }

        public void DeleteSubTask(SubTask model)
        {
            if (model.IsMaster)
            {
                if (DB.SubTasks.Any(e => e.ParentId == model.ID))
                {
                    throw new Exception("不能直接删除主办单位的任务，请先删除其协办单位");
                }
            }
            DB.SubTasks.Remove(model);
            DB.SaveChanges();
        }
    }
}