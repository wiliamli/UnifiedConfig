using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 小组-仓储
    /// </summary>
    public class TeamRepository : RepositoryBase<Team, JwellPermissionContext, long>, ITeamRepository
    {
        public TeamRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        public void Add(Team entity)
        {
            this.DbContext.Teams.Add(entity);
        }
        //public bool SyncData(List<Team> teams, List<Service> services, List<Member> members, List<ServiceMember> serviceMembers)
        //{
        //    using (var dbContextTransaction = this.DbContext.Database.BeginTransaction())
        //    {
        //        if (teams != null && teams.Count > 0)
        //            this.DbContext.Set<Team>().AddRange(teams);

        //        if (services != null && services.Count > 0)
        //            this.DbContext.Set<Service>().AddRange(services);

        //        if (members != null && members.Count > 0)
        //            this.DbContext.Set<Member>().AddRange(members);

        //        if (serviceMembers != null && serviceMembers.Count > 0)
        //            this.DbContext.Set<ServiceMember>().AddRange(serviceMembers);

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

        //public bool SyncData(Service service, Member member, ServiceMember serviceMember)
        //{
        //    using (var dbContextTransaction = this.DbContext.Database.BeginTransaction())
        //    {
        //        if (service != default(Service))
        //            this.DbContext.Set<Service>().Add(service);

        //        if (member != default(Member))
        //            this.DbContext.Set<Member>().Add(member);

        //        if (serviceMember != default(ServiceMember))
        //            this.DbContext.Set<ServiceMember>().Add(serviceMember);

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
