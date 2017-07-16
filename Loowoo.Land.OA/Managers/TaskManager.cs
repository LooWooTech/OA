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
                query = query.Where(e => e.MC.Contains(parameter.SearchKey));
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

        public IEnumerable<TaskTodo> GetTodoList(int taskId)
        {
            return DB.Todos.Where(e => e.TaskId == taskId);
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

        public IEnumerable<TaskProgress> GetProgressList(int taskId)
        {
            return DB.TaskProgresses.Where(e => e.TaskId == taskId && !e.Deleted).OrderByDescending(e => e.ID);
        }

        public void SaveProgress(TaskProgress model)
        {
            DB.TaskProgresses.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public TaskProgress GetProgress(int id)
        {
            return DB.TaskProgresses.Find(id);
        }

        public void DeleteProgress(TaskProgress model)
        {
            model.Deleted = true;
            DB.SaveChanges();
        }
    }
}