// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;
using Gardener.Client.Base;

namespace Gardener.Client.AntDesignUi.Base.Constants
{
    /// <summary>
    /// 客户端常量配置
    /// </summary>
    public partial class ClientConstant : Gardener.Client.Base.Constants.ClientConstant
    {

        /// <summary>
        /// 操作按钮大小
        /// </summary>
        public readonly static string OperationButtonSize = "default";

        /// <summary>
        /// 底部友链
        /// </summary>
        public readonly static LinkItem[] FooterLinks =
        {
                new LinkItem{ Key = "Furion", BlankTarget = true, Title = "Furion" ,Href="https://gitee.com/monksoul/Furion"},
                new LinkItem{ Key = "Ant Design Blazor", BlankTarget = true, Title = "Ant Design",Href="https://github.com/ant-design-blazor/ant-design-blazor"},
                new LinkItem{ Key = "Gardener", BlankTarget = true, Title = "Gardener",Href="https://gitee.com/hgflydream/Gardener"}
        };


        /// <summary>
        /// 默认的操作框配置
        /// </summary>
        /// <remarks>
        /// 每次都是新对象
        /// </remarks>
        public static OperationDialogSettings DefaultOperationDialogSettings
        {
            get
            {
                return new OperationDialogSettings
                {
                    Width = 500,
                    MaskClosable = true,
                    Closable = true,
                    ModalCentered = true,
                    DrawerPlacement = Placement.Right,
                    DialogType = OperationDialogType.Modal
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public readonly static int PageOperationDialogWidth = 1200;//1080;//960;
        /// <summary>
        /// 启用多标签
        /// </summary>
        public readonly static bool EnabledTabs = true;
        /// <summary>
        /// 默认表格大小
        /// </summary>
        /// <remarks>
        /// Default
        /// Middle
        /// Small
        /// </remarks>
        public readonly static TableSize DefaultTableSize = TableSize.Small;



    }
}
