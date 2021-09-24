// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Client.Base
{
    public static class ComponentUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dtos"></param>
        /// <param name="childrenFunc"></param>
        /// <param name="valueFunc"></param>
        /// <param name="labelFunc"></param>
        /// <returns></returns>
        public static List<CascaderNode> DtoConvertToCascaderNode<TDto>(IEnumerable<TDto> dtos,Func<TDto,IEnumerable<TDto>> childrenFunc,Func<TDto, String> labelFunc, Func<TDto, String> valueFunc,String [] disabledIds=null)
        {
            List<CascaderNode> nodes = new List<CascaderNode>();
            //生成级联对象
            if (dtos != null)
            {
                foreach (var item in dtos)
                {
                    CascaderNode node = new CascaderNode();
                    nodes.Add(node);
                    node.Label = labelFunc(item);
                    node.Value = valueFunc(item);
                    node.Disabled = disabledIds == null ? false : disabledIds.Any(x => x.Equals(node.Value));
                    node.Children = DtoConvertToCascaderNode<TDto>(childrenFunc(item),childrenFunc,labelFunc,valueFunc, disabledIds);
                }
            }
            return nodes;

        }

    }
}
