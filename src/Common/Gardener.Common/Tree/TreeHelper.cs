// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Common
{
    /// <summary>
    /// 树操作工具
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        public static List<TKey> GetAllChildrenNodes<TKey, TDto>(ICollection<TDto> dtos, Func<TDto, TKey> getId, Func<TDto, ICollection<TDto>?> getChildren)
        {
            List<TKey> ids = new List<TKey>();
            if (dtos == null)
            {
                return ids;
            }
            foreach (TDto dto in dtos)
            {
                ids.Add(getId(dto));
                var children = getChildren(dto);
                if (children != null)
                {
                    ids.AddRange(GetAllChildrenNodes(children, getId, getChildren));
                }
            }
            return ids;
        }
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        public static List<TKey> GetAllChildrenNodes<TKey, TDto>(TDto dto, Func<TDto, TKey> getId, Func<TDto, ICollection<TDto>?> getChildren)
        {
            List<TKey> ids=new List<TKey>();
            ids.Add(getId(dto));
            var children= getChildren(dto);
            if (children != null)
            {
                ids.AddRange(GetAllChildrenNodes(children, getId, getChildren));
            }
            return ids;
        }
        /// <summary>
        /// 查询树的某个节点
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dtos"></param>
        /// <param name="query"></param>
        /// <param name="getChildren"></param>
        /// <returns></returns>
        public static TDto? QueryNode<TDto>(ICollection<TDto> dtos, Func<TDto, bool> query, Func<TDto, ICollection<TDto>?> getChildren)
        {
            if (dtos == null)
            {
                return default(TDto);
            }
            foreach (TDto dto in dtos)
            {
                if (query(dto))
                {
                    return dto;
                }
                var children = getChildren(dto);
                if (children != null)
                {
                    var node = QueryNode(children, query, getChildren);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return default(TDto);
        }

        #region ITreeNode with Node Id: Guid?
        public static void TreeToList<T>(this IList<T> treeList, IList<T> list)
            where T : class, ITreeNode<T>
        {
            foreach (var item in treeList)
            {
                list.Add(item);
                if (item.Children != null && item.Children.Count > 0)
                {
                    TreeToList<T>((IList<T>)item.Children, list);
                }
            }
        }

        /// <summary>
        /// List to Tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeList"></param>
        /// <param name="parentId">get all children of parentId</param>
        /// <returns></returns>
        public static List<T> ListToTree<T>(List<T> nodeList
            , Guid? parentId = null)
            where T : class, ITreeNode<T>
        {
            List<T> childTreeList = new List<T>();

            // Get child tree
            foreach (var childTree in nodeList.Where(it => it.ParentId == parentId))
            {
                // Get grand child Tree
                var grandChildTreeList = ListToTree(nodeList, childTree.Id);
                childTree.Children = grandChildTreeList;
                childTreeList.Add(childTree);
            }

            return childTreeList;


            // 这里返回了重复的子节点，参考的Caviar

            //public static List<T> ListToTree<T>(this List<T> nodeList)
            //where T : class, ITreeGuid<T>

            //List<T> treeList = new List<T>();
            //if (list == null) return treeList;
            //foreach (var item in list)
            //{
            //    // root, Guid
            //    if (!item.ParentId.HasValue)
            //    {
            //        treeList.Add(item);
            //    }
            //    else
            //    {
            //        var parentNode = list.SingleOrDefault(u => u.Id.Equals(item.ParentId));
            //        if (parentNode == null)
            //        {
            //            treeList.Add(item);//没有找到父节点，所以直接加入最上层节点
            //        }
            //        else
            //        {
            //            parentNode.Children.Add(item);//加入父节点
            //        }
            //    }
            //}
            //return treeList;
        }

        #endregion

        // End
    }
}
