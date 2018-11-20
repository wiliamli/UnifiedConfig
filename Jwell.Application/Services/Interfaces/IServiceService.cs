using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    ///    服务管理-服务-接口 
    /// </summary>
    public interface IServiceService : IApplicationService
    {
        /// <summary>
        /// 获取服务分页数据
        /// </summary>
        /// <param name="pageParam">分页参数</param>
        /// <param name="employeeId">成员工号</param>
        /// <returns></returns>
        PageResult<ServiceListDto> GetServiceDtos(ServicePageParam pageParam, string employeeId);

        void Add();
    }
}
