using Jwell.Framework.Mvc;
using Jwell.UnifiedConfig.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace Jwell.UnifiedConfig.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [UserAuthorizeApi]
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult StandardAction(Action action)
        {
            //var result = new StandardJsonResult();
            //result.StandardAction(action);
            //return result;
            var result = new StandardJsonResult();
            if (ModelState.IsValid)
            {
                result.StandardAction(action);
            }
            else
            {
                result.Success = false;
                result.Message = ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult<T> StandardAction<T>(Func<T> action) where T : new()
        {
            var result = new StandardJsonResult<T>();
            if (ModelState.IsValid)
            {
                result.StandardAction(() =>
                {
                    result.Data = action();
                });
            }
            else
            {
                result.Success = false;
                result.Message = ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage;
                result.Data = new T();
            }
            return result;
        }

        [HttpPost]
        public async Task<StandardJsonResult<T>> StandardAction<T>(Func<Task<T>> action)
        {
            var result = new StandardJsonResult<T>();
             result.StandardAction(async () =>
            {
                result.Data = await action();
            });
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        protected UserInfo GetUserInfo()
        {
            return ValidityProver.Prover.UserInfo;
        }
    }
}