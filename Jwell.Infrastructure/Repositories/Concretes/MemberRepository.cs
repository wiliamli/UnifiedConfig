using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;
using System.Collections.Generic;
using System.Data.Entity;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 小组成员仓储
    /// </summary>
    public class MemberRepository : RepositoryBase<Member, JwellPermissionContext, long>, IMemberRepository
    {
        public MemberRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        //public bool Delete(Member entity, IEnumerable<ServiceMember> serviceMembers)
        //{
        //    using (var dbContextTransaction = this.DbContext.Database.BeginTransaction())
        //    {
        //        this.DbContext.Entry(entity).State = EntityState.Modified;
        //        if (serviceMembers != null)
        //            this.DbContext.Set<ServiceMember>().RemoveRange(serviceMembers);
        //        try
        //        {
        //            this.DbContext.SaveChanges();
        //            dbContextTransaction.Commit();
        //            return true;
        //        }
        //        catch (System.Exception)
        //        {
        //            dbContextTransaction.Rollback();
        //            return false;
        //        }
        //    }
        //}
    }
}
