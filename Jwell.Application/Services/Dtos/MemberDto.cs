using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 小组成员DTO
    /// </summary>
    public class MemberDto
    {
        /// <summary>
        /// 成员姓名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 成员工号
        /// </summary>
        public string EmployeeID { get; set; }
        

        ///// <summary>
        ///// 是否拥有权限
        ///// </summary>
        //public bool HasAuthor { get; set; }
    }
}
