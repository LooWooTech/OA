using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FormManager : ManagerBase
    {
        public Form GetModel(int formId)
        {
            return DB.Forms.FirstOrDefault(e => e.ID == formId);
        }
        /// <summary>
        /// 作用：获取表单列表
        /// 作者：汪建龙
        /// 编写时间：2017年3月22日11:38:44
        /// </summary>
        /// <returns></returns>
        public List<Form> GetList()
        {
            return DB.Forms.OrderBy(e => e.ID).ToList();
        }
    }
}