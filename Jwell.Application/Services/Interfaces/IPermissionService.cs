using Jwell.Application.Services.Params;
using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 成员服务-服务-接口
    /// </summary>
    public interface IPermissionService : IApplicationService
    {
        /// <summary>
        /// 跟新服务配置管理权限
        /// </summary>
        /// <param name="param">服务权限添加参数</param>
        /// <returns></returns>
        bool UpdateServicePermission(ServiceMemberAddParam param);

        /// <summary>
        /// 获取拥有本服务配置管理权限的人员
        /// </summary>
        /// <param name="serviceNumber">服务编号</param>
        /// <returns></returns>
        List<string> GetMemberHasAuthor(string serviceNumber);
    }
}
