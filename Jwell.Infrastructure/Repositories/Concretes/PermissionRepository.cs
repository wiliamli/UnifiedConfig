using System;
using System.Collections.Generic;
using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 成员服务-仓储
    /// </summary>
    public class PermissionRepository : RepositoryBase<Permission, JwellPermissionContext, long>, IPermissionRepository
    {
        public PermissionRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        //public bool AddAndDeleteMembers(IEnumerable<ServiceMember> addEntities, IEnumerable<ServiceMember> deleteEntities)
        //{
        //    using (var dbContextTransaction = this.DbContext.Database.BeginTransaction())
        //    {
        //        if (addEntities != null)
        //            this.DbContext.Set<ServiceMember>().AddRange(addEntities);
        //        if (deleteEntities != null)
        //            this.DbContext.Set<ServiceMember>().RemoveRange(deleteEntities);

        //        try
        //        {
        //            this.DbContext.SaveChanges();
        //            dbContextTransaction.Commit();
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            dbContextTransaction.Rollback();
        //            return false;
        //        }
        //    }
        //}
    }
}
