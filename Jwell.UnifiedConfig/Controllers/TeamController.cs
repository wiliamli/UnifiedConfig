using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Mvc;
using Jwell.Modules.Logger;
using Jwell.UnifiedConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class TeamController : BaseApiController
    {

        private ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        // GET: api/Team
        /// <summary>
        /// 获取小组信息
        /// </summary>
        /// <returns></returns>
        public StandardJsonResult<TeamDto> GetTeam()
        {
            return StandardAction<TeamDto>(() =>
            {
                var userInfo = GetUserInfo();
                return this.teamService.GetOne(userInfo.EmployeeID);
            });
        }

        // POST: api/Team
        //public void Post([FromBody]string value)
        //{
        //    var dto = new TeamDto
        //    {
        //        TeamCode = "xxxxx",
        //        TeamLeaderName = "admin",
        //        TeamLeaderNumber = "170613"
        //    };
        //    this.teamService.Add(dto);
        //}
    }
}
