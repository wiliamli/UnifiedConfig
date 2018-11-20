using Jwell.Domain.Entities.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Jwell.Domain.Entities
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Table("Config_ConfigItem")]
    public class ConfigItem : BaseEntity
    {
        /// <summary>
        /// 配置项编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string ConfigNumber { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 配置环境
        /// </summary>
        [Required]
        public ConfigEnvironment ConfigEnvironment { get; set; }

        ///// <summary>
        ///// 配置项编号
        ///// </summary>
        //[Required]
        //[StringLength(36)]
        //public string ItemNumber { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        [Required]
        [StringLength(36)]
        public string Key { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Required]
        [StringLength(36)]
        public string Value { get; set; }

        /// <summary>
        /// 配置项修改状态
        /// </summary>
        [Required]
        public ConfigItemStatus Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime ModifiedTime { get; set; }
    }

    /// <summary>
    /// 配置环境
    /// </summary>
    public enum ConfigEnvironment : byte
    {
        /// <summary>
        /// 开发阶段
        /// </summary>
        [Description("开发阶段")]
        Test = 0,

        /// <summary>
        /// 测试阶段
        /// </summary>
        [Description("测试阶段")]
        QA = 1,

        /// <summary>
        /// 灰度阶段
        /// </summary>
        [Description("灰度阶段")]
        Grey = 2,

        /// <summary>
        /// 生产阶段
        /// </summary>
        [Description("生产阶段")]
        Product = 3
    }

    public enum ConfigItemStatus : byte
    {
        /// <summary>
        /// 添加待推送
        /// </summary>
        [Description("添加待推送")]
        Add = 1,

        /// <summary>
        /// 修改待推送
        /// </summary>
        [Description("修改待推送")]
        Edit = 2,

        /// <summary>
        /// 删除待推送
        /// </summary>
        [Description("删除待推送")]
        Delete = 3,

        /// <summary>
        /// 已推送
        /// </summary>
        [Description("已推送")]
        Pushed = 4
    }

    public static class ConfigItemStatusExt
    {
        public static string GetDescription(this ConfigItemStatus value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? string.Empty : attribute.Description;
        }
    }
}
