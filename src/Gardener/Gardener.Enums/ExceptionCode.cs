// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 异常状态码
    /// 详细提示见:applicationsettings.json/ErrorCodeMessageSettings
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 用户锁定
        /// </summary>
        [Description("用户锁定")]
        USER_LOCKED,
        /// <summary>
        /// 用户密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        USER_NAME_OR_PASSWORD_ERROR,
        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        USER_NAME_REPEAT,
        /// <summary>
        /// 资源键值重复
        /// </summary>
        [Description("资源键值重复")]
        RESOURCE_KEY_REPEAT,
        /// <summary>
        /// 刷新token不存在或已过期
        /// </summary>
        [Description("刷新token不存在或已过期")]
        REFRESHTOKEN_NO_EXIST_OR_EXPIRE,
        /// <summary>
        /// 未包含文件
        /// </summary>
        [Description("未包含文件")]
        NO_INCLUD_FILE,
        /// <summary>
        /// 条件组中的操作类型错误
        /// </summary>
        [Description("条件组中的操作类型错误")]
        FILTER_GROUP_OPERATE_ERROR,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("指定的属性“{0}”在类型“{1}”中不存在")]
        FIELD_IN_TYPE_NOT_FOUND
    }
}
