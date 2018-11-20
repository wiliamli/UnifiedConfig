using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 成员-服务-接口
    /// </summary>
    public interface IMemberService : IApplicationService
    {
        /// <summary>
        /// 获取小组成员分页列表
        /// </summary>
        /// <param name="pageParam">分页参数</param>
        /// <param name="employeeID">组长编号</param>
        /// <returns></returns>
        PageResult<MemberDto> GetDtos(PageParam pageParam, string employeeID);

        /// <summary>
        /// 添加小组成员
        /// </summary>
        /// <param name="members">小组成员添加集合</param>
        /// <param name="employeeId">组长编号</param>
        /// <param name="createdBy">添加人</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        bool Add(IEnumerable<MemberAddParam> members, string employeeId, string createdBy, ref string errorMsg);

        /// <summary>
        /// 删除小组成员
        /// </summary>
        /// <param name="employeeID">员工工号</param>
        /// <returns></returns>
        bool Delete(string employeeID, string modified);
    }
}
