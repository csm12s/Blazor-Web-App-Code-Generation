// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public static class ConfirmServiceExtension
    {
        public async static Task<ConfirmResult> YesNo(this ConfirmService confirmService, string title, string content, ConfirmIcon confirmIcon = ConfirmIcon.Info, string btn1Text = "确定", string btn2Text = "取消", string btn3Text = "")
        {
          return  await confirmService.Show(content, title, ConfirmButtons.YesNo, confirmIcon, new ConfirmButtonOptions()
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
            return await confirmService.YesNo(title, content, ConfirmIcon.Question, LocalizerUtil.GetValue("确定"), LocalizerUtil.GetValue("取消"));
        }

        public async static Task<ConfirmResult> YesNoDelete(this ConfirmService confirmService)
        {
            return await confirmService.YesNoDelete(LocalizerUtil.GetValue("删除"), string.Format(LocalizerUtil.GetValue("确定要执行{0}吗?"), LocalizerUtil.GetValue("删除", true)));
        }
    }
}
