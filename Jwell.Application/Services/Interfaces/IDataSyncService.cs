using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces
{
    /// <summary>
    /// 数据同步-服务-接口
    /// </summary>
    public interface IDataSyncService : IApplicationService
    {
        /// <summary>
        /// 从缓存同步服务管理的数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        void SyncDataFromCache();

        /// <summary>
        /// 从消息队列同步服务管理的数据
        /// </summary>
        /// <param name="value"></param>
        void SyncDataFromMsgQueue(string value);
    }
}
