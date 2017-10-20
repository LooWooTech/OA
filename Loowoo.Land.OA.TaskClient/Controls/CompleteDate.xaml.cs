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
    /// Interaction logic for CompleteDate.xaml
    /// </summary>
    public partial class CompleteDate : UserControl
    {
        public CompleteDate()
        {
            InitializeComponent();
        }

        public DateTime? Date { get; set; }

        public CompleteDate(DateTime? date) : this()
        {
            Date = date;
            ctrMonth.Content = date.HasValue ? date.Value.Month + "月" : "-";
            ctrDay.Content = date.HasValue ? date.Value.Day.ToString() : "待定";
        }
    }
}
