// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public static class ConfirmServiceExtension
    {
        public async static Task<ConfirmResult> YesNo(this ConfirmService confirmService, string title, string content, ConfirmIcon confirmIcon = ConfirmIcon.Info, string btn1Text = "确定", string btn2Text = "取消", string btn3Text = "")
        {
            return await confirmService.Show(content, title, ConfirmButtons.YesNo, confirmIcon, new ConfirmButtonOptions()
            {
                Button1Props = new ButtonProps
                {
                    ChildContent = LocalizerUtil.GetValue(btn1Text)
                },
                Button2Props = new ButtonProps
                {
                    ChildContent = LocalizerUtil.GetValue(btn2Text)
                }
            });
        }

        public async static Task<ConfirmResult> YesNoDelete(this ConfirmService confirmService, string title, string content)
        {
            return await confirmService.YesNo(title, content, ConfirmIcon.Question, LocalizerUtil.GetValue(SharedLocalResource.Yes), LocalizerUtil.GetValue(SharedLocalResource.Cancel));
        }

        public async static Task<ConfirmResult> YesNoDelete(this ConfirmService confirmService)
        {
            return await confirmService.YesNoDelete(LocalizerUtil.GetValue(SharedLocalResource.Delete), LocalizerUtil.GetValue(SharedLocalResource.OperateConfirmMessage));
        }
    }
}
