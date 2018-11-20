using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 服务分页查询参数
    /// </summary>
    public class ServicePageParam : PageParam
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Key { get; set; }
    }
}
