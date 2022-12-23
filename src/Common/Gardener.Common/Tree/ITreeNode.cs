// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Common;


public interface ITreeNode<T>
{
    /// <summary>
    /// Node Id
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// Node ParentId
    /// </summary>
    public Guid? ParentId { get; }
    /// <summary>
    /// Node Children
    /// </summary>
    public ICollection<T> Children { get; set; }
}


