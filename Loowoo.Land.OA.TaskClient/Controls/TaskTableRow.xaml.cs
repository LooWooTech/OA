﻿using Loowoo.Land.OA.TaskClient.Models;
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
            ctrCompleteDate.FontSize = Config.FontSize;
            ctrTaskList.FontSize = Config.FontSize;
            ctrMasterDepartment.FontSize = Config.FontSize;
            ctrMasterTaskName.FontSize = Config.FontSize;
        }

        public TaskTableRow(Models.MasterTaskViewModel row) : this()
        {
            Model = row;
            Dispatcher.Invoke(UpdateControl);
        }

        public Models.MasterTaskViewModel Model { get; set; }

        private void UpdateControl()
        {
            ctrCompleteDate.UpdateControl(Model.ScheduleDate.HasValue ? Model.ScheduleDate.Value.ToString("M月d日") : "待定");
            ctrMasterDepartment.Text = Model.Department;
            if (Model.RowsHeight > 1)
            {
                ctrMasterTaskName.TextWrapping = TextWrapping.WrapWithOverflow;
            }
            ctrMasterTaskName.Text = Model.TaskName.Replace("\n"," ");
            ctrTaskList.UpdateControl(Model.Rows);
        }
    }
}