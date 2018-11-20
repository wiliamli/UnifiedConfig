using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 服务列表Dto
    /// </summary>
    public class ServiceListDto
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        public String ServiceNumber { get; set; }

        /// <summary>
        /// 服务标识
        /// </summary>

        public String ServiceName { get; set; }

        ///// <summary>
        ///// 备注
        ///// </summary>

        //public String Remark { get; set; }

            /// <summary>
            ///服务标识
            /// </summary>
        public string ServiceSign { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>

        public string LastEditTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonIgnore]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 当前用户为Leader
        /// </summary>
        public bool IsLeader { get; set; }
    }
    public static class ServiceListDtoExt
    {
        public static PageResult<ServiceListDto> ToPageDtos(this IQueryable<ServiceListDto> query, PageParam pageParam)
        {
            var pageResult = new PageResult<ServiceListDto>(query.OrderBy(pageParam.Sort, pageParam.SortDirection), pageParam.PageIndex, pageParam.PageSize);
            pageResult.Pager.ForEach(e => e.LastEditTime = e.ModifiedTime.ToString("yyyy-MM-dd"));
            return pageResult;
        }
    }

}
