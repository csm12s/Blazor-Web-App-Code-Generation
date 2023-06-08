// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using System.ComponentModel.DataAnnotations;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.Client.Pages.FunctionView
{
    public partial class FunctionEdit: EditOperationDialogBase<FunctionDto,Guid, SystemManagerResource>
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
