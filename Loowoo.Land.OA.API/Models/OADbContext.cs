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

        public DbSet<User> Users { get; set; }
        public DbSet<Diary> Diarys { get; set; }

        #region  公文部分
        public DbSet<ReceiveDocument> Receive_Documents { get; set; }
        public DbSet<SendDocument> Send_Documents { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowStep> Flow_Steps { get; set; }

        #endregion
    }
}