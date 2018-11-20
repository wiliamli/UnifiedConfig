using Jwell.Application.Services.Dtos;
using Jwell.Framework.Application.Service;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 小组-服务-接口
    /// </summary>
    public interface ITeamService: IApplicationService
    {
        void Add(TeamDto dto);

        /// <summary>
        /// 获取一条小组信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        TeamDto GetOne(string employeeId);
    }
}
