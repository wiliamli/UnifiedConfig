using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 配置项-服务-接口
    /// </summary>
    public interface IConfigItemService : IApplicationService
    {
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="param">配置项添加参数</param>
        /// <param name="createdBy">创建人</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        bool Add(ConfigItemAddParam param, string createdBy, ref string errorMsg);

        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="number">配置项编号</param>
        /// <param name="param">配置项修改参数</param>
        /// <param name="modifiedBy">修改人</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        bool Edit(string number, ConfigItemEditParam param, string modifiedBy, ref string errorMsg);

        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="number">配置项编号</param>
        /// <param name="modifiedBy">修改人</param>
        /// <returns></returns>
        bool Delete(string number, string modifiedBy);

        /// <summary>
        /// 获取一条配置项
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        ConfigItemEditDto Get(string number);

        /// <summary>
        /// 推送配置
        /// </summary>
        /// <param name="number">配置项编号数组</param>
        /// <param name="modifiedBy">修改人</param>
        /// <returns></returns>
        bool Push(string[] numbers, string modifiedBy);
    }
}
