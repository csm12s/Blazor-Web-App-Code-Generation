// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Enums;
using Gardener.UserCenter.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Client.Pages.FunctionView
{
    public partial class FunctionEdit: EditDrawerBase<FunctionDto,Guid>
    {
        [Required(ErrorMessage ="不能为空")]
        private string _currentEditModelHttpMethodType
        {
            get
            {
                return _editModel.Method.ToString();
            }
            set
            {
                _editModel.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), value);
            }
        }
        
       
    }
}
