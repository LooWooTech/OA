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
                e.Status,
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
                    t.CreatorId,
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
            var flowNodeData = flowData.GetFirstNodeData();
            var toUserNodeData = Core.FlowNodeDataManager.GetModelByExtendId(data.ID, data.ToUserId);
            if (toUserNodeData == null)
            {
                toUserNodeData = Core.FlowNodeDataManager.CreateChildNodeData(flowNodeData, data.ToUserId, data.ID);
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
                Title = "[创建任务]" + info.Title,
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

            var flowNodeData = Core.FlowNodeDataManager.GetModelByExtendId(model.ID, model.ToUserId);
            Core.FlowNodeDataManager.Delete(flowNodeData);
            Core.UserFormInfoManager.Delete(model.TaskId, model.ToUserId);
            Core.FeedManager.Delete(new Feed { ToUserId = model.ToUserId, InfoId = model.TaskId });
        }

        /// <summary>
        /// 提交子任务，创建子任务相关流程
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="result"></param>
        /// <param name="toUserId"></param>
        [HttpGet]
        public void SubmitSubTask(int id, string content = null, bool result = true, int toUserId = 0)
        {
            var model = Core.TaskManager.GetSubTask(id);
            if (model.IsMaster)
            {
                var children = Core.TaskManager.GetSubTaskList(model.TaskId, model.ID);
                if (!children.All(e => e.Status == SubTaskStatus.Complete))
                {
                    throw new Exception("子任务还没完成，无法提交");
                }
            }
            if (model.Todos.Any(e => !e.Completed))
            {
                throw new Exception("子任务还没完成，无法提交");
            }
            model.Status = SubTaskStatus.Checking;

            var flowNodeData = Core.FlowNodeDataManager.GetModelByExtendId(model.ID, model.ToUserId);
            if (flowNodeData.Submited)
            {
                return;
            }
            flowNodeData.Content = content;
            flowNodeData.Result = result;
            Core.FlowNodeDataManager.Submit(flowNodeData);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = model.TaskId,
                UserId = model.ToUserId,
                Status = FlowStatus.Done
            });

            //如果是协办科室，则直接提交结束
            if (!model.IsMaster)
            {
                var parentSubTask = Core.TaskManager.GetSubTask(model.ParentId);
                toUserId = parentSubTask.ToUserId;
            }
            else
            {
                //判断子任务是否都已经完成

                //主办科室提交，则需要创建分管领导主流程
                if (toUserId == 0)
                {
                    throw new Exception("没有指定分管领导");
                }
            }
            Core.FlowNodeDataManager.CreateChildNodeData(flowNodeData, toUserId, model.ID);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = model.TaskId,
                UserId = toUserId,
                Status = FlowStatus.Doing,
            });
            Core.FeedManager.Save(new Feed
            {
                FromUserId = model.ToUserId,
                ToUserId = toUserId,
                Action = UserAction.Submit,
                Type = FeedType.Flow,
                InfoId = model.TaskId,
                Title = "[提交任务]" + model.Content,
            });
        }

        [HttpGet]
        public IEnumerable<FlowNodeData> CheckList(int taskId)
        {
            var info = Core.FormInfoManager.GetModel(taskId);
            return info.FlowData.Nodes.Where(e => e.UserId == CurrentUser.ID);
        }

        /// <summary>
        /// 分管领导审批
        /// </summary>
        [HttpGet]
        public void CheckSubTask(int id, string content = null, bool result = true)
        {
            var model = Core.FlowNodeDataManager.GetModel(id);
            if (model == null || model.Submited)
            {
                throw new Exception("没有需要审批的流程");
            }
            model.Content = content;
            model.Result = result;
            var subTask = Core.TaskManager.GetSubTask(model.ExtendId);
            //更新自己当前的流程状态
            Core.FlowNodeDataManager.Submit(model);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = subTask.TaskId,
                Status = FlowStatus.Done,
                UserId = model.UserId
            });

            int toUserId = 0;
            if (result)
            {
                subTask.Status = SubTaskStatus.Complete;
                //如果是是主办科室，则需要发给局领导，如果是协办科室，则结束
                if (subTask.IsMaster)
                {
                    //判断该Task的其他的主办是否全部完成，如果是，则发给局领导
                    var list = Core.TaskManager.GetSubTaskList(subTask.TaskId, 0).ToList();
                    var allCompleted = list.Where(e => e.ID != subTask.ID).All(e => e.Status == SubTaskStatus.Complete);
                    if (allCompleted)
                    {
                        var info = Core.FormInfoManager.GetModel(subTask.TaskId);
                        var flowData = info.FlowData;
                        //局领导的ID在主流程的最后一步
                        var flowNode = flowData.Flow.GetLastNode();
                        var user = Core.FlowNodeManager.GetUserList(flowNode).FirstOrDefault();
                        if (user == null)
                        {
                            throw new Exception("流程未配置局领导ID");
                        }

                        toUserId = flowNode.UserIds[0];
                        Core.FlowNodeDataManager.CreateNodeData(flowData.ID, flowNode, toUserId);
                        Core.FeedManager.Save(new Feed
                        {
                            FromUserId = CurrentUser.ID,
                            ToUserId = toUserId,
                            Action = UserAction.Submit,
                            Type = FeedType.Flow,
                            InfoId = subTask.TaskId,
                            Title = "[任务审批] " + info.Title
                        });
                    }
                }
                Core.FeedManager.Save(new Feed
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = subTask.ToUserId,
                    Action = UserAction.Submit,
                    Type = FeedType.Flow,
                    InfoId = subTask.TaskId,
                    Title = "[任务完成] " + subTask.Content
                });
            }
            else
            {
                subTask.Status = SubTaskStatus.Back;
                var parentFlowNodeData = Core.FlowNodeDataManager.GetModel(model.ParentId);
                toUserId = parentFlowNodeData.UserId;
                Core.FlowNodeDataManager.CreateChildNodeData(model, toUserId, subTask.ID);
                Core.FeedManager.Save(new Feed
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = toUserId,
                    Action = UserAction.Submit,
                    Type = FeedType.Flow,
                    InfoId = subTask.TaskId,
                    Title = "[任务失败] " + subTask.Content
                });
            }
            if (toUserId > 0)
            {
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = subTask.TaskId,
                    Status = FlowStatus.Back,
                    UserId = toUserId
                });
            }
        }

        [HttpPost]
        public void SaveTodo(TaskTodo model)
        {
            var subTask = Core.TaskManager.GetSubTask(model.SubTaskId);
            model.CreatorId = CurrentUser.ID;
            Core.TaskManager.SaveTodo(model);

            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = subTask.TaskId,
                UserId = model.ToUserId,
                Status = FlowStatus.Doing
            });

            Core.FeedManager.Save(new Feed
            {
                InfoId = subTask.TaskId,
                ToUserId = model.ToUserId,
                Title = model.Content,
                FromUserId = CurrentUser.ID,
                Type = FeedType.Task,
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
            var subTask = Core.TaskManager.GetSubTask(model.SubTaskId);
            var infoId = subTask.TaskId;
            Core.TaskManager.DeleteTodo(model);
            if (!subTask.Todos.Any(e => e.ToUserId == model.ToUserId))
            {
                Core.UserFormInfoManager.Delete(infoId, model.ToUserId);
                Core.FeedManager.Delete(new Feed { InfoId = infoId, ToUserId = model.ToUserId, FromUserId = model.CreatorId });
            }
        }
    }
}
