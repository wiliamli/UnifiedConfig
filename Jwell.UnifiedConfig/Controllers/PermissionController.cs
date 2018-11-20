using Jwell.Application.Services;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class PermissionController : BaseApiController
    {
        private IPermissionService MemberSerice { get; set; }

        public PermissionController(IPermissionService memberService)
        {
            this.MemberSerice = memberService;
        }

        public StandardJsonResult<List<string>> Get(string serviceNumber)
        {
            return StandardAction(() => this.MemberSerice.GetMemberHasAuthor(serviceNumber));
        }

        // POST: api/Permission
        /// <summary>
        /// 配置管理权限授权
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public StandardJsonResult Post([FromBody]ServiceMemberAddParam param)
        {
            bool isSuccess = false;
            var jsonResult = StandardAction(() =>
            {
                isSuccess = this.MemberSerice.UpdateServicePermission(param);
            });

            jsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(jsonResult.Message))
                jsonResult.Message = isSuccess ? "成功" : "失败";
            return jsonResult;
        }
    }
}
