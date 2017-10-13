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
                ctrTaskList.Children.Clear();

                var marginTop = 0;
                foreach (var row in model.Rows)
                {
                    ctrTaskList.Children.Add(new TaskTableRow(row)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = row.RowsHeight * Config.RowHeight,
                        Margin = new Thickness(0, marginTop, 0, 0)
                    });
                    marginTop += row.RowsHeight * Config.RowHeight;
                }
            }));
        }

        public void Play()
        {
            ctrLoading.Visibility = Visibility.Hidden;
            ctrTaskList.Opacity = 100;
        }

        public void Stop()
        {
            ctrTaskList.Opacity = 0;
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
