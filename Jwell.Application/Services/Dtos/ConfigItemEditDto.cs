using Jwell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 配置修改Dto
    /// </summary>
    public class ConfigItemEditDto
    {
        /// <summary>
        /// 配置编号
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
    }

    public static class ConfigItemEditDtoExt
    {
        public static ConfigItemEditDto ToDto(this IQueryable<ConfigItem> query)
        {
            var queryResult = query.Select(x => new
            {
                ConfigNumber = x.ConfigNumber,
                Key = x.Key,
                Value = x.Value,
            }).FirstOrDefault();

            return new ConfigItemEditDto
            {
                ConfigNumber = queryResult?.ConfigNumber,
                Key = queryResult?.Key,
                Value = queryResult?.Value
            };
        }
    }
}
