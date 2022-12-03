// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Gardener.Common;


public interface ITree_Guid<T>
{
    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// 父id
    /// </summary>
    public Guid? ParentId { get; }
    /// <summary>
    /// 子节点
    /// </summary>
    public ICollection<T> Children { get; set; }
}

public interface ITree<T, TKey>
{
    /// <summary>
    /// id
    /// </summary>
    public TKey Id { get; }
    /// <summary>
    /// 父id
    /// </summary>
    public TKey ParentId { get; }
    /// <summary>
    /// 子节点
    /// </summary>
    public ICollection<T> Children { get; set; }
}
