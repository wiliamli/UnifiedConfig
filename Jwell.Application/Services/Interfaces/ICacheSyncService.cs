using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces
{
    /// <summary>
    /// 缓存同步-服务-接口
    /// </summary>
    public interface ICacheSyncService: IApplicationService
    {
        /// <summary>
        /// 同步配置数据
        /// </summary>
        /// <returns></returns>
        bool SyncConfigData(int retryTime = 2);
    }
}
