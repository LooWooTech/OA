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
        private FileManager _fileManager { get; set; }
        public FileManager FileManager
        {
            get { return _fileManager == null ? _fileManager = new FileManager() : _fileManager; }
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
        private FlowManager _flowManager { get; set; }
        public FlowManager FlowManager
        {
            get { return _flowManager == null ? _flowManager = new FlowManager() : _flowManager; }
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
        private FlowNodeDataManager _flowNodeDataManager { get; set; }
        public FlowNodeDataManager FlowNodeDataManager
        {
            get { return _flowNodeDataManager == null ? _flowNodeDataManager = new FlowNodeDataManager() : _flowNodeDataManager; }
        }
        private UserFormManager _userFormManager { get; set; }
        public UserFormManager UserFormManager
        {
            get { return _userFormManager == null ? _userFormManager = new UserFormManager() : _userFormManager; }
        }
        #endregion
        #region  公文部分
        private MissiveManager _missiveManager { get; set; }
        public MissiveManager MissiveManager
        {
            get { return _missiveManager == null ? _missiveManager = new MissiveManager() : _missiveManager; }
        }
        



        #endregion




        public DiaryManager DiaryManager { get; private set; }

        public InfoTypeManager InfoTypeManager { get; private set; }
        public StepManager StepManager { get; set; }



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
      

        /// <summary>
        /// 收文
        /// </summary>
        public ReceiveDocumentManager Receive_DocumentManager { get; private set; }
        /// <summary>
        /// 发文
        /// </summary>
        public SendDocumentManager Send_DocumentManager { get; private set; }

        public FlowStepManager FlowStepManager { get; private set; }

    }
}