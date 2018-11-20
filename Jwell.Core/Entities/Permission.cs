using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jwell.Domain.Entities.Base;

namespace Jwell.Domain.Entities
{
    /// <summary>
    /// 成员-服务中间表
    /// </summary>
    [Table("Config_Permission")]
    public class Permission : BaseEntity
    {
        /// <summary>
        /// 成员编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string EmployeeID { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string ServiceNumber { get; set; }
    }
}
