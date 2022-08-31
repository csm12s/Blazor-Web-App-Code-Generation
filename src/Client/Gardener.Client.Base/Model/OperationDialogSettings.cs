// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 操作会话框设置
    /// </summary>
    public class OperationDialogSettings
    {

        /// <summary>
        /// 弹出类型
        /// </summary>
        public OperationDialogType DialogType
        {
            get; set;

        } = OperationDialogType.Modal;

        /// <summary>
        /// 是否有关闭按钮
        /// </summary>
        public bool Closable { get; set; } = true;

        /// <summary>
        /// 点击蒙层是否允许关闭
        /// </summary>
        public bool MaskClosable { get; set; } = true;
        /// <summary>
        /// 弹出宽度
        /// </summary>
        public int Width { get; set; } = 500;

        #region  Drawer
        /// <summary>
        /// 抽屉弹出位置
        /// </summary>
        public Placement DrawerPlacement { get; set; } = Placement.Right;

        #endregion


        #region Modal

        /// <summary>
        /// 弹出框是否水平居中
        /// </summary>
        public bool ModalCentered { get; set; } = true;

        #endregion
    }


}
