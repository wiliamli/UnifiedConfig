using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System.Collections.Generic;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 配置项仓储接口
    /// </summary>
    public interface IConfigItemRepository : IRepository<ConfigItem, long>
    {
        bool UpdateEnties(IEnumerable<ConfigItem> entities);
    }
}
