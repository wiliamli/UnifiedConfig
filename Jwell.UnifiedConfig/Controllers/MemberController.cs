using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.UnifiedConfig.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    public class MemberController : BaseApiController
    {
        private IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }
        // GET: api/TeamMember
        /// <summary>
        /// 获取成员分页数据
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public StandardJsonResult<PageResult<MemberDto>> GetMemberByPage([FromUri]PageParam pageParam)
        {
            return StandardAction<PageResult<MemberDto>>(() =>
            {
                var userInfo = GetUserInfo();
                return this.memberService.GetDtos(pageParam, userInfo.EmployeeID);
            });
        }

        //POST:api/TeamMember
        /// <summary>
        /// 添加小组成员
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        public StandardJsonResult Post([FromBody]List<MemberAddParam> members)
        {
            string errorMsg = string.Empty;
            bool isSuccess = false;
            var jsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                isSuccess = this.memberService.Add(members, userInfo.EmployeeID, userInfo.Name, ref errorMsg);
            });

            jsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(jsonResult.Message))
                jsonResult.Message = isSuccess ? "添加成功" : string.IsNullOrEmpty(errorMsg) ? "添加失败" : errorMsg;
            return jsonResult;
        }

        // DELETE: api/TeamMember/5
        /// <summary>
        /// 删除小组成员
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public StandardJsonResult Delete(string employeeID)
        {
            bool isSuccess = false;
            var jsonResult = StandardAction(() =>
            {
                var userInfo = GetUserInfo();
                isSuccess = this.memberService.Delete(employeeID, userInfo.Name);
            });
            jsonResult.Success = isSuccess;
            jsonResult.Message = isSuccess ? "成功" : "失败";
            return jsonResult;
        }
    }
}
