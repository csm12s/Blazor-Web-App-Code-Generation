// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Domains
{
    /// <summary>
    /// 代码生成设置信息
    /// </summary>
    [Description("代码生成设置")]
    public class EntityCodeGenerationSetting : GardenerEntityBase
    {
        /// <summary>
        /// 实体全称
        /// </summary>
        [Required]
        [DisplayName("实体全称")]
        public string EntityFullName { get; set; } = null!;
        /// <summary>
        /// 控制器路由
        /// </summary>
        [DisplayName("控制器路由")]
        public string? ControllerRoute { get; set; }
        /// <summary>
        /// 控制器分组   
        /// </summary>
        [DisplayName("控制器分组")]
        public string? ControllerGroup { get; set; }
        /// <summary>
        /// 模块名称   
        /// </summary>
        [DisplayName("模块名称")]
        public string? ModuleName { get; set; }


    }
}
