using Jwell.Application.Constant;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Interfaces;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Utilities;
using Jwell.Modules.Cache;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Concretes
{
    public class DataSyncService : ApplicationService, IDataSyncService
    {
        private ICacheClient CacheClient { get; set; }

        private ITeamRepository TeamRepository { get; set; }

        private IMemberRepository MemberRepository { get; set; }

        private IServiceRepository ServiceRepository { get; set; }

        private IPermissionRepository ServiceMemberRepository { get; set; }
        public DataSyncService(ITeamRepository teamRepository,
            IMemberRepository memberRepository,
            IServiceRepository serviceRepository, 
            IPermissionRepository serviceMemberRepository,
            ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
            this.TeamRepository = teamRepository;
            this.MemberRepository = memberRepository;
            this.ServiceRepository = serviceRepository;
            this.ServiceMemberRepository = serviceMemberRepository;
        }

        [UnitOfWork(UseTransaction = true)]
        public void SyncDataFromCache()
        {
            var syncData = this.GetDataFromCache();
            if (syncData == null || syncData.Count() == 0)
                return;

            #region 查询数据中存在的数据
            var existTeamList = this.TeamRepository.Queryable().Where(x => x.IsDeleted == false)
                    .Select(x => new TeamInfo
                    {
                        TeamCode = x.TeamCode,
                        TeamNumber = x.TeamNumber
                    }).ToList();
            var existMemberList = this.MemberRepository.Queryable().Where(x => x.IsDeleted == false)
                .Select(x => new MemberInfo
                {
                    EmployeeID = x.EmployeeID
                }).ToList();
            var existServiceList = this.ServiceRepository.Queryable().Where(x => x.IsDeleted == false)
                .Select(x => x.ServiceNumber).ToList();
            var existServiceMemberList = this.ServiceMemberRepository.Queryable()
                .Select(x => new ServiceMemberInfo
                {
                    EmployeeID = x.EmployeeID,
                    ServiceNumber = x.ServiceNumber
                }).ToList();
            #endregion

            var addTeamList = new List<Team>();
            var addServiceList = new List<Service>();
            var addMemberList = new List<Member>();
            var addServiceMemberList = new List<Permission>();

            #region 计算数据库数据与同步数据的差集，做增量处理
            foreach (var teamInfo in syncData)
            {
                var teamNumber = default(string);
                var team = CheckTeamIsExist(teamInfo.TeamCode, existTeamList, ref teamNumber);
                if (team != null && !addTeamList.Exists(e => e.TeamCode.Equals(team.TeamCode)))
                    addTeamList.Add(team);

                var leaderEmployeeID = String.Empty;
                var leader = CheckLeaderIsExist(teamInfo, existMemberList, teamNumber, ref leaderEmployeeID);
                if (leader != null && !addMemberList.Exists(e => e.EmployeeID.Equals(leader.EmployeeID)))
                    addMemberList.Add(leader);

                var leaderPermission = CheckLeaderPermissionIsExist(teamInfo.ServiceInfoes, leaderEmployeeID, existServiceMemberList);
                if (leaderPermission.Count() > 0)
                    addServiceMemberList.AddRange(leaderPermission);


                foreach (var memberServiceInfo in teamInfo.ServiceInfoes)
                {
                    var employeeID = default(string);
                    var member = CheckMemberIsExist(memberServiceInfo, existMemberList, teamNumber, ref employeeID);
                    if (member != null && !addMemberList.Exists(e => e.EmployeeID.Equals(member.EmployeeID)))
                        addMemberList.Add(member);

                    var service = CheckServiceIsExist(memberServiceInfo, existServiceList, teamNumber);
                    if (service != null && !addServiceList.Exists(e => e.ServiceNumber.Equals(service.ServiceNumber)))
                        addServiceList.Add(service);

                    var serviceMember = CheckServiceMemberIsExist(memberServiceInfo, existServiceMemberList, employeeID);
                    if (serviceMember != null && !addServiceMemberList.Exists(e => e.EmployeeID.Equals(serviceMember.EmployeeID) && e.ServiceNumber.Equals(serviceMember.ServiceNumber)))
                        addServiceMemberList.Add(serviceMember);
                }
            }
            #endregion

            addTeamList = addTeamList.Distinct().ToList();
            if (addTeamList.Count > 0)
                this.TeamRepository.AddRange(addTeamList);

            addServiceList = addServiceList.Distinct().ToList();
            if (addServiceList.Count > 0)
                this.ServiceRepository.AddRange(addServiceList);

            addMemberList = addMemberList.Distinct().ToList();
            if (addMemberList.Count > 0)
                this.MemberRepository.AddRange(addMemberList);

            addServiceMemberList = addServiceMemberList.Distinct().ToList();
            if (addServiceMemberList.Count > 0)
                this.ServiceMemberRepository.AddRange(addServiceMemberList);
        }

        [UnitOfWork(UseTransaction = true)]
        public void SyncDataFromMsgQueue(string value)
        {
            var syncData = Serializer.FromJson<TeamInfoDto>(value);
            if (syncData == null)
                return;
            #region 查询数据中存在的数据
            var existTeamList = this.TeamRepository.Queryable().Where(x => x.IsDeleted == false)
                    .Select(x => new TeamInfo
                    {
                        TeamCode = x.TeamCode,
                        TeamNumber = x.TeamNumber
                    }).ToList();
            var existMemberList = this.MemberRepository.Queryable().Where(x => x.IsDeleted == false)
                .Select(x => new MemberInfo
                {
                    EmployeeID = x.EmployeeID
                }).ToList();
            var existServiceList = this.ServiceRepository.Queryable().Where(x => x.IsDeleted == false)
                .Select(x => x.ServiceNumber).ToList();
            var existServiceMemberList = this.ServiceMemberRepository.Queryable()
                .Select(x => new ServiceMemberInfo
                {
                    EmployeeID = x.EmployeeID,
                    ServiceNumber = x.ServiceNumber
                }).ToList();
            #endregion

            var addMemberList = new List<Member>();
            var addServiceList = new List<Service>();
            var addMemberServiceList = new List<Permission>();

            var teamNumber = default(string);
            var addTeam = CheckTeamIsExist(syncData.TeamCode, existTeamList, ref teamNumber);

            var leaderEmployeeID = String.Empty;
            var leader = CheckLeaderIsExist(syncData, existMemberList, teamNumber, ref leaderEmployeeID);
            if (leader != null && !addMemberList.Exists(e => e.EmployeeID.Equals(leader.EmployeeID)))
                addMemberList.Add(leader);

            var leaderPermission = CheckLeaderPermissionIsExist(syncData.ServiceInfoes, leaderEmployeeID, existServiceMemberList);
            if (leaderPermission.Count() > 0)
                addMemberServiceList.AddRange(leaderPermission);


            foreach (var serviceInfo in syncData.ServiceInfoes)
            {
                var employeeID = default(string);
                var addMember = CheckMemberIsExist(serviceInfo, existMemberList, teamNumber, ref employeeID);
                if (addMember != null)
                    addMemberList.Add(addMember);

                var addService = CheckServiceIsExist(serviceInfo, existServiceList, teamNumber);
                if (addService != null)
                    addServiceList.Add(addService);
                var addMemberService = CheckServiceMemberIsExist(serviceInfo, existServiceMemberList, employeeID);
                if (addMemberService != null)
                    addMemberServiceList.Add(addMemberService);
            }

            if (addTeam != default(Team))
                this.TeamRepository.Add(addTeam);

            addServiceList = addServiceList.Distinct().ToList();
            if (addServiceList.Count() > 0)
                this.ServiceRepository.AddRange(addServiceList);

            addMemberList = addMemberList.Distinct().ToList();
            if (addMemberList.Count() > 0)
                this.MemberRepository.AddRange(addMemberList);

            addMemberServiceList = addMemberServiceList.Distinct().ToList();
            if (addMemberServiceList.Count() > 0)
                this.ServiceMemberRepository.AddRange(addMemberServiceList);
        }

        private List<TeamInfoDto> GetDataFromCache()
        {
            this.CacheClient.DB = ApplicationConstant.SYNCDATADB;
            return this.CacheClient.GetCache<List<TeamInfoDto>>(ApplicationConstant.SYNCDATAKEY);
        }

        private Team CheckTeamIsExist(string teamCode, List<TeamInfo> existTeamList, ref string teamNumber)
        {
            var teamExist = false;
            foreach (var existTeam in existTeamList)
            {
                //小组的标识相同，认为已存在相同小组
                if (teamCode.Equals(existTeam.TeamCode))
                {
                    teamExist = true;
                    teamNumber = existTeam.TeamNumber;
                    break;
                }
            }
            var team = default(Team);
            if (!teamExist)//team不存在，加入team同步数组
            {
                teamNumber = Guid.NewGuid().ToString("N");
                team = new Team
                {
                    TeamCode = teamCode,
                    IsDeleted = false,
                    TeamNumber = teamNumber,
                    CreatedBy = "sync",
                    CreatedTime = DateTime.Now,
                    ModifiedBy = "sync",
                    ModifiedTime = DateTime.Now
                };
            }
            return team;
        }

        private Member CheckLeaderIsExist(TeamInfoDto dto, List<MemberInfo> existMemberList, string teamNumber, ref string employeeID)
        {
            var memberExist = false;
            foreach (var existMember in existMemberList)
            {
                if (existMember.EmployeeID.Equals(dto.LeaderId))
                {
                    memberExist = true;
                    employeeID = existMember.EmployeeID;
                    break;
                }
            }
            var member = default(Member);
            if (!memberExist)
            {
                employeeID = dto.LeaderId;
                member = new Member
                {
                    IsDeleted = false,
                    CreatedBy = "sync",
                    CreatedTime = DateTime.Now,
                    ModifiedBy = "sync",
                    ModifiedTime = DateTime.Now,
                    EmployeeID = dto.LeaderId,
                    IsLeader = true,
                    MemberName = dto.LeaderName,
                    MemberNumber = Guid.NewGuid().ToString("N"),
                    TeamNumber = teamNumber
                };
            }
            return member;
        }
 
        private Member CheckMemberIsExist(ServiceInfoDto dto, List<MemberInfo> existMemberList,string teamNumber,ref string employeeID)
        {
            var memberExist = false;
            foreach (var existMember in existMemberList)
            {
                //工号相等，表示已经存在相同组员
                if (existMember.EmployeeID.Equals(dto.OwnerId))
                {
                    memberExist = true;
                    employeeID = existMember.EmployeeID;
                    break;
                }
            }
            var member = default(Member);
            if (!memberExist)//成员不存在,加入member同步数组
            {
                employeeID = dto.OwnerId;
                member = new Member
                {
                    IsDeleted = false,
                    MemberNumber = Guid.NewGuid().ToString("N"),
                    MemberName = dto.OwnerName,
                    EmployeeID = dto.OwnerId,
                    TeamNumber = teamNumber,
                    CreatedBy = dto.CreatedBy,
                    CreatedTime = dto.CreatedTime,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedTime = dto.ModifiedTime,
                    IsLeader = false
                };
            }
            return member;
        }

        private Service CheckServiceIsExist(ServiceInfoDto dto, List<string> existServiceList, string teamNumber)
        {
            var service = default(Service);
            //服务编号相同，则认为存在相同服务
            if (!existServiceList.Contains(dto.ServiceNumber))//服务不存在，加入服务同步数组
            {
                service = new Service
                {
                    IsDeleted = false,
                    ServiceNumber = dto.ServiceNumber,
                    ServiceName = dto.ServiceName,
                    ServiceSign = dto.ServiceSign,
                    TeamNumber = teamNumber,
                    CreatedBy = dto.CreatedBy,
                    CreatedTime = dto.CreatedTime,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedTime = dto.ModifiedTime
                };
            }
            return service;
        }

        private List<Permission> CheckLeaderPermissionIsExist(List<ServiceInfoDto> serviceInfoDtos, string leaderEmployeeID, List<ServiceMemberInfo> existServiceMemberList)
        {
            var list = new List<Permission>();
            foreach (var dto in serviceInfoDtos)
            {
                //成员工号和服务编号相同，则认为已存在相同的成员服务信息
                var isExist = existServiceMemberList.Any(x => x.EmployeeID.Equals(leaderEmployeeID) && x.ServiceNumber.Equals(dto.ServiceNumber));
                if (!isExist)
                {
                    list.Add(new Permission
                    {
                        EmployeeID = leaderEmployeeID,
                        ServiceNumber = dto.ServiceNumber
                    });
                }
            }
            return list;
        }

        private Permission CheckServiceMemberIsExist(ServiceInfoDto dto, List<ServiceMemberInfo> existServiceMemberList, string employeeID)
        {
            bool serviceMemberExist = false;
            var serviceMember = default(Permission);
            foreach (var existServiceMember in existServiceMemberList)
            {
                //成员工号和服务编号相同，则认为已存在相同的成员服务信息
                if (employeeID.Equals(existServiceMember.EmployeeID) && dto.ServiceNumber.Equals(existServiceMember.ServiceNumber))
                {
                    serviceMemberExist = true;
                    break;
                }
            }
            if (!serviceMemberExist)//如果成员服务不存在，加入成员服务同步数组
            {
                serviceMember = new Permission
                {
                    EmployeeID = employeeID,
                    ServiceNumber = dto.ServiceNumber
                };
            }
            return serviceMember;
        }

        private class TeamInfo
        {
            public string TeamCode { get; set; }
            public string TeamNumber { get; set; }
        }

        private class MemberInfo
        {
            public string EmployeeID { get; set; }
        }

        private class ServiceMemberInfo
        {
            public string EmployeeID { get; set; }

            public string ServiceNumber { get; set; }
        }
    }
}
