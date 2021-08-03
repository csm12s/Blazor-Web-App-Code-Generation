// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Collections.Generic;

namespace Gardener.Client
{
    public static class AntTreeExtension
    {
        
        /// <summary>
        /// 获取所有上级node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<TreeNode<T>> GetParents<T>(this Tree<T> tree, TreeNode<T> node)
        {
            List<TreeNode<T>> parents = new List<TreeNode<T>>();
            if (node.ParentNode != null)
            {
                parents.Add(node.ParentNode);
                parents.AddRange(tree.GetParents(node.ParentNode));
            }
            return parents;
        }
    }
}
