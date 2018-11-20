using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 员工Dto
    /// </summary>
    public class EmployeeInfoDto
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }

    public static class EmployeeInfoDtoExt
    {
        public static PageResult<EmployeeDto> ToPageDto(this IEnumerable<EmployeeInfoDto> source, int pageIndex, int pageSize)
        {
            var usersPage = source.Select(x => new EmployeeDto
            {
                Department = x.Department,
                EmployeeID = x.EmployeeID,
                UserName = x.UserName
            }).OrderBy(x => x.EmployeeID);
            return new PageResult<EmployeeDto>(usersPage, pageIndex, pageSize, source.Count());
        }
    }
}
