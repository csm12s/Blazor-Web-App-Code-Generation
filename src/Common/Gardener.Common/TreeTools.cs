// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Gardener.Common
{
    /// <summary>
    /// 树操作工具
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
    }
}
