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
    /// 详细提示配置到:exceptionmessagesettings.json/ErrorCodeMessageSettings
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 身份验证失败
        /// </summary>
        [Description("身份验证失败")]
        UNAUTHORIZED,
        /// <summary>
        /// 拒绝访问资源
        /// </summary>
        [Description("拒绝访问资源")]
        FORBIDDEN,
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
        /// 验证码验证失败
        /// </summary>
        [Description("验证码验证失败")]
        VERIFY_CODE_VERIFICATION_FAILED,
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
        FIELD_IN_TYPE_NOT_FOUND,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("查询的值类型“{0}”未找到转换器")]
        QUERY_VALUE_TYPE_NO_FIND_CONVERTER,
        /// <summary>
        /// 请求的地址无效
        /// </summary>
        [Description("请求的地址无效")]
        REQUEST_URL_IS_INVALID,
        /// <summary>
        /// 刷新token不能用于鉴权
        /// </summary>
        [Description("刷新token不能用于鉴权")]
        REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION,
        /// <summary>
        /// TOKEN无效
        /// </summary>
        [Description("TOKEN无效")]
        TOKEN_INVALID,
        /// <summary>
        /// 客户端登录失败
        /// </summary>
        [Description("客户端登录失败")]
        CLIENT_LOGIN_FAIL,
        /// <summary>
        /// 客户端未找到
        /// </summary>
        [Description("客户端未找到")]
        CLIENT_NO_FIND,
        /// <summary>
        /// 时间戳已过期
        /// </summary>
        [Description("时间戳已过期")]
        TIMESPAN_IS_EXPIRED,
        /// <summary>
        /// 邮件服务器未找到
        /// </summary>
        [Description("邮件服务器未找到")]
        EMAIL_SERVER_NO_FIND,

        /// <summary>
        /// 接口方法需要备注
        /// </summary>
        [Description("接口方法需要备注")]
        Controller_Need_Comment,

        /// <summary>
        /// 
        /// </summary>
        [Description("SugarRepository 初始化错误")]
        Sugar_Repository_Init_Fail,

        /// <summary>
        /// 表名已存在
        /// </summary>
        [Description("表名已存在")]
        Table_Name_Exist,

        /// <summary>
        /// 代码生成模板编译错误
        /// </summary>
        [Description("代码生成模板编译错误")]
        Code_Gen_Template_Compile_Error,

        /// <summary>
        /// 查询出错，检索字段在数据库中可能为null
        /// </summary>
        [Description("查询出错，检索字段在数据库中可能为null")]
        Search_Error_DB_Field_Is_Null,
    }
}
