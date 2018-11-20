using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Interfaces;
using Jwell.Application.Services.Interfaces.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private IEmployeeService EmployeeService { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            this.EmployeeService = employeeService;
        }
        // GET: api/Employee
        public StandardJsonResult<PageResult<EmployeeDto>> GetEmployeeByPage([FromUri]EmployeePageParam param)
        {
            return StandardAction(() => EmployeeService.GetEmployeeByPage(param));
        }
    }
}
