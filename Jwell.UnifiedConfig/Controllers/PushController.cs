using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class PushController : BaseApiController
    {
        private IConfigItemService ConfigItemService { get; set; }

        public PushController(IConfigItemService configItemService)
        {
            this.ConfigItemService = configItemService;
        }

        // POST: api/Push
        /// <summary>
        /// 推送修改的配置
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public StandardJsonResult PostPushConfig([FromBody]string[] numbers)
        {
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                isSuccess = this.ConfigItemService.Push(numbers, userInfo.Name);
            });
            standardJsonResult.Success = isSuccess;
            standardJsonResult.Message = isSuccess ? "推送成功" : "推送失败";
            return standardJsonResult;
        }
    }
}
