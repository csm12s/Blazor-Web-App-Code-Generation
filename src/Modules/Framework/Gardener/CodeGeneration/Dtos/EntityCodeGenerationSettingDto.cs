// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 代码生成设置信息
    /// </summary>
    [Description("代码生成设置")]
    public class EntityCodeGenerationSettingDto : BaseDto<int>
    {
        /// <summary>
        /// 实体全称
        /// </summary>
        [Required]
        [DisplayName("实体全称")]
        public string EntityFullName { get; set; }
        /// <summary>
        /// 控制器路由
        /// </summary>
        [DisplayName("控制器路由")]
        public string ControllerRoute { get; set; }
        /// <summary>
        /// 控制器分组   
        /// </summary>
        [DisplayName("控制器分组")]
        public string ControllerGroup { get; set; }
        /// <summary>
        /// 模块名称   
        /// </summary>
        [DisplayName("模块名称")]
        public string ModuleName { get; set; }
    }
}
