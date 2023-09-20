// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.LocalizationLocalizer;

namespace AntDesign
{
    /// <summary>
    /// 确认提示框服务扩展
    /// </summary>
    public static class ConfirmServiceExtension
    {
        /// <summary>
        /// 弹出确认提示框
        /// </summary>
        /// <param name="confirmService"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="confirmIcon"></param>
        /// <param name="btn1Text"></param>
        /// <param name="btn2Text"></param>
        /// <param name="btn3Text"></param>
        /// <returns></returns>
        public async static Task<ConfirmResult> YesNo(this ConfirmService confirmService, string title, string content, ConfirmIcon confirmIcon, string btn1Text = nameof(SharedLocalResource.Yes), string btn2Text = nameof(SharedLocalResource.Cancel), string btn3Text = "")
        {
            return await confirmService.Show(content, title, ConfirmButtons.YesNo, confirmIcon, new ConfirmButtonOptions()
            {
                Button1Props = new ButtonProps
                {
                    ChildContent = Lo.GetValue(btn1Text)
                },
                Button2Props = new ButtonProps
                {
                    ChildContent = Lo.GetValue(btn2Text)
                }
            });
        }
        /// <summary>
        /// 弹出确认提示框
        /// </summary>
        /// <param name="confirmService"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>confirmIcon此时为 <see cref="ConfirmIcon.Question"/></para>
        /// </remarks>
        public async static Task<ConfirmResult> YesNo(this ConfirmService confirmService, string title, string content)
        {
            return await confirmService.YesNo(title, content, ConfirmIcon.Question);
        }
        /// <summary>
        /// 弹出确认提示框
        /// </summary>
        /// <param name="confirmService"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>content此时为 <see cref="SharedLocalResource.OperateConfirmMessage"/></para>
        /// <para>confirmIcon此时为 <see cref="ConfirmIcon.Question"/></para>
        /// </remarks>
        public async static Task<ConfirmResult> YesNo(this ConfirmService confirmService, string title)
        {
            return await confirmService.YesNo(title, Lo.GetValue(nameof(SharedLocalResource.OperateConfirmMessage)), ConfirmIcon.Question);
        }
        /// <summary>
        /// 弹出删除确认提示框
        /// </summary>
        /// <param name="confirmService"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>title此时为 <see cref="SharedLocalResource.Delete"/></para>
        /// <para>content此时为 <see cref="SharedLocalResource.OperateConfirmMessage"/></para>
        /// <para>confirmIcon此时为 <see cref="ConfirmIcon.Question"/></para>
        /// </remarks>
        public async static Task<ConfirmResult> YesNoDelete(this ConfirmService confirmService)
        {
            return await confirmService.YesNo(Lo.GetValue(nameof(SharedLocalResource.Delete)), Lo.GetValue(nameof(SharedLocalResource.OperateConfirmMessage)));
        }
    }
}
