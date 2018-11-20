using System.Linq;
using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Paging;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Extensions;
using Jwell.Modules.Cache;
namespace Jwell.Application
{
    public class AdminUserService : ApplicationService, IAdminUserService
    {
        private IRepository<AdminUser,long> Repository { get; set; }
        private IHCacheClient CacheClient { get; set; }


        public AdminUserService(IRepository<AdminUser,long> repository, IHCacheClient cacheClient)
        {
            Repository = repository;
            CacheClient = cacheClient;
        }

        public int Count()
        {
            AdminUser adminUser = new AdminUser()
            {
                Account = "1234",
                Code = "12345"
            };

            AdminUser adminUser2 = adminUser.Clone<AdminUser>();
            adminUser2.Account = "adminUser2";

            System.Collections.Concurrent.ConcurrentDictionary<string, string> dic = 
                new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            System.Collections.Generic.KeyValuePair<string, string> keyValue = 
                new System.Collections.Generic.KeyValuePair<string, string>("a","b");
            
            dic.AddIfNotContains(keyValue);
            CacheClient.DB = 2;
            CacheClient.SetHT("test", dic);
            CacheClient.SetHV("test", "123", "456");
            CacheClient.SetHV("test", "789", "abc");
            bool success = CacheClient.IsExistH("test");

            success = CacheClient.IsExistHV("test","a");
            string a =CacheClient.GetHV("test","a");
            var list =CacheClient.GetHValues("test");
            success=CacheClient.RemoveHK("test","a");
            success = CacheClient.ReomveHKS("test",new string[] { "123", "789"});

            //success = CacheClient.IsExist("test");

            //success = CacheClient.RemoveCache("test");
            return 0;//Repository.Queryable().Count();
        }

        public PageResult<AdminUserDto> GetAdminUserDtos(PageParam page)
        {
           return Repository.Queryable().ToPageResult(page).ToDtos();
        }
    }
}
