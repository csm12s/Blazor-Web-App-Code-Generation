// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.WoChat.Client.Components
{
    /// <summary>
    /// 弹框时可传参数
    /// </summary>
    public class WoChatDialogConfig
    {
        /// <summary>
        /// 界面高度
        /// </summary>
        public int Height { get; set; } = 600;
        /// <summary>
        /// 消息标题区高度
        /// </summary>
        public int MessageTitleHeight { get; set; } = 50;
        /// <summary>
        /// 消息输入区高度
        /// </summary>
        public int MessageInputHeight { get; set; } = 200;

        /// <summary>
        /// 默认选择的会话编号
        /// </summary>
        public Guid? DefaultSelectedSessionId { get; set; }
    }
}
