using Jwell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 配置项添加参数
    /// </summary>
    public class ConfigItemAddParam
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键是必须的")]
        [RegularExpression(@"[^`~!@#$^&*()=|{}':;',?]+", ErrorMessage ="不能包含特殊字符")]
        public string Key { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值是必须的")]
        public string Value { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        [Required(ErrorMessage = "服务编号是必须的")]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 配置环境
        /// </summary>
        [Required(ErrorMessage = "配置环境是必须的")]
        public ConfigEnvironment Env { get; set; }
    }
}
