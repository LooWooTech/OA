using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("freeflow")]
    public class FreeFlow
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DepartmentLimitMode LimitMode { get; set; }

        /// <summary>
        /// 跨部门
        /// </summary>
        public bool CrossDepartment { get; set; }
        /// <summary>
        /// 跨级别
        /// </summary>
        public bool CrossLevel { get; set; }

        [Column("DepartmentIds")]
        public string DepartmentIdsValue { get; set; }
        [NotMapped]
        public int[] DepartmentIds
        {
            get
            {
                return DepartmentIdsValue.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    DepartmentIdsValue = null;
                else
                    DepartmentIdsValue = string.Join(",", value);
            }
        }

        [Column("CompleteUserDepartmentIds")]
        public string CompleteUserDepartmentIdsValue { get; set; }
        [NotMapped]
        public int[] CompleteUserDepartmentIds
        {
            get
            {
                return CompleteUserDepartmentIdsValue.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    CompleteUserDepartmentIdsValue = null;
                else
                    CompleteUserDepartmentIdsValue = string.Join(",", value);
            }
        }

        [Column("CompleteUserJobTitleIds")]
        public string CompleteUserJobTitleIdsValue { get; set; }
        [NotMapped]
        public int[] CompleteUserJobTitleIds
        {
            get
            {
                return CompleteUserJobTitleIdsValue.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    CompleteUserJobTitleIdsValue = null;
                else
                    CompleteUserJobTitleIdsValue = string.Join(",", value);
            }
        }

        public bool IsCompleteUser(User user)
        {
            var inDepartment = CompleteUserDepartmentIds != null && user.DepartmentIds != null && CompleteUserDepartmentIds.Any(id => user.DepartmentIds.Contains(id));
            var inJobTitle = CompleteUserJobTitleIds != null && CompleteUserJobTitleIds.Contains(user.JobTitleId);
            if (CompleteUserDepartmentIds != null && CompleteUserJobTitleIds != null)
            {
                return inDepartment && inJobTitle;
            }
            else if (CompleteUserDepartmentIds != null)
            {
                return inDepartment;
            }
            else if (CompleteUserJobTitleIds != null)
            {
                return inJobTitle;
            }
            return false;
        }
    }

    public enum DepartmentLimitMode
    {
        [Description("指定部门")]
        Assign = 1,
        [Description("拟稿人部门")]
        Poster = 2,
        [Description("自己部门")]
        Self = 3,
    }
}
