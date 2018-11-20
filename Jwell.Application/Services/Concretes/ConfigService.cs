using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Paging;
using Jwell.Modules.Cache;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 配置管理-服务
    /// </summary>
    public class ConfigService : ApplicationService, IConfigService
    {
        private IConfigItemRepository ConfigItemRepository { get; set; }
        public ConfigService( IConfigItemRepository configItemRepository)
        {
            this.ConfigItemRepository = configItemRepository;
        }
        
        public PageResult<ConfigItemDto> GetDtos(ConfigItemPageParam pageParam, string serviceNumber, ConfigEnvironment env)
        {
            serviceNumber = serviceNumber.Trim();
            var query = from config in this.ConfigItemRepository.Queryable()
                        where config.IsDeleted == false && config.ServiceNumber == serviceNumber && config.ConfigEnvironment == env
                        select new ConfigItemDto
                        {
                            ConfigNumber = config.ConfigNumber,
                            Key = config.Key,
                            Value = config.Value,
                            EnumStatus = config.Status
                        };
            //关键字过滤
            if (!string.IsNullOrEmpty(pageParam.Key))
                query = query.Where(x => x.Key.Contains(pageParam.Key));

            return query.ToPageDtos(pageParam);
        }

        [UnitOfWork(UseTransaction = true)]
        public bool SyncConfig(string serviceNumber, ConfigEnvironment fromEnv, ConfigEnvironment toEnv, string employId, ref string errorMsg)
        {
            var fromEnvQuery = from config in this.ConfigItemRepository.Queryable()
                               where config.IsDeleted == false && config.ServiceNumber == serviceNumber && config.ConfigEnvironment == fromEnv
                               select new
                               {
                                   Key = config.Key,
                                   Value = config.Value
                               };

            var toEnvQuery  = from config in this.ConfigItemRepository.Queryable()
                              where config.IsDeleted == false && config.ServiceNumber == serviceNumber && config.ConfigEnvironment == toEnv
                              select new
                              {
                                  Key = config.Key,
                                  Value = config.Value
                              };
            var fromEnvItems = fromEnvQuery.ToList();
            var toEnvItems = toEnvQuery.ToList();
            var addItems = new List<Item>();
            //做增量添加
            foreach (var item in fromEnvItems)
            {
                if (!toEnvItems.Exists(e => e.Key == item.Key))
                    addItems.Add(new Item { Key = item.Key, Value = item.Value });
            }

            var addConfigItem = addItems.Select(x => new ConfigItem
            {
                CreatedTime = DateTime.Now,
                Status = ConfigItemStatus.Add,
                ConfigNumber = Guid.NewGuid().ToString("N"),
                CreatedBy = employId,
                IsDeleted = false,
                Key = x.Key,
                Value = x.Value,
                ModifiedBy = employId,
                ModifiedTime = DateTime.Now,
                ConfigEnvironment = toEnv,
                ServiceNumber = serviceNumber,
            }).ToList();

            if (addConfigItem.Count == 0)
                return false;
            return this.ConfigItemRepository.AddRange(addConfigItem) > 0;
        }

        private class Item
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
