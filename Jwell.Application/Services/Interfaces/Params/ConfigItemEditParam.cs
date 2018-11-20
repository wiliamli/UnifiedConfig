using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 配置项编辑参数
    /// </summary>
    public class ConfigItemEditParam 
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}
