using Jwell.Application.Services.Params;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Domain.Uow;
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
    /// 服务成员-服务
    /// </summary>
    public class PermissionService : ApplicationService, IPermissionService
    {
        private IPermissionRepository Repository { get; set; }

        private MemberRepository MemberRepository { get; set; }
        public PermissionService(IPermissionRepository repository,MemberRepository memberRepository)
        {
            this.Repository = repository;
            this.MemberRepository = memberRepository;
        }

        [UnitOfWork(UseTransaction =true)]
        public bool UpdateServicePermission(ServiceMemberAddParam param)
        {
            var serviceNumber = param.ServiceNumber.Trim();
            var existPermission = (from permission in this.Repository.Queryable()
                               join member in this.MemberRepository.Queryable() on permission.EmployeeID equals member.EmployeeID
                               where member.IsDeleted == false && member.IsLeader == false && permission.ServiceNumber == serviceNumber
                               select permission).ToList();

            //计算删除的Permission
            var deleteMember = new List<Permission>();
            foreach (var member in existPermission)
            {
                if (!param.EmployeeIDs.Contains(member.EmployeeID))
                    deleteMember.Add(member);
            }

            //计算新增的Permission
            var addMember = new List<Permission>();
            var existMemberId = existPermission.Select(x => x.EmployeeID).ToList();
            foreach (var employeeID in param.EmployeeIDs)
            {
                if (!existMemberId.Contains(employeeID))
                    addMember.Add(new Permission
                    {
                        ServiceNumber = param.ServiceNumber,
                        EmployeeID = employeeID
                    });
            }
           

            foreach (var deleteEnity in deleteMember)
            {
                this.Repository.Delete(deleteEnity);
            }

            foreach (var addEntity in addMember)
            {
                this.Repository.Add(addEntity);
            }
            return true;
        }

        public List<string> GetMemberHasAuthor(string serviceNumber)
        {
            serviceNumber = serviceNumber.Trim();
            return (from permission in this.Repository.Queryable()
                    join member in this.MemberRepository.Queryable() on permission.EmployeeID equals member.EmployeeID
                    where member.IsDeleted == false && member.IsLeader == false && permission.ServiceNumber == serviceNumber
                    select permission.EmployeeID).ToList();
        }
    }
}
