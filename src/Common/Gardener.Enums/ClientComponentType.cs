using System.ComponentModel;

namespace Gardener.Enums;

public enum ClientComponentType
{
    [Description("文本输入")]
    Input,

    [Description("数字输入")]
    InputNumber,

    [Description("单选框")]
    Select,

    [Description("多选框")]
    MultiSelect,

    [Description("树形选框")]
    TreeSelect,

    [Description("开关")]
    Switch,

    [Description("日期时间")]
    DateTime,

    [Description("图片")]
    Image,

    /// <summary>
    /// wwwroot 之外的图片
    /// TODO：目前采用图片转Base64，
    /// 在Dto里面新建一个字段存储Base64，前端用base64呈现，获取方法：ImageHelper.ImageToBase64(imageRemotePath)
    /// </summary>
    [Description("远程图片")]
    RemoteImage,
}