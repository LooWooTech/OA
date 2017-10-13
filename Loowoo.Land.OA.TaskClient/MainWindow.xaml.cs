using Loowoo.Common;
using Loowoo.Land.OA.TaskClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Loowoo.Land.OA.TaskClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private bool _stop = false;

        public async void Start()
        {
            while (!_stop)
            {
                try
                {
                    await Play();
                }
                catch (Exception ex)
                {
                    ctrTaskTable.PlayMessage("连接API失败，暂时无法展示任务内容。");
                    await ctrTaskTable.Await(Config.PlaySeconds);
                    LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}\r\n{ex.ToJson()}\r\n");
                }
            }
        }

        public void Stop()
        {
            _stop = true;
        }

        private async System.Threading.Tasks.Task Play()
        {
            var api = new ApiInvoker();
            ctrTaskTable.PlayMessage("加载数据中...");
            var tasks = await api.GetTasks();
            if (tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    var subTasks = await api.GetSubTasks(task.ID);
                    var models = BuildTaskViewModels(task, subTasks);
                    foreach (var item in models)
                    {
                        ctrTaskTable.UpdateModel(item);
                        ctrTaskTable.Play();
                        await ctrTaskTable.Await(Config.PlaySeconds);
                        ctrTaskTable.Stop();
                    }
                }
            }
            else
            {
                ctrTaskTable.PlayMessage("没有未完成任务");
                await ctrTaskTable.Await(1);
            }
        }

        private void BuildPageData(List<TaskViewModel> list, Models.Task task, IEnumerable<SubTask> masters)
        {
            var taskViewModel = new TaskViewModel { TaskName = task.Name };
            list.Add(taskViewModel);

            var totalRows = 0;
            foreach (var master in masters.OrderBy(e => e.ID))
            {
                var masterViewModel = new MasterTaskViewModel
                {
                    ID = master.ID,
                    ScheduleDate = master.ScheduleDate,
                    TaskName = master.Content,
                    Department = master.ToDepartmentName,
                };

                foreach (var child in master.Children)
                {
                    var childViewModel = new SubTaskViewModel
                    {
                        Name = child.Content,
                        Status = child.Status,
                        Department = child.ToDepartmentName,
                    };
                    if (totalRows + childViewModel.Rows > Config.MaxRows)
                    {
                        break;
                    }
                    else
                    {
                        masterViewModel.Rows.Add(childViewModel);
                        totalRows += childViewModel.Rows;
                    }
                }

                if (masterViewModel.Rows.Count > 0)
                {
                    taskViewModel.Rows.Add(masterViewModel);
                }
                if (totalRows >= Config.MaxRows)
                {
                    totalRows = 0;
                    taskViewModel = new TaskViewModel { TaskName = task.Name };
                    list.Add(taskViewModel);
                }
            }
        }

        private List<TaskViewModel> BuildTaskViewModels(Models.Task task, List<SubTask> subTasks)
        {
            var list = new List<TaskViewModel>();

            var masters = subTasks.Where(e => e.ParentId == 0);
            foreach (var master in masters)
            {
                master.Children = subTasks.Where(e => e.ParentId == master.ID).ToList();
                if (master.Children.Count == 0)
                {
                    master.Children.Add(new SubTask
                    {
                        ParentId = master.ID,
                        Content = master.Content,
                        ToDepartmentName = master.ToDepartmentName,
                        Status = master.Status,
                    });
                }
            }
            BuildPageData(list, task, masters);

            return list;
        }
    }
}
