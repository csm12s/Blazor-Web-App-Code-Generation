using System.ComponentModel;

namespace Gardener.Enums;
/// <summary>
/// 客户端组件类型
/// </summary>
public enum ClientComponentType
{
    /// <summary>
    /// 文本输入
    /// </summary>
    [Description("文本输入")]
    Input,
    /// <summary>
    /// 数字输入
    /// </summary>
    [Description("数字输入")]
    InputNumber,
    /// <summary>
    /// 单选框
    /// </summary>
    [Description("单选框")]
    Select,
    /// <summary>
    /// 多选框
    /// </summary>
    [Description("多选框")]
    MultiSelect,
    /// <summary>
    /// 树形选框
    /// </summary>
    [Description("树形选框")]
    TreeSelect,
    /// <summary>
    /// 开关
    /// </summary>
    [Description("开关")]
    Switch,
    /// <summary>
    /// 日期时间
    /// </summary>
    [Description("日期时间")]
    DateTime,
    /// <summary>
    /// 图片
    /// </summary>
    [Description("图片")]
    Image,

    /// <summary>
    /// 远程图片
    /// </summary>
    /// <remarks>
    /// wwwroot 之外的图片， 目前采用图片转Base64，
    /// 在Dto里面新建一个字段存储Base64，前端用base64呈现，获取方法：ImageHelper.ImageToBase64(imageRemotePath)
    /// </remarks>
    [Description("远程图片")]
    RemoteImage,
    /// <summary>
    /// Enum标签
    /// </summary>
    [Description("Enum标签")]
    TagEnum,
    /// <summary>
    /// YesNo标签
    /// </summary>
    [Description("YesNo标签")]
    TagYesNo,
}