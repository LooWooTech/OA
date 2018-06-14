using Loowoo.Land.OA.TaskClient.Models;
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

namespace Loowoo.Land.OA.TaskClient.Controls
{
    /// <summary>
    /// Interaction logic for SubTaskRow.xaml
    /// </summary>
    public partial class SubTaskRow : UserControl
    {
        public SubTaskRow()
        {
            InitializeComponent();
            ctrName.FontSize = Config.FontSize;
        }

        public SubTaskRow(Models.SubTaskViewModel row) : this()
        {
            if (string.IsNullOrEmpty(row.Name))
            {
                container.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
            }
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ctrName.Text = row.Name.Replace("\n", " ");
                ctrStatus.UpdateControl(row.Status);
            }));
        }
    }
}
