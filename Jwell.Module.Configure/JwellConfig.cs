using Jwell.Framework.Configure;
using Jwell.Modules.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Modules.SetupConfig;

namespace Jwell.Modules.Configure
{
    public class JwellConfig
    {
        private static IHCacheClient CacheClient;

        static JwellConfig()
        {
            CacheClient = new HCacheClient();
            CacheClient.DB = ApplicationConstant.CACHEDB;
        }

        /// <summary>
        /// 根据键获取本地对应的配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 根据键获取统一配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            var serviceNumber = Jwell.Modules.SetupConfig.SetupConfig.ServiceNumber;//SetupConfig.ServiceNumber;
            var env = Jwell.Modules.SetupConfig.SetupConfig.Enviroment;
            var hashId = serviceNumber.ToLower() + env.ToLower();
            return CacheClient.GetHV(hashId, key);
        }
    }
}
