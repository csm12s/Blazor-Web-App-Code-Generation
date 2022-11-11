using System.ComponentModel;

namespace Gardener.Enums;

public enum ClientComponentType
{
    [Description("输入")]
    Input,

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

}