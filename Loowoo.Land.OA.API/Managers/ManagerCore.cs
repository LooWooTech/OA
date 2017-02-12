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
        public DocumentManager DocumentManager { get; private set; }
        public FlowManager FlowManager { get; private set; }
        public FlowStepManager FlowStepManager { get; private set; }
    }
}