// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace Gardener.Client.Entry
{
    public partial class App
    {
        private FormConfig formConfig = new FormConfig
        {
            ValidateMessages = new FormValidateErrorMessages
            {
                Required = "'{0}' 不能为空"
            }
        };

        

    }
}
