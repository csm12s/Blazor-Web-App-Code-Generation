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
    /// 树操作工具 TODO: 这里改成TreeHelper，建议Common里的都叫Helper，其他地方的叫Util、Tool总之统一就行啦
    /// </summary>
    public static class TreeTools
    {
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        public static List<TKey> GetAllChildrenNodes<TKey, TDto>(ICollection<TDto> dtos, Func<TDto, TKey> getId, Func<TDto, ICollection<TDto>> getChildren)
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
        public static List<TKey> GetAllChildrenNodes<TKey, TDto>(TDto dto, Func<TDto, TKey> getId, Func<TDto, ICollection<TDto>> getChildren)
        {
            if (dto == null) return null;
            List<TKey> ids = GetAllChildrenNodes(getChildren(dto),getId,getChildren);
            ids.Add(getId(dto));
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
        public static TDto QueryNode<TDto>(ICollection<TDto> dtos, Func<TDto, bool> query, Func<TDto, ICollection<TDto>> getChildren)
        {
            Console.WriteLine("begin");
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
                    var node= QueryNode(children, query, getChildren);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return default(TDto);
        }

        #region ITree with nullable parent id: Guid
        public static void TreeToList<T>(this IList<T> treeList, IList<T> list)
            where T : class, ITreeGuid<T>
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
        /// List 转 Tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeList"></param>
        /// <param name="parentId">get all children of parentId</param>
        /// <returns></returns>
        public static List<T> ListToTree<T>(List<T> nodeList
            , Guid? parentId = null)
            where T : class, ITreeGuid<T>
        {
            List<T> childTreeList = new List<T>();

            // Get child tree
            foreach (var childTree in nodeList.Where(it => it.ParentId == parentId))
            {
                // Get grand child Tree
                var grandChildTreeList = ListToTree(nodeList, childTree.Id);
                childTree.Children = grandChildTreeList.Count == 0 ? 
                    null 
                    : grandChildTreeList;

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

        #region ITree with not null parent id: int, string
        public static void TreeToList<T, TKey>(this IList<T> treeList, IList<T> list)
            where T : class, ITree<T, TKey>
        {
            foreach (var item in treeList)
            {
                list.Add(item);
                if (item.Children != null && item.Children.Count > 0)
                {
                    TreeToList<T, TKey>((IList<T>)item.Children, list);
                }
            }
        }

        // TODO: 这里或参考上面的ListToTree方法，使用递归
        public static List<T> ListToTree<T, TKey>(this List<T> list) 
            where T : class, ITree<T, TKey>
        {
            List<T> treeList = new List<T>();
            if (list == null) return treeList;
            foreach (var item in list)
            {
                // root, int, string
                if (item.ParentId is int && item.ParentId.Equals(0)
                    || item.ParentId is string && item.ParentId.Equals(""))
                {
                    treeList.Add(item);
                }
                else
                {
                    var parentNode = list.SingleOrDefault(u => u.Id.Equals(item.ParentId));
                    if (parentNode == null)
                    {
                        treeList.Add(item);//没有找到父节点，所以直接加入最上层节点
                    }
                    else
                    {
                        parentNode.Children.Add(item);//加入父节点
                    }
                }
            }
            return treeList;
        }
        #endregion


        // End
    }
}
