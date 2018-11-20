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
    /// 配置项Dto
    /// </summary>
    public class ConfigItemDto
    {
        /// <summary>
        /// 配置项编号
        /// </summary>
        public string ConfigNumber { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 配置状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 配置枚举状态
        /// </summary>
        [JsonIgnore]
        public ConfigItemStatus EnumStatus { get; set; }
    }

    public static class ConfigItemDtoExt
    {
        public static PageResult<ConfigItemDto> ToPageDtos(this IQueryable<ConfigItemDto> query, PageParam pageParam)
        {
            var pageResult = new PageResult<ConfigItemDto>(query.OrderBy(pageParam.Sort, pageParam.SortDirection), pageParam.PageIndex, pageParam.PageSize);
            pageResult.Pager.ForEach(e => e.Status = e.EnumStatus.GetDescription());
            return pageResult;
        }
    }
}
