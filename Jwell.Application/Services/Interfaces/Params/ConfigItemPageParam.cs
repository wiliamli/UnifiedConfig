using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 配置分页查询参数
    /// </summary>
    public class ConfigItemPageParam : PageParam
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 配置编号
        /// </summary>
        [Required]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 配置环境
        /// </summary>
        [Required]
        public ConfigEnvironment Env { get; set; }
    }
}
