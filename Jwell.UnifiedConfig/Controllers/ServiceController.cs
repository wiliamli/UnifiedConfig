using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
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
    public class ServiceController : BaseApiController
    {
        private IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }
        // GET: api/Service
        /// <summary>
        /// 获取服务分页
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public StandardJsonResult<PageResult<ServiceListDto>> GetServiceByPage([FromUri]ServicePageParam pageParam)
        {
            return StandardAction<PageResult<ServiceListDto>>(() =>
            {
                var userInfo = GetUserInfo();
                return this.serviceService.GetServiceDtos(pageParam, userInfo.EmployeeID);
            });
        }

        // POST: api/Service
        //public void Post([FromBody]string value)
        //{
        //    this.serviceService.Add();
        //}
    }
}
