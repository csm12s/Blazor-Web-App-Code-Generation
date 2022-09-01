﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;

namespace Gardener.Client.Base.Constants
{
    /// <summary>
    /// 客户端常量配置
    /// </summary>
    public class ClientConstant
    {
        /// <summary>
        /// 本地化浏览器缓存key
        /// </summary>
        public readonly static string BlazorCultureKey = "BlazorCulture";

        /// <summary>
        /// 时间显示格式化
        /// </summary>
        public readonly static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

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
        /// 时间显示格式化
        /// </summary>
        public readonly static string InputDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss zzz";

        /// <summary>
        /// 每页数据量大小
        /// </summary>
        public readonly static int pageSize = 15;

        /// <summary>
        /// 默认的操作框配置
        /// </summary>
        public readonly static OperationDialogSettings DefaultOperationDialogSettings = new OperationDialogSettings
        {
            Width = 500,
            MaskClosable = true,
            Closable = true,
            ModalCentered=true,
            DrawerPlacement= Placement.Right,
            DialogType= OperationDialogType.Drawer,
        };

    }
}
