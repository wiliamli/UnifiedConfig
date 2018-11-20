using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Paging;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 服务信息-服务
    /// </summary>
    public class ServiceService : ApplicationService, IServiceService
    {
        private IServiceRepository Repository { get; set; }
        private IPermissionRepository PermisisonRepository { get; set; }

        private ITeamRepository TeamRepository { get; set; }
        
        private IMemberRepository MemberRepository { get; set; }

        public ServiceService(IServiceRepository repository, 
            IPermissionRepository permisisonRepository,
            ITeamRepository teamRepository,
            IMemberRepository memberRepository)
        {
            this.Repository = repository;
            this.PermisisonRepository = permisisonRepository;
            this.TeamRepository = teamRepository;
            this.MemberRepository = memberRepository;
        }

        public PageResult<ServiceListDto> GetServiceDtos(ServicePageParam pageParam, string employeeId)
        {
            employeeId = employeeId.Trim();
            var query = from permission in this.PermisisonRepository.Queryable()
                        join service in this.Repository.Queryable() on permission.ServiceNumber equals service.ServiceNumber
                        join member in this.MemberRepository.Queryable() on permission.EmployeeID equals member.EmployeeID
                        where service.IsDeleted == false && permission.EmployeeID == employeeId
                        select new ServiceListDto
                        {
                            IsLeader = member.IsLeader,
                            ServiceName = service.ServiceName,
                            ServiceNumber = service.ServiceNumber,
                            ServiceSign = service.ServiceSign,
                            ModifiedTime = service.ModifiedTime
                        };
            if (!string.IsNullOrEmpty(pageParam.Key))
                query = query.Where(x => x.ServiceName.Contains(pageParam.Key));
            return query.ToPageDtos(pageParam);
        }

        public void Add()
        {
            this.Repository.Add(new Service
            {
                TeamNumber = Guid.NewGuid().ToString("N"),
                ServiceNumber = Guid.NewGuid().ToString("N"),
                ServiceName = "test",
                IsDeleted = false,
                CreatedBy = "test",
                CreatedTime = DateTime.Now,
                ModifiedBy = "test",
                ModifiedTime = DateTime.Now,
                ServiceSign = "xxxx"
            });
        }
    }
}
