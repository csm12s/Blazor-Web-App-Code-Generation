// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 上传文件类型
    /// </summary>
    public enum AttachmentFileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        IMAGE,
        /// <summary>
        /// excel
        /// </summary>
        [Description("Excel")]
        EXCEL,
        /// <summary>
        /// word
        /// </summary>
        [Description("Word")]
        WORD
    }
}
