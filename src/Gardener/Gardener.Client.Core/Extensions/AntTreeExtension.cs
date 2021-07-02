// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public static class AntTreeExtension
    {
        /// <summary>
        /// 展开、收起指定nodes及其所有子node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree">tree</param>
        /// <param name="nodes">需要操作的nodes</param>
        /// <param name="flag">true 展开、false 收起</param>
        /// <returns></returns>
        public static async Task ExpandAll<T>(this Tree<T> tree, List<TreeNode<T>> nodes, bool flag)
        {
            foreach (var node in nodes)
            {
                if (node.Expanded != flag) 
                {
                    node.Expand(flag);
                }
                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    await tree.ExpandAll(node.ChildNodes, flag);
                }
            }
        }

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

        /// <summary>
        /// 递归选中节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static async Task CheckAll<T>(this Tree<T> tree, List<TreeNode<T>> nodes, Func<T, bool> flagFunc)
        {
            foreach (var node in nodes)
            {
                var flag = flagFunc(node.DataItem);
                //有变化再进行变更
                if (node.Checked != flag)
                {
                    node.SetChecked(flag);
                }
                await tree.CheckAll(node.ChildNodes, flagFunc);
            }
        }
    }
}
