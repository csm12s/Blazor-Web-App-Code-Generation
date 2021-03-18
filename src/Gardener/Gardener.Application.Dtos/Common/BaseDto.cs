// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// dto基础类
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public abstract class BaseDto<Tkey>
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [DisplayName("编号")]
        public Tkey Id { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; }
    }
}
