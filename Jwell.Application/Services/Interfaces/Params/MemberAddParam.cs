using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 成员添加参数
    /// </summary>
    public class MemberAddParam
    {
        /// <summary>
        /// 成员编号
        /// </summary>
        [Required]
        public string EmployeeID { get; set; }

        /// <summary>
        /// 成员名字
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
