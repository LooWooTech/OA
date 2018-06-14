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
    /// Interaction logic for TaskDepartment.xaml
    /// </summary>
    public partial class TaskDepartment : UserControl
    {
        public TaskDepartment()
        {
            InitializeComponent();
            ctrName.FontSize = Config.FontSize > Config.MaxFontSize ? Config.MaxFontSize : Config.FontSize;
        }

        public TaskDepartment(string name):this()
        {
            ctrName.Text = name;
        }
    }
}
