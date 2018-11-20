using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
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
    /// 成员-服务
    /// </summary>
    public class MemberService : ApplicationService, IMemberService
    {
        private IMemberRepository Repository { get; set; }

        private IPermissionRepository ServiceMemberRepository { get; set; }

        private ITeamRepository TeamRepository { get; set; }

        private IServiceRepository ServiceRepository { get; set; }

        public MemberService(IMemberRepository repository,
            IPermissionRepository serviceMemberRepository,
            ITeamRepository teamRepository,
            IServiceRepository serviceRepository)
        {
            this.Repository = repository;
            this.ServiceMemberRepository = serviceMemberRepository;
            this.TeamRepository = teamRepository;
            this.ServiceRepository = serviceRepository;
        }

        [UnitOfWork(UseTransaction = true)]
        public bool Add(IEnumerable<MemberAddParam> members, string employeeId, string createdBy, ref string errorMsg)
        {
            //查询所有已添加成员的工号集合
            var query = from member in this.Repository.Queryable()
                        where member.IsDeleted == false
                        select member.EmployeeID;

            var existMemberNumberList = query.ToList();

            //计算重复添加的成员
            var existMemberList = members.Where(m => existMemberNumberList.Contains(m.EmployeeID)).ToList();

            if (existMemberList.Count > 0)//存在重复添加的成员
            {
                var resultMessage = new StringBuilder();
                resultMessage.Append("成员");
                foreach (var member in existMemberList)
                {
                    resultMessage.Append($"{member.Name}({member.EmployeeID}),");
                }
                resultMessage.Remove(resultMessage.Length - 1, 1);
                resultMessage.Append("已被添加到项目组，不能重复添加");
                errorMsg = resultMessage.ToString();
                return false;
            }
            else//不存在重复添加的成员
            {
                employeeId = employeeId.Trim();
                var teamNumber = this.Repository.Queryable().Where(x => x.IsDeleted == false && x.EmployeeID == employeeId && x.IsLeader == true)
                    .Select(x => x.TeamNumber).FirstOrDefault();
                if (string.IsNullOrEmpty(teamNumber))
                {
                    errorMsg = "您无权添加小组成员";
                    return false;
                }
                var addEntities = members.Select(m => new Member
                {
                    CreatedBy = createdBy,
                    CreatedTime = DateTime.Now,
                    IsDeleted = false,
                    MemberNumber = Guid.NewGuid().ToString("N"),
                    MemberName = m.Name,
                    EmployeeID = m.EmployeeID,
                    ModifiedBy = createdBy,
                    ModifiedTime = DateTime.Now,
                    TeamNumber = teamNumber,
                    IsLeader=false
                });
                return this.Repository.AddRange(addEntities) > 0;
            }
        }

        [UnitOfWork(UseTransaction = true)]
        public bool Delete(string employeeID, string modified)
        {
            employeeID = employeeID.Trim();
            var member = this.Repository.Queryable().Where(m => m.EmployeeID == employeeID && m.IsDeleted == false).FirstOrDefault();
            if (member == null)
                return false;
            member.IsDeleted = true;
            member.ModifiedBy = modified;
            member.ModifiedTime = DateTime.Now;
            //删除成员的服务权限信息
            var serviceMembers = this.ServiceMemberRepository.Queryable().Where(m => m.EmployeeID == employeeID).ToList();

            this.Repository.Update(member);
            foreach (var servicemember in serviceMembers)
            {
                this.ServiceMemberRepository.Delete(servicemember);
            }
            return true;
        }

        public PageResult<MemberDto> GetDtos(PageParam pageParam, string employeeID)
        {
            employeeID = employeeID.Trim();
            var teamNumber = this.Repository.Queryable().Where(x => x.IsDeleted == false && x.EmployeeID == employeeID && x.IsLeader == true)
                .Select(x => x.TeamNumber).FirstOrDefault();

            var query = from member in this.Repository.Queryable()
                        where member.IsDeleted == false && member.TeamNumber == teamNumber && member.IsLeader == false
                        select new MemberDto
                        {
                            MemberName = member.MemberName,
                            EmployeeID = member.EmployeeID
                        };

            return query.ToPageResult(pageParam);
        }
    }
}
