using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public object Model(int id)
        {
            return Core.TaskManager.GetModel(id);
        }

        [HttpGet]
        public object List(string searchKey = null, FlowStatus? status = null, int page = 1, int rows = 20)
        {
            var form = Core.FormManager.GetModel(FormType.Task);
            var parameter = new FormInfoParameter
            {
                FormId = form.ID,
                Status = status,
                Page = new PageParameter(page, rows),
                UserId = CurrentUser.ID,
                SearchKey = searchKey
            };
            var datas = Core.TaskManager.GetList(parameter);
            return new PagingResult
            {
                List = datas.Select(e => new
                {
                    e.ID,
                    e.Name,
                    e.ScheduleDate,
                    e.From,
                    e.FromType,
                    e.Goal,
                    e.Info.FormId,
                    e.Info.CreateTime,
                    e.Info.UpdateTime,
                    e.Info.FlowStep,
                    e.Info.FlowDataId,
                }),
                Page = parameter.Page
            };
        }

        [HttpPost]
        public void Save([FromBody]Task data)
        {
            var form = Core.FormManager.GetModel(FormType.Task);
            var isAdd = data.ID == 0;
            //判断id，如果不存在则创建forminfo
            if (data.ID == 0)
            {
                data.Info = new FormInfo
                {
                    FormId = form.ID,
                    PostUserId = CurrentUser.ID,
                    Title = data.Name
                };
                Core.FormInfoManager.Save(data.Info);
            }
            else
            {
                data.Info = Core.FormInfoManager.GetModel(data.ID);
                data.Info.Title = data.Name;
            }
            if (data.Info.FlowDataId == 0)
            {
                data.Info.Form = form;
                Core.FlowDataManager.CreateFlowData(data.Info);
            }
            Core.TaskManager.Save(data);

            Core.FeedManager.Save(new Feed
            {
                InfoId = data.ID,
                Title = data.Name,
                Description = data.Goal,
                FromUserId = CurrentUser.ID,
                Action = isAdd ? UserAction.Create : UserAction.Update,
            });
        }

        [HttpGet]
        public object SubTaskList(int taskId)
        {
            return Core.TaskManager.GetSubTaskList(taskId).Select(e => new
            {
                e.ID,
                e.CreateTime,
                e.CreatorId,
                CreatorName = e.Creator == null ? "" : e.Creator.RealName,
                e.Completed,
                e.Content,
                e.UpdateTime,
                e.ToDepartmentId,
                e.ToDepartmentName,
                e.ToUserId,
                ToUserName = e.ToUser == null ? "" : e.ToUser.RealName,
                e.TaskId,
                e.ScheduleDate,
                e.ParentId,
                e.IsMaster,
                Todos = e.Todos.Select(t => new
                {
                    t.ID,
                    t.CreateTime,
                    t.ScheduleDate,
                    t.ToUserId,
                    ToUserName = t.ToUser == null ? "" : t.ToUser.RealName,
                    t.SubTaskId,
                    t.UpdateTime,
                    t.Completed,
                    t.Content,
                })
            });
        }

        [HttpPost]
        public void SaveSubTask(SubTask data)
        {
            if (data.ToDepartmentId == 0)
            {
                throw new Exception("没有指定科室");
            }
            if (data.ToUserId == 0)
            {
                throw new Exception("没有指定责任人");
            }
            var isAdd = data.ID == 0;
            var department = Core.DepartmentManager.Get(data.ToDepartmentId);
            data.ToDepartmentName = department.Name;
            data.CreatorId = CurrentUser.ID;
            Core.TaskManager.SaveSubTask(data);
            if (!isAdd) return;

            var info = Core.FormInfoManager.GetModel(data.TaskId);
            var flowData = info.FlowData;
            var flowNodeData = flowData.GetFlowNodeDataByStep((int)TaskFlowStep.Working);
            //如果还没指派过任务
            if (flowNodeData == null)
            {
                //创建任务办理流程记录（不指定任何人）
                flowNodeData = Core.FlowNodeDataManager.CreateNextNodeData(flowData, 0);
            }
            var toUserNodeData = flowData.Nodes.FirstOrDefault(e => e.ParentId == flowNodeData.ID && e.UserId == data.ToUserId);
            if (toUserNodeData == null)
            {
                if (data.IsMaster)
                {
                    toUserNodeData = Core.FlowNodeDataManager.CreateChildNodeData(flowNodeData, data.ToUserId);
                }
                else
                {
                    var parentTask = Core.TaskManager.GetSubTask(data.ParentId);
                    var parentTaskFlowNodeData = flowData.Nodes.FirstOrDefault(e => e.ParentId == flowNodeData.ID && e.UserId == parentTask.ToUserId);
                    toUserNodeData = Core.FlowNodeDataManager.CreateChildNodeData(parentTaskFlowNodeData, data.ToUserId);
                }
            }

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = data.TaskId,
                UserId = data.ToUserId,
                Status = FlowStatus.Doing,
            });
            //通知相关人员
            Core.FeedManager.Save(new Feed
            {
                Action = UserAction.Create,
                FromUserId = CurrentUser.ID,
                ToUserId = data.ToUserId,
                Title = info.Title,
                Description = data.Content,
                Type = FeedType.Task,
                InfoId = data.TaskId,
            });
        }

        [HttpDelete]
        public void DeleteSubTask(int id)
        {
            var model = Core.TaskManager.GetSubTask(id);
            Core.TaskManager.DeleteSubTask(model);

            var info = Core.FormInfoManager.GetModel(model.TaskId);

            var parentNodeData = info.FlowData.GetFlowNodeDataByStep((int)TaskFlowStep.Working);
            var flowNodeData = info.FlowData.Nodes.FirstOrDefault(e => e.UserId == model.ToUserId && e.ParentId == parentNodeData.ID);
            Core.FlowNodeDataManager.Delete(flowNodeData);
            Core.UserFormInfoManager.Delete(info.ID, model.ToUserId);

        }

        [HttpGet]
        public object TodoList(int subTaskId)
        {
            return Core.TaskManager.GetTodoList(subTaskId).Select(e => new
            {
                e.ID,
                e.Completed,
                e.Content,
                e.CreateTime,
                e.ScheduleDate,
                e.SubTaskId,
                e.ToUserId,
                ToUserName = e.ToUser == null ? null : e.ToUser.RealName,
                e.UpdateTime
            });
        }

        [HttpPost]
        public void SaveTodo(TaskTodo model)
        {
            model.CreatorId = CurrentUser.ID;
            Core.TaskManager.SaveTodo(model);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = model.SubTask.TaskId,
                UserId = model.ToUserId,
                Status = FlowStatus.Doing
            });
            //创建自由流程，转发给此人
            Core.FeedManager.Save(new Feed
            {
                ToUserId = model.ToUserId,
                Title = model.Content,
                Type = FeedType.Info,
                Action = UserAction.Create,
            });
        }

        [HttpGet]
        public void UpdateTodoStatus(int id)
        {
            var model = Core.TaskManager.GetTodo(id);
            model.Completed = !model.Completed;
            model.UpdateTime = DateTime.Now;
            Core.TaskManager.SaveTodo(model);
        }

        [HttpDelete]
        public void DeleteTodo(int id)
        {
            var model = Core.TaskManager.GetTodo(id);
            var infoId = model.SubTask.TaskId;
            Core.TaskManager.DeleteTodo(model);
            if (!model.SubTask.Todos.Any(e => e.ToUserId == model.ToUserId))
            {
                Core.UserFormInfoManager.Delete(infoId, model.ToUserId);
                Core.FeedManager.Delete(new Feed { InfoId = infoId, ToUserId = model.ToUserId, FromUserId = model.CreatorId });
            }
        }
    }
}
