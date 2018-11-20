using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System.Collections.Generic;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 服务成员-仓储-接口
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission, long>
    {
        //bool AddAndDeleteMembers(IEnumerable<ServiceMember> addEntities, IEnumerable<ServiceMember> deleteEntities);
    }
}
