using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 成员-服务添加参数
    /// </summary>
    public class ServiceMemberAddParam
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        [Required]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 成员编号
        /// </summary>
        [Required]
        public string[] EmployeeIDs { get; set; }
    }
}
