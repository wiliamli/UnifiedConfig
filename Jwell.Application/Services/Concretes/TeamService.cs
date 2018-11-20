using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Uow;
using Jwell.Repository.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 小组-服务
    /// </summary>
    public class TeamService: ApplicationService, ITeamService
    {
        private ITeamRepository Repository { get; set; }

        private IMemberRepository MemberRepository { get; set; }

        public TeamService(ITeamRepository repository,IMemberRepository memberRepository)
        {
            this.Repository = repository;
            this.MemberRepository = memberRepository;
        }
       
        public void Add(TeamDto dto)
        {
            var team = new Team
            {
                TeamNumber = Guid.NewGuid().ToString("N"),
                TeamCode = dto.TeamCode,
                IsDeleted = false,
                CreatedBy = "test10",
                CreatedTime = DateTime.Now,
                ModifiedBy = "tes10",
                ModifiedTime = DateTime.Now,
            };
            
            var member = this.MemberRepository.Queryable().Where(e => e.MemberNumber == "78e1c68b109b4e948bb7915dda056f0c").FirstOrDefault();
            member.MemberName = "red修改";

           this.Repository.Add(team);
        }
        
        public TeamDto GetOne(string employeeId)
        {
            employeeId = employeeId.Trim();
            var query = from member in this.MemberRepository.Queryable()
                        join team in this.Repository.Queryable() on member.TeamNumber equals team.TeamNumber
                        join teamMember in this.MemberRepository.Queryable() on team.TeamNumber equals teamMember.TeamNumber into members
                        where member.IsDeleted == false && team.IsDeleted == false && member.EmployeeID == employeeId
                        select new
                        {
                            TeamCode = team.TeamCode,
                            CreateTime = team.CreatedTime,
                            Members = from m in members 
                                      select new {
                                          EmployeeID = m.EmployeeID,
                                          Name = m.MemberName,
                                          IsLeader = m.IsLeader
                                      }
                        };

            var queryResult = query.FirstOrDefault();

            return new TeamDto
            {
                CreatedTime = queryResult?.CreateTime.ToString("yyyy-MM-dd"),
                TeamCode = queryResult?.TeamCode,
                TeamLeaderName = queryResult?.Members.Where(x => x.IsLeader == true).FirstOrDefault()?.Name,
                TeamLeaderNumber = queryResult?.Members.Where(x => x.IsLeader == true).FirstOrDefault()?.EmployeeID
            };
        }
    }
}
