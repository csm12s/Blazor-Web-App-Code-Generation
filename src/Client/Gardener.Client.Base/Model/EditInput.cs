// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EditInput<TKey>
    {
        /// <summary>
        /// 选中的节点主键
        /// </summary>
        public TKey Id { get; set; } = default(TKey);
        /// <summary>
        /// 0添加
        /// 1编辑
        /// </summary>
        public EditInputType Type { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EditInput<TKey> IsAdd()
        {
            return new EditInput<TKey> { Type = EditInputType.Add };
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EditInput<TKey> IsAdd(TKey id)
        {
            return new EditInput<TKey> { Id = id, Type = EditInputType.Add };
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EditInput<TKey> IsEdit(TKey id)
        {
            return new EditInput<TKey> { Id = id, Type = EditInputType.Edit };
        }


    }
    /// <summary>
    /// 类型
    /// </summary>
    public enum EditInputType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit
    }
}
