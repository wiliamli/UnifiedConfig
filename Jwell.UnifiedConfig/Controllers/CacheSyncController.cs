using Jwell.Application.Services.Interfaces;
using Jwell.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class CacheSyncController : BaseApiController
    {
        private ICacheSyncService CacheSyncService { get; set; }

        public CacheSyncController(ICacheSyncService cacheSyncService)
        {
            this.CacheSyncService = cacheSyncService;
        }
        // GET: api/CacheSync
        [AllowAnonymous]
        public StandardJsonResult GetSyncCache()
        {
            bool isSuccess = false;
            var jsonResult = StandardAction(() =>
            {
                isSuccess = this.CacheSyncService.SyncConfigData();
            });
            jsonResult.Success = isSuccess;
            jsonResult.Message = isSuccess ? "同步成功" : "同步失败";
            return jsonResult;
        }
    }
}
