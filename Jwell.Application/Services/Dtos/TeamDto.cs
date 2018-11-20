using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 小组Dto
    /// </summary>
    public class TeamDto
    {
        ///// <summary>
        ///// 小组名称
        ///// </summary>
        //public string TeamName { get; set; }
        /// <summary>
        /// 小组标识
        /// </summary>

        public string TeamCode { get; set; }

        /// <summary>
        /// 小组Leader姓名
        /// </summary>

        public string TeamLeaderName { get; set; }

        /// <summary>
        /// 小组Leader工号
        /// </summary>

        public string TeamLeaderNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        public string CreatedTime { get; set; }

        [JsonIgnore]
        public DateTime CreateTime { get; set; }
    }

    public static class TeamDtoExt
    {
        public static TeamDto ToDto(this IQueryable<TeamDto> query)
        {
            var queryResult = query.FirstOrDefault();

            return new TeamDto
            {
                CreatedTime = queryResult?.CreateTime.ToString("yyyy-MM-dd"),
                TeamCode = queryResult?.TeamCode,
                TeamLeaderName = queryResult?.TeamLeaderName,
                TeamLeaderNumber = queryResult?.TeamLeaderNumber
            };
        }
    }
}
