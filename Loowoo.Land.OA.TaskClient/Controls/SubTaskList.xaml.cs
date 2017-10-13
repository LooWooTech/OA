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

        public void UpdateControl(List<SubTaskViewModel> rows)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                container.Children.Clear();
                var i = 0;
                foreach (var row in rows)
                {
                    container.Children.Add(new SubTaskRow(row)
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = row.Rows * Config.RowHeight,
                        Margin = new Thickness(0, Config.RowHeight * i, 0, 0)
                    });
                    i++;
                }
            }));
        }
    }
}
