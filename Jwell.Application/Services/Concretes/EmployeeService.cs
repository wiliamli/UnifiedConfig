using Jwell.Application.Constant;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Interfaces;
using Jwell.Application.Services.Interfaces.Params;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using Jwell.Modules.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Concretes
{
    /// <summary>
    /// 员工-服务
    /// </summary>
    public class EmployeeService : ApplicationService, IEmployeeService
    {
        private ICacheClient CacheClient { get; set; }

        public EmployeeService(ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
        }

        public PageResult<EmployeeDto> GetEmployeeByPage(EmployeePageParam param)
        {
            this.CacheClient.DB = ApplicationConstant.EMPLOYEECACHEDB;
            var users = this.CacheClient.GetCache<List<EmployeeInfoDto>>(ApplicationConstant.EMPLOYEEKEY) as IEnumerable<EmployeeInfoDto>;
            if (!string.IsNullOrEmpty(param.Name))
                users = users.Where(x => x.UserName.Contains(param.Name));
            if (!string.IsNullOrEmpty(param.Department))
                users = users.Where(x => x.Department.Contains(param.Department));
            if (!string.IsNullOrEmpty(param.EmployeeId))
                users = users.Where(x => x.EmployeeID.Contains(param.EmployeeId));
            return users.ToPageDto(param.PageIndex, param.PageSize);
        }
    }
}
