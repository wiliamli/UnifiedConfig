using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces.Params
{
    /// <summary>
    /// 用户分页查询参数
    /// </summary>
    public class EmployeePageParam 
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        private int pageIndex = 1;
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                if (value >= 1) pageIndex = value;
            }
        }

        public int PageSize { get; set; }
    }
}
