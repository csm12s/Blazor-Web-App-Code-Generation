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
    public class FunctionDto : BaseDto<Guid>
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("分组")]
        public string Group { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("服务")]
        public string Service { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("概要")]
        public string Summary { get; set; }

        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("唯一键")]
        public string Key { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("地址")]
        public string Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DisplayName("请求方法")]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        [DisplayName("启用审计")]
        public bool EnableAudit { get; set; }
    }
}
