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
    /// Interaction logic for TaskTableRow.xaml
    /// </summary>
    public partial class TaskTableRow : UserControl
    {
        public TaskTableRow()
        {
            InitializeComponent();
            ctrMasterTaskName.FontSize = Config.FontSize;
        }

        public TaskTableRow(MasterTaskViewModel row) : this()
        {
            Model = row;
            Dispatcher.Invoke(UpdateControl);
        }

        public MasterTaskViewModel Model { get; set; }

        private void UpdateControl()
        {
            if (Model.Children.Count == 0)
            {
                container.ColumnDefinitions[1].Width = new GridLength(26, GridUnitType.Star);
                container.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);
            }
            else if (Model.Children.Count == 1 && Model.Children[0].Name == Model.TaskName)
            {
                Model.Children[0].Name = "";
                container.ColumnDefinitions[1].Width = new GridLength(26, GridUnitType.Star);
                container.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Pixel);
            }
            ctrMasterTaskName.Text = Model.TaskName.Replace("\n"," ");
            ctrTaskList.UpdateControl(Model);
        }
    }
}
