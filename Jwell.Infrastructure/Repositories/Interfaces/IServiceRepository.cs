using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 服务-仓储-接口
    /// </summary>
    public interface IServiceRepository : IRepository<Service, long>
    {
    }
}
