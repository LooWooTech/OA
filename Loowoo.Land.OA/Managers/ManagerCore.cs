using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
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

        public ConfigManager ConfigManager { get; private set; }

        public CategoryManager CategoryManager { get; private set; }
        public FileManager FileManager { get; private set; }

        public UserManager UserManager { get; private set; }
        public GroupManager GroupManager { get; private set; }
        public DepartmentManager DepartmentManager { get; private set; }

        public FormManager FormManager { get; private set; }
        public FormInfoManager FormInfoManager { get; private set; }
        public UserFormInfoManager UserFormInfoManager { get; private set; }
        public FormInfoExtend1Manager FormInfoExtend1Manager { get; private set; }

        public FeedManager FeedManager { get; private set; }
        public MessageManager MessageManager { get; private set; }

        public MailManager MailManager { get; private set; }

        public FlowManager FlowManager { get; private set; }
        public FlowNodeManager FlowNodeManager { get; private set; }
        public FlowDataManager FlowDataManager { get; private set; }
        public FlowNodeDataManager FlowNodeDataManager { get; private set; }

        public FreeFlowManager FreeFlowManager { get; private set; }
        public FreeFlowDataManager FreeFlowDataManager { get; private set; }
        public FreeFlowNodeDataManager FreeFlowNodeDataManager { get; private set; }

        public HolidayManager HolidayManager { get; private set; }
        public AttendanceManager AttendanceManager { get; private set; }
        public JobTitleManager JobTitleManager { get; private set; }

        public CarManager CarManager { get; private set; }
        public MeetingRoomManager MeetingRoomManager { get; private set; }
        public SealManager SealManager { get; private set; }

        public FeedManager UserActionManager { get; private set; }

        public MissiveManager MissiveManager { get; private set; }
        public TaskManager TaskManager { get; private set; }

        public SmsManager SmsManager { get; private set; }

        public SalaryManager SalaryManager { get; private set; }
    }
}
