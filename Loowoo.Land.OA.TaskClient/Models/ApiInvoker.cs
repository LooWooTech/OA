using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.TaskClient.Models
{
    public class ApiInvoker
    {
        private string ApiHost;

        private string ApiTaskUrl;
        private string ApiSubTaskUrl;

        public ApiInvoker()
        {
            ApiHost = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
            ApiTaskUrl = $"{ApiHost}/api/task/TasksForLED";
            ApiSubTaskUrl = $"{ApiHost}/api/task/SubTasksForLED";
        }

        private System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();

        public async Task<List<Task>> GetTasks()
        {
            var json = await Client.GetStringAsync(ApiTaskUrl);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Task>>(json);
        }

        public async Task<List<SubTask>> GetSubTasks(int taskId)
        {
            var json = await Client.GetStringAsync(ApiSubTaskUrl + "?taskId=" + taskId);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SubTask>>(json);
        }
    }
}
