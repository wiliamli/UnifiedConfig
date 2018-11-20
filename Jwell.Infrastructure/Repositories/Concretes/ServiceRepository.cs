using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;
using System.Transactions;
using System.Data.Entity;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 服务-仓储
    /// </summary>
    public class ServiceRepository:RepositoryBase<Service, JwellPermissionContext, long>,IServiceRepository
    {
        public ServiceRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }
    }
}
