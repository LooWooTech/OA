using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System.Linq;
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
        public void UpdateZRR(int id)
        {
            var info = Core.FormInfoManager.GetModel(id);
            //判断当前流程是不是走到了第二步，如果是，第二步的接收人为任务的责任人
            if (info.FlowData.Nodes.Count == 2)
            {
                var lastNodeData = info.FlowData.Nodes[1];
                var model = Core.TaskManager.GetModel(id);
                model.ZRR_ID = lastNodeData.UserId;
                Core.TaskManager.Save(model);
            }
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
                    e.MC,
                    e.JH_SJ,
                    e.LY,
                    e.LY_LX,
                    e.XB_DW,
                    e.ZB_DW,
                    ZRR_Name = e.ZRR == null ? null : e.ZRR.RealName,
                    e.GZ_MB,
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
                    Title = data.MC
                };
                Core.FormInfoManager.Save(data.Info);
            }
            else
            {
                data.Info = Core.FormInfoManager.GetModel(data.ID);
                data.Info.Title = data.MC;
            }
            if (data.Info.FlowDataId == 0)
            {
                data.Info.Form = form;
                Core.FlowDataManager.CreateFlowData(data.Info);
            }
            data.ID = data.Info.ID;
            Core.TaskManager.Save(data);

            Core.FeedManager.Save(new Feed
            {
                InfoId = data.ID,
                Title = data.MC,
                Description = data.GZ_MB,
                FromUserId = CurrentUser.ID,
                Action = isAdd ? UserAction.Create : UserAction.Update,
            });
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.TaskManager.Delete(id);
        }

    }
}
