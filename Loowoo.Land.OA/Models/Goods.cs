using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("goods")]
    public class Goods
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 还剩数量
        /// </summary>
        public int Number { get; set; }

        public int FreezeNumber { get; set; }
        /// <summary>
        /// 物品介绍
        /// </summary>
        public string Description { get; set; }

        public int PictureId { get; set; }

        public GoodsStatus Status { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }

    [Table("goods_register")]
    public class GoodsRegister
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int GoodsId { get; set; }

        public int UserId { get; set; }

        public int Number { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

    public enum GoodsStatus
    {
        Disabled,
        Enabled
    }

    [Table("goods_apply")]
    public class GoodsApply
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public virtual FormInfo Info { get; set; }

        public int GoodsId { get; set; }

        public virtual Goods Goods { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }

        public int ApplyUserId { get; set; }

        public virtual User ApplyUser { get; set; }

        public int ApprovalUserId { get; set; }

        public virtual User ApprovalUser { get; set; }

        public bool? Result { get; set; }
    }

    public class GoodsParameter
    {
        public int CategoryId { get; set; }

        public string SearchKey { get; set; }

        public PageParameter Page { get; set; }
    }

    public class GoodsApplyParameter
    {
        public int GoodsId { get; set; }

        public int ApplyUserId { get; set; }

        public int ApprovalUserId { get; set; }

        public CheckStatus? Status { get; set; }

        public PageParameter Page { get; set; }
    }
}
