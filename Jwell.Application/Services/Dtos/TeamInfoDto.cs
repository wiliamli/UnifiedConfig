using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 小组信息Dto
    /// </summary>
    public class TeamInfoDto
    {
        public TeamInfoDto()
        {
            ServiceInfoes = new List<ServiceInfoDto>();
        }

        /// <summary>
        /// 小组leader工号
        /// </summary>
        public string LeaderId { get; set; }

        /// <summary>
        /// 小组leader姓名
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 小组标识
        /// </summary>
        public string TeamCode { get; set; }

        /// <summary>
        /// 服务信息列表
        /// </summary>
        public List<ServiceInfoDto> ServiceInfoes { get; set; }
    }

    /// <summary>
    /// 服务信息Dto
    /// </summary>
    public class ServiceInfoDto
    {
        /// <summary>
        /// 服务number
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务标识
        /// </summary>
        public string ServiceSign { get; set; }

        /// <summary>
        /// 服务拥有者工号
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// 服务拥有者姓名
        /// </summary>
        public string OwnerName { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifiedBy { get; set; }

    }

}
