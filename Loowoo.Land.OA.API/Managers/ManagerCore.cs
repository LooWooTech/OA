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
        public DiaryManager DiaryManager { get; private set; }

        #region  公文部分
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

        public DocumentManager DocumentManager { get; private set; }
        public FlowManager FlowManager { get; private set; }
        
    }
}