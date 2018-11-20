using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 小组-仓储
    /// </summary>
    public interface ITeamRepository : IRepository<Team, long>
    {
        void Add(Team entity);
        //bool SyncData(List<Team> teams, List<Service> services, List<Member> members, List<ServiceMember> serviceMembers);

        //bool SyncData(Service service, Member member, ServiceMember serviceMember);
    }
}
