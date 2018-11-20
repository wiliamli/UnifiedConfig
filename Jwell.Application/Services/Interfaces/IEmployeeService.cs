using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Interfaces.Params;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces
{
    /// <summary>
    /// 用户-服务-接口
    /// </summary>
    public interface IEmployeeService: IApplicationService
    {
        /// <summary>
        /// 获取员工分页列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PageResult<EmployeeDto> GetEmployeeByPage(EmployeePageParam param);
    }
}
