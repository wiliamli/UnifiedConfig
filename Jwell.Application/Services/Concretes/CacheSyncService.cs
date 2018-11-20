using Jwell.Application.Services.Interfaces;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Modules.Cache;
using Jwell.Repository.Repositories;
using Polly;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Concretes
{
    /// <summary>
    /// 缓存同步-服务-接口
    /// </summary>
    public class CacheSyncService : ApplicationService, ICacheSyncService
    {
        //private IConfigRepository ConfigRepository { get; set; }

        private IConfigItemRepository ConfigItemRepository { get; set; }

        private IHCacheClient CacheClient { get; set; }

        public CacheSyncService(IConfigItemRepository configItemRepository,IHCacheClient cacheClient)
        {
            this.ConfigItemRepository = configItemRepository;
            this.CacheClient = cacheClient;
        }

        public bool SyncConfigData(int retryTime = 2)
        {
            try
            {
                Policy.Handle<Exception>().Retry(retryTime).Execute(() => TrySyncConfigData());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void TrySyncConfigData()
        {
            var query = from config in this.ConfigItemRepository.Queryable()
                        where config.IsDeleted == false && config.Status == ConfigItemStatus.Pushed
                        group config by new { serviceNumber = config.ServiceNumber, Env = config.ConfigEnvironment } into configs
                        select new
                        {
                            ServiceNumber = configs.Key.serviceNumber,
                            Env = configs.Key.Env,
                            Items = from item in configs
                                    select new
                                    {
                                        Key = item.Key,
                                        Value = item.Value
                                    }
                        };
          
            var queryResult = query.ToList();
            foreach (var syncData in queryResult)
            {
                ConcurrentDictionary<string, string> conDictionary = new ConcurrentDictionary<string, string>();
                var hashId = syncData.ServiceNumber.ToLower() + syncData.Env.ToString().ToLower();
                foreach (var item in syncData.Items)
                {
                    conDictionary.TryAdd(item.Key, item.Value);
                }
                if (conDictionary.Count() > 0)
                    CacheClient.SetHT(hashId, conDictionary);
            }
        }
    }
}
