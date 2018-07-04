using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class OADbContext : DbContext
    {
        public OADbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<OADbContext>(null);
        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<File> Files { get; set; }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormInfo> FormInfos { get; set; }
        public DbSet<UserFormInfo> UserFormInfos { get; set; }
        public DbSet<FormInfoExtend1> FormInfoExtend1s { get; set; }

        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowNode> FlowNodes { get; set; }
        public DbSet<FlowData> FlowDatas { get; set; }
        public DbSet<FlowNodeData> FlowNodeDatas { get; set; }

        public DbSet<FreeFlow> FreeFlows { get; set; }
        public DbSet<FreeFlowData> FreeFlowDatas { get; set; }
        public DbSet<FreeFlowNodeData> FreeFlowNodeDatas { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<UserFlowContact> UserFlowContacts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupRight> GroupRights { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Seal> Seals { get; set; }

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<UserMail> UserMails { get; set; }

        public DbSet<Subscription> SubScriptions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceGroup> AttendanceGroups { get; set; }
        public DbSet<CheckInOut> CheckInOuts { get; set; }

        public DbSet<Missive> Missives { get; set; }
        public DbSet<MissiveRedTitle> MissiveRedTitles { get; set; }
        public DbSet<MissiveServiceLog> MissiveServiceLogs { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskTodo> Todos { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        public DbSet<Config> Configs { get; set; }

        public DbSet<Sms> Sms { get; set; }

        public DbSet<Salary> Salaries { get; set; }
        public DbSet<SalaryData> SalaryDatas { get; set; }


        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<UserMissive> UserMissives { get; set; }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsApply> GoodsApplies { get; set; }
        public DbSet<GoodsRegister> GoodsRegisters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
