using Loowoo.Land.OA.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class ManagerCore
    {
        public static readonly ManagerCore Instance = new ManagerCore();

        private ManagerCore()
        {
            foreach (var p in this.GetType().GetProperties())
            {
                if (p.PropertyType == this.GetType())
                {
                    continue;
                }
                var val = p.GetValue(this);
                if (val == null)
                {
                    p.SetValue(this, Activator.CreateInstance(p.PropertyType));
                }
            }
        }
     

        public UserManager UserManager { get; private set; }

        #region 流程
        public FormManager FormManager { get; private set; }

        #endregion

        public DepartmentManager DepartmentManager { get; private set; }
        public GroupManager GroupManager { get; private set; }

        public UserGroupManager User_GroupManager { get; private set; }
        public DiaryManager DiaryManager { get; private set; }

        public InfoTypeManager InfoTypeManager { get; private set; }
        public StepManager StepManager { get; set; }
        #region  公文部分
        public MissiveManager MissiveManager { get; private set; }


        /// <summary>
        /// 收文
        /// </summary>
        public ReceiveDocumentManager Receive_DocumentManager { get; private set; }
        /// <summary>
        /// 发文
        /// </summary>
        public SendDocumentManager Send_DocumentManager { get; private set; }

        public FlowStepManager FlowStepManager { get; private set; }

        #endregion


        #region 任务部分
        public TaskManager TaskManager { get; private set; }

        #endregion

        #region  车辆部分
        public CarManager CarManager { get; private set; }
        public Car_EventLogManager Car_EventLogManager { get; private set; }
        #endregion

        #region 动态部分
        public FeedManager FeedManager { get; private set; }
        #endregion

        #region 会议部分
        public Meeting_RoomManager Meeting_RoomManager { get; private set; }
        public MeetingManager MeetingManager { get; private set; }
        #endregion


        public DocumentManager DocumentManager { get; private set; }
        public FlowManager FlowManager { get; private set; }

        public FileManager FileManager { get; private set; }
        
    }
}