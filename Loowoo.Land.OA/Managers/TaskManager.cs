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
            var infos = Core.UserFormInfoManager.GetList(parameter);
            parameter.InfoIds = infos.Select(e => e.InfoId).ToArray();

            var query = DB.Tasks.AsQueryable();
            if (parameter.InfoIds != null)
            {
                query = query.Where(e => parameter.InfoIds.Contains(e.ID));
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Name.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID);
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

        public IEnumerable<SubTask> GetSubTaskList(int taskId)
        {
            return DB.SubTasks.Where(e => e.TaskId == taskId);
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

        public void DeleteTodo(int id)
        {
            var model = GetTodo(id);
            DB.Todos.Remove(model);
            DB.SaveChanges();
        }

        public SubTask GetSubTask(int subTaskId)
        {
            return DB.SubTasks.FirstOrDefault(e => e.ID == subTaskId);
        }

        public void DeleteSubTask(int subTaskId)
        {
            var entity = GetSubTask(subTaskId);
            DB.SubTasks.Remove(entity);
            DB.SaveChanges();
        }
    }
}