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

        //private ManagerCore()
        //{
        //    foreach (var p in this.GetType().GetProperties())
        //    {
        //        if (p.PropertyType == this.GetType())
        //        {
        //            continue;
        //        }
        //        var val = p.GetValue(this);
        //        if (val == null)
        //        {
        //            p.SetValue(this, Activator.CreateInstance(p.PropertyType));
        //        }
        //    }
        //}

       

        #region  基础部分
        private CategoryManager _categoryManager { get; set; }
        public CategoryManager CategoryManager
        {
            get { return _categoryManager == null ? _categoryManager = new CategoryManager() : _categoryManager; }
        }
        private DepartmentManager _departmentManager { get; set; }
        public DepartmentManager DepartmentManager
        {
            get { return _departmentManager == null ? _departmentManager = new DepartmentManager() : _departmentManager; }
        }
        private ConfidentialLevelManager _confidentialLevelManager { get; set; }
        public ConfidentialLevelManager ConfidentialLevelManager
        {
            get { return _confidentialLevelManager == null ? _confidentialLevelManager = new ConfidentialLevelManager() : _confidentialLevelManager; }
        }
        private EmergencyManager _emergencyManager { get; set; }
        public EmergencyManager EmergencyManager
        {
            get { return _emergencyManager == null ? _emergencyManager = new EmergencyManager() : _emergencyManager; }
        }
        private SubScriptionManager _subScriptionManager { get; set; }
        public SubScriptionManager SubScriptionManager
        {
            get { return _subScriptionManager == null ? _subScriptionManager = new SubScriptionManager() : _subScriptionManager; }
        }

        #endregion


        #region  用户 组

        private UserManager _userManager { get; set; }
        public UserManager UserManager
        {
            get { return _userManager == null ? _userManager = new Managers.UserManager() : _userManager; }
        }
        private GroupManager _groupManager { get; set; }
        public GroupManager GroupManager
        {
            get { return _groupManager == null ? _groupManager = new GroupManager() : _groupManager; }
        }
        private UserGroupManager _userGroupManager { get; set; }
        public UserGroupManager User_GroupManager
        {
            get { return _userGroupManager == null ? _userGroupManager = new UserGroupManager() : _userGroupManager; }
        }
        private FeedManager _feedManager { get; set; }
        public FeedManager FeedManager
        {
            get { return _feedManager == null ? _feedManager = new Managers.FeedManager() : _feedManager; }
        }
        #endregion


        #region 流程
        private FormManager _formManager { get; set; }
        public FormManager FormManager
        {
            get { return _formManager == null ? _formManager = new FormManager() : _formManager; }
        }
        private FlowDataManager _flowDataManager { get; set; }
        public FlowDataManager FlowDataManager
        {
            get { return _flowDataManager == null ? _flowDataManager = new FlowDataManager() : _flowDataManager; }
        }

        private FlowNodeManager _flowNodeManager { get; set; }
        public FlowNodeManager FlowNodeManager
        {
            get { return _flowNodeManager == null ? _flowNodeManager = new FlowNodeManager() : _flowNodeManager; }
        }
        #endregion

       
    

    
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



        #region 会议部分
        public Meeting_RoomManager Meeting_RoomManager { get; private set; }
        public MeetingManager MeetingManager { get; private set; }
        #endregion


        public DocumentManager DocumentManager { get; private set; }
        public FlowManager FlowManager { get; private set; }

        public FileManager FileManager { get; private set; }
        
    }
}