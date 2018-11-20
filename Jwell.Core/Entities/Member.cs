using Jwell.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domain.Entities
{
    /// <summary>
    /// 小组成员信息表
    /// </summary>
    [Table("Config_Member")]
    public class Member: BaseEntity
    {
        /// <summary>
        /// 小组编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string TeamNumber { get; set; }

        /// <summary>
        /// 成员编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string MemberNumber { get; set; }

        /// <summary>
        /// 成员名字
        /// </summary>
        [Required]
        [StringLength(16)]
        public string MemberName { get; set; }

        /// <summary>
        /// 成员工号
        /// </summary>
        [Required]
        [StringLength(16)]
        public string EmployeeID { get; set; }

        /// <summary>
        /// 是否为小组leader
        /// </summary>
        [Required]
        public bool IsLeader { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime ModifiedTime { get; set; }
    }
}
