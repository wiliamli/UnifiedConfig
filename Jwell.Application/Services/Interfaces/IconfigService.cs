using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 配置-服务-接口
    /// </summary>
    public interface IConfigService : IApplicationService
    {
        /// <summary>
        /// 获取配置分页数据
        /// </summary>
        /// <param name="pageParam">分页参数</param>
        /// <param name="serviceNumber">服务编号</param>
        /// <param name="env">配置环境</param>
        /// <returns></returns>
        PageResult<ConfigItemDto> GetDtos(ConfigItemPageParam pageParam, string serviceNumber, ConfigEnvironment env);

        /// <summary>
        /// 同步配置环境
        /// </summary>
        /// <param name="serviceNumber">服务编号</param>
        /// <param name="fromEnv">被同步配置环境</param>
        /// <param name="toEnv">同步配置环境</param>
        /// <param name="employId">同步人</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        bool SyncConfig(string serviceNumber, ConfigEnvironment fromEnv, ConfigEnvironment toEnv, string employId,ref string errorMsg);
    }
}
