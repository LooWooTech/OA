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
    /// Interaction logic for SubTaskStatus.xaml
    /// </summary>
    public partial class SubTaskStatus : UserControl
    {
        public SubTaskStatus()
        {
            InitializeComponent();
            ctrDoingStatus.FontSize = ctrDoneStatus.FontSize = Config.FontSize > Config.MaxFontSize ? Config.MaxFontSize : Config.FontSize;
        }

        public void UpdateControl(Models.SubTaskStatus status)
        {
            ctrDoingStatus.Visibility = status == Models.SubTaskStatus.Doing ? Visibility.Visible : Visibility.Hidden;
            ctrDoneStatus.Visibility = status == Models.SubTaskStatus.Complete ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
