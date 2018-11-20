using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Interfaces.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.UnifiedConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class ConfigController : BaseApiController
    {
        private IConfigService Service { get; set; }

        public ConfigController(IConfigService service)
        {
            this.Service = service;
        }

        // GET: api/Config
        /// <summary>
        /// 获取配置项分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public StandardJsonResult<PageResult<ConfigItemDto>> GetConfigItemByPage([FromUri]ConfigItemPageParam pageParam)
        {
            return StandardAction(() => Service.GetDtos(pageParam, pageParam.ServiceNumber, pageParam.Env));
        }

        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public StandardJsonResult PostSyncConfig([FromBody] ConfigSyncParam param)
        {
            bool isSuccess = false;
            string errorMsg = string.Empty;
            var jsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                //var userInfo = new UserInfo() { Name = "test", EmployeeID = "1231" };
                isSuccess = Service.SyncConfig(param.ServiceNumber, param.FromEnv, param.ToEnv, userInfo.EmployeeID, ref errorMsg);
            });
            jsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(jsonResult.Message))
                jsonResult.Message = isSuccess ? "同步成功" : string.IsNullOrEmpty(errorMsg) ? "同步失败" : errorMsg;

            return jsonResult;
        }
    }
}
