using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Loowoo.Land.OA.TaskClient.Models;

namespace Loowoo.Land.OA.TaskClient.Controls
{
    /// <summary>
    /// Interaction logic for SubTaskList.xaml
    /// </summary>
    public partial class SubTaskList : UserControl
    {
        public SubTaskList()
        {
            InitializeComponent();
        }

        public void UpdateControl(MasterTaskViewModel master)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                container.Children.Clear();
                var i = 0;
                var totalRows = 0;
                foreach (var child in master.Children)
                {
                    var isLast = i == master.Children.Count - 1;
                    var height = child.Rows * Config.RowHeight;
                    if (isLast)
                    {
                        if (totalRows + child.Rows < master.Rows)
                        {
                            height = (master.Rows - totalRows) * Config.RowHeight;
                        }
                    }
                    totalRows += child.Rows;
                    container.Children.Add(new SubTaskRow(child)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = height,
                        Margin = new Thickness(0, Config.RowHeight * i, 0, 0)
                    });
                    i++;
                }
            }));
        }
    }
}
