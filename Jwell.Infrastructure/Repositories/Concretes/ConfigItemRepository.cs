using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;
using System.Collections.Generic;
using System.Data.Entity;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public class ConfigItemRepository : RepositoryBase<ConfigItem, JwellDbContext, long>, IConfigItemRepository
    {
        public ConfigItemRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        public bool UpdateEnties(IEnumerable<ConfigItem> entities)
        {
            using (var dbContextTransaction = this.DbContext.Database.BeginTransaction())
            {
                foreach (var entity in entities)
                {
                    this.DbContext.Entry(entity).State = EntityState.Modified;
                }
                try
                {
                    this.DbContext.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (System.Exception)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }
    }
}
