﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class OperationDialogInput<TKey>
    {
        /// <summary>
        /// 选中的节点主键
        /// </summary>
        public TKey Id { get; set; } = default(TKey);
        /// <summary>
        /// 0添加
        /// 1编辑
        /// </summary>
        public DrawerInputType Type { get; set; }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OperationDialogInput<TKey> IsAdd()
        {
            return new OperationDialogInput<TKey> { Type = DrawerInputType.Add };
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OperationDialogInput<TKey> IsAdd(TKey id)
        {
            return new OperationDialogInput<TKey> { Id = id, Type = DrawerInputType.Add };
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OperationDialogInput<TKey> IsEdit(TKey id)
        {
            return new OperationDialogInput<TKey> { Id = id, Type = DrawerInputType.Edit };
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OperationDialogInput<TKey> IsSelect(TKey id)
        {
            return new OperationDialogInput<TKey> { Id = id, Type = DrawerInputType.Select };
        }


    }
    /// <summary>
    /// 类型
    /// </summary>
    public enum DrawerInputType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit=1,
        /// <summary>
        /// 查看
        /// </summary>
        Select=2,
    }
}
