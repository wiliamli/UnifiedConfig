using Jwell.Application.Services.Concretes;
using Jwell.Application.Services.Interfaces;
using Jwell.Modules.Cache;
using Jwell.Modules.MessageQueue.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Jwell.Framework.Modules;
using Jwell.Framework.Ioc;
using Jwell.Repository.Repositories;
using Jwell.Application.Constant;

namespace Jwell.UnifiedConfig.Web.App_Start
{
    public static class SyncData
    {

        private static IDataSyncService Service;

        static SyncData()
        {
            Service = IocContainer.Container.Resolve<IDataSyncService>();
        }

        public static void Start()
        {
            //订阅消息队列，同步数据
            RedisMQ mq = new RedisMQ();
            mq.Subscribe(ApplicationConstant.SYNCDATACHANNEL, (channel, value) =>
            {
                Service.SyncDataFromMsgQueue(value);
            });

            //同步服务管理数据
            Service.SyncDataFromCache();
        }
    }
}