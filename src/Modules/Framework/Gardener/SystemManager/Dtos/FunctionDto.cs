// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 功能信息
    /// </summary>
    [Description("功能信息")]
    public class FunctionDto : BaseDto<Guid>
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Group")]
        public string Group { get; set; } = null!;

        /// <summary>
        /// 服务
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Service")]
        public string Service { get; set; } = null!;

        /// <summary>
        /// 概要
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Summary")]
        public string Summary { get; set; } = null!;

        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Key")]
        public string Key { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Description")]
        public string? Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Path")]
        public string? Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DisplayName("Method")]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        [DisplayName("EnableAudit")]
        public bool EnableAudit { get; set; }
    }
}
