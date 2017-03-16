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

        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowNode> FlowNodes { get; set; }
        public DbSet<FlowData> Flow_Datas { get; set; }
        public DbSet<FlowNodeData> Flow_Node_Datas { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> User_Groups { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Subscription> SubScriptions { get; set; }
    }
}
