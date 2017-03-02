using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class OADbContext:DbContext
    {
        public OADbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<OADbContext>(null);
        }

      

        #region  基础部分
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ConfidentialLevel> ConfidentialLevels { get; set; }
        public DbSet<Emergency> Emergencys { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subscription> SubScriptions { get; set; }
        public DbSet<File> Files { get; set; }
        #endregion

        #region  用户 组

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> User_Groups { get; set; }
        #endregion


        #region 流程

        public DbSet<Form> Forms { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowNode> Flow_Nodes { get; set; }
        public DbSet<FlowData> Flow_Datas { get; set; }
        public DbSet<FlowNodeData> Flow_Node_Datas { get; set; }
        public DbSet<UserForm> User_Forms { get; set; }


        #endregion

        public DbSet<Step> Steps { get; set; }
        public DbSet<StepUser> Step_Uer { get; set; }
        public DbSet<InfoType> InfoTypes { get; set; }

        public DbSet<Diary> Diarys { get; set; }

  


        #region  公文部分
        public DbSet<Missive> Missives { get; set; }

        public DbSet<ReceiveDocument> Receive_Documents { get; set; }
        public DbSet<SendDocument> Send_Documents { get; set; }
      
        public DbSet<FlowStep> Flow_Steps { get; set; }

        #endregion

        #region  公文部分

        public DbSet<Task> Tasks { get; set; }
        #endregion


        #region  车辆部分

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarEventLog> Car_Eventlogs { get; set; }
        #endregion

        #region  动态消息部分
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Message> Messages { get; set; }
        #endregion


        #region  会议
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingRoom> Meeting_Rooms { get; set; }
        #endregion

    }
}