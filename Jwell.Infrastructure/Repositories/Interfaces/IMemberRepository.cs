using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System.Collections;
using System.Collections.Generic;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 小组成员仓储接口
    /// </summary>
    public interface IMemberRepository : IRepository<Member, long>
    {
        //bool Delete(Member entity, IEnumerable<ServiceMember> serviceMembers);
    }
}
