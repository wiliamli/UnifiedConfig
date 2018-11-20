using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Domain.Uow;
using Jwell.Modules.Cache;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 配置项-服务
    /// </summary>
    public class ConfigItemService : ApplicationService, IConfigItemService
    {
        private IConfigItemRepository Repository { get; set; }

        private IHCacheClient CacheClient { get; set; }

        public ConfigItemService(IConfigItemRepository repository,IHCacheClient cacheClient)
        {
            this.Repository = repository;
            this.CacheClient = cacheClient;
        }

        public bool Add(ConfigItemAddParam param, string createdBy, ref string errorMsg)
        {
            var entity = this.Repository.Queryable().Where(e => e.Key == param.Key.Trim() && e.ServiceNumber == param.ServiceNumber &&
            e.ConfigEnvironment == param.Env && e.IsDeleted == false).FirstOrDefault();
            if (entity != null && entity.Status != ConfigItemStatus.Delete)//判断配置项是否已存在
            {
                errorMsg = $"配置项{param.Key}已存在";
                return false;
            }
            if (entity != null && entity.Status == ConfigItemStatus.Delete)//判断配置项是否已删除，但未推送
            {
                errorMsg = $"配置项{param.Key}已删除，但未推送，请推送后再添加";
                return false;
            }

            entity = new ConfigItem
            {
                ConfigEnvironment = param.Env,
                ServiceNumber=param.ServiceNumber,
                ConfigNumber = Guid.NewGuid().ToString("N"),
                Key = param.Key,
                Value = param.Value,
                Status = ConfigItemStatus.Add,
                IsDeleted = false,
                CreatedBy = createdBy,
                CreatedTime = DateTime.Now,
                ModifiedBy = createdBy,
                ModifiedTime = DateTime.Now
            };
            return this.Repository.Add(entity) > 0;
        }

        public bool Edit(string number, ConfigItemEditParam param, string modifiedBy,ref string errorMsg)
        {
            number = number.Trim();
            param.Key = param.Key.Trim();
            var entity = this.Repository.Queryable()
               .Where(x => x.ConfigNumber == number && x.IsDeleted == false).FirstOrDefault();
            if (entity == null)
                return false;

            var existEntity = this.Repository.Queryable().Where(x => x.IsDeleted == false &&
            x.ServiceNumber == entity.ServiceNumber && x.ConfigEnvironment == entity.ConfigEnvironment && x.Key == param.Key).FirstOrDefault();

            if (existEntity != null && existEntity.Status != ConfigItemStatus.Delete)//判断配置项是否已存在
            {
                errorMsg = $"配置项{param.Key}已存在";
                return false;
            }
            if (existEntity != null && existEntity.Status == ConfigItemStatus.Delete)//判断配置项是否已删除，但未推送
            {
                errorMsg = $"配置项{param.Key}已删除，但未推送，请推送后再修改";
                return false;
            }
            
            entity.Key = param.Key;
            entity.Value = param.Value;
            entity.Status = ConfigItemStatus.Edit;
            entity.ModifiedBy = modifiedBy;
            entity.ModifiedTime = DateTime.Now;
            return this.Repository.Update(entity) > 0;
        }

        public bool Delete(string number, string modifiedBy)
        {
            number = number.Trim();
            var entity = this.Repository.Queryable()
                .Where(x => x.ConfigNumber == number && x.IsDeleted == false && x.Status != ConfigItemStatus.Delete).FirstOrDefault();
            if (entity == null)
                return false;

            entity.Status = ConfigItemStatus.Delete;
            entity.ModifiedBy = modifiedBy;
            entity.ModifiedTime = DateTime.Now;
            return this.Repository.Update(entity) > 0;
        }
        
        public ConfigItemEditDto Get(string number)
        {
            number = number.Trim();
            return this.Repository.Queryable()
                .Where(x => x.ConfigNumber == number && x.IsDeleted == false)
                .ToDto();
        }
        
        public bool Push(string[] numbers, string modifiedBy)
        {
            var query = from item in this.Repository.Queryable()
                        where item.IsDeleted == false && item.Status != ConfigItemStatus.Pushed && numbers.Contains(item.ConfigNumber)
                        select item;

            var queryResult = query.ToList();
            if (queryResult == null || query.Count() == 0)
                return false;

            //检验查询结果中所有的serviceNumber和Env是否一致
            var firstQueryResult = queryResult.First();
            bool isAccordance = true;
            foreach (var item in queryResult)
            {
                if (item.ConfigEnvironment != firstQueryResult.ConfigEnvironment || !item.ServiceNumber.Equals(firstQueryResult.ServiceNumber))
                {
                    isAccordance = false;
                    break;
                }
            }
            //不一致，则推送失败
            if (!isAccordance)
                return false;

            //更新所有配置项的状态
            queryResult.ForEach(e =>
            {
                if (e.Status == ConfigItemStatus.Delete)
                    e.IsDeleted = true;
                e.Status = ConfigItemStatus.Pushed;
                e.ModifiedBy = modifiedBy;
                e.ModifiedTime = DateTime.Now;
            });
            bool isSuccess = this.Repository.UpdateEnties(queryResult);

            //数据库更新成功，则同步缓存
            if (isSuccess)
            {
                new Task(() =>
                {
                    var hashId = firstQueryResult.ServiceNumber.ToLower() + firstQueryResult.ConfigEnvironment.ToString().ToLower();
                    ConcurrentDictionary<string, string> conDictionary = new ConcurrentDictionary<string, string>();

                    //移除缓存中删除的配置项
                    var removeKeys = queryResult.Where(e => e.IsDeleted == true).Select(e => e.Key).ToList();
                    if (removeKeys != null && removeKeys.Count() > 0)
                        CacheClient.ReomveHKS(hashId, removeKeys);

                    //跟新缓存中添加和修改的配置项
                    foreach (var configItem in queryResult.Where(e => e.IsDeleted == false))
                    {
                        conDictionary.TryAdd(configItem.Key, configItem.Value);
                    }
                    if (conDictionary.Count() > 0)
                        CacheClient.SetHT(hashId, conDictionary);
                }).Start();
            }
            return isSuccess;
        }
    }
}
