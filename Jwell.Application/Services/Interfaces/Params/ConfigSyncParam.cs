using Jwell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces.Params
{
    /// <summary>
    /// 配置同步参数
    /// </summary>
    public class ConfigSyncParam
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        [Required]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 被同步的配置环境
        /// </summary>
        [Required]
        public ConfigEnvironment FromEnv { get; set; }

        /// <summary>
        /// 同步的配置环境
        /// </summary>
        [Required]
        public ConfigEnvironment ToEnv { get; set; }
    }
}
