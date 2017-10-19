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

namespace Loowoo.Land.OA.TaskClient.Controls
{
    /// <summary>
    /// Interaction logic for TaskTable.xaml
    /// </summary>
    public partial class TaskTable : UserControl
    {
        public TaskTable()
        {
            InitializeComponent();
        }

        public TaskViewModel Model { get; set; }

        public void UpdateModel(TaskViewModel model)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Model = model;
                ctrTaskName.Content = "项目内容：" + model.TaskName.Replace("\n", " ");
                container_task.Children.Clear();
                container_date.Children.Clear();
                container_department.Children.Clear();
                var marginTop = 0;
                CompleteDate prevDateControl = null;
                TaskDepartment prevDepartment = null;
                foreach (var row in model.Children)
                {
                    //添加任务列表
                    container_task.Children.Add(new TaskTableRow(row)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = row.Rows * Config.RowHeight,
                        Margin = new Thickness(0, marginTop, 0, 0)
                    });
                    //添加日期列表，如果日期重复，则合并
                    var newDateControl = new CompleteDate(row.ScheduleDate)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = row.Rows * Config.RowHeight,
                        Margin = new Thickness(0, marginTop, 0, 0)
                    };
                    if (prevDateControl != null && newDateControl.ctrDate.Content.ToString() == prevDateControl.ctrDate.Content.ToString())
                    {
                        prevDateControl.Height += newDateControl.Height;
                    }
                    else
                    {
                        prevDateControl = newDateControl;
                        container_date.Children.Add(newDateControl);
                    }
                    //添加部门列表，如果重复则合并
                    var newDepartment = new TaskDepartment(row.Department)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = row.Rows * Config.RowHeight,
                        Margin = new Thickness(0, marginTop, 0, 0)
                    };
                    if (prevDepartment != null && newDepartment.ctrName.Text == prevDepartment.ctrName.Text)
                    {
                        prevDepartment.Height += newDepartment.Height;
                    }
                    else
                    {
                        prevDepartment = newDepartment;
                        container_department.Children.Add(newDepartment);
                    }

                    marginTop += row.Rows * Config.RowHeight;

                }
            }));
        }

        public void Play()
        {
            ctrLoading.Visibility = Visibility.Hidden;
            container_task.Opacity = 100;
        }

        public void Stop()
        {
            container_task.Opacity = 0;
        }

        public async System.Threading.Tasks.Task Await(int seconds)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                Thread.Sleep(1000 * seconds);
            });
        }

        public void PlayMessage(string msg = "正在加载...")
        {
            ctrLoading.ctrMessage.Content = msg;
            ctrLoading.Visibility = Visibility.Visible;
        }
    }
}
