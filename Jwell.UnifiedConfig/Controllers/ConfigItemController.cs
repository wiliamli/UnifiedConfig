using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.UnifiedConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class ConfigItemController : BaseApiController
    {
        private IConfigItemService ConfigItemService { get; set; }

        public ConfigItemController(IConfigItemService configItemService)
        {
            this.ConfigItemService = configItemService;
        }

        // GET: api/ConfigItem
        /// <summary>
        /// 获取一条配置
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public StandardJsonResult<ConfigItemEditDto> GetConfigItem(string number)
        {
            return StandardAction<ConfigItemEditDto>(() => this.ConfigItemService.Get(number));
        }

        // POST: api/ConfigItem
        /// <summary>
        /// 添加一条配置
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public StandardJsonResult PostConfigItem([FromBody]ConfigItemAddParam param)
        {
            string errorMsg = string.Empty;
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
             {
                 var userInfo = GetUserInfo();
                 //var userInfo = new UserInfo() { Name = "test" };
                 isSuccess = this.ConfigItemService.Add(param, userInfo.Name, ref errorMsg);
             });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "添加成功" : (string.IsNullOrEmpty(errorMsg) ? "添加失败" : errorMsg);
            return standardJsonResult;
        }

        // PUT: api/ConfigItem/5
        /// <summary>
        /// 修改一条配置
        /// </summary>
        /// <param name="number"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public StandardJsonResult PutConfigItem(string number, [FromBody]ConfigItemEditParam param)
        {
            bool isSuccess = false;
            string errorMsg = string.Empty;
            var standardJsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                //var userInfo = new UserInfo() { Name = "test" };
                isSuccess = this.ConfigItemService.Edit(number, param, userInfo.Name, ref errorMsg);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "修改成功" : string.IsNullOrEmpty(errorMsg) ? "修改失败" : errorMsg;
            return standardJsonResult;
        }

        // DELETE: api/ConfigItem/5
        /// <summary>
        /// 删除一条配置
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public StandardJsonResult DeleteConfigItem(string number)
        {
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                //var userInfo = new UserInfo() { Name = "test" };
                isSuccess = this.ConfigItemService.Delete(number, userInfo.Name);
            });
            standardJsonResult.Success = isSuccess;
            standardJsonResult.Message = isSuccess ? "删除成功" : "删除失败";
            return standardJsonResult;
        }
    }
}
