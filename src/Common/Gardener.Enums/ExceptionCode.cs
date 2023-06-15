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
        /// 系统异常默认提示
        /// </summary>
        [Description("系统异常默认提示")]
        System_Default_Error_Message,
        /// <summary>
        /// 身份验证失败
        /// </summary>
        [Description("身份验证失败")]
        Unauthorized,
        /// <summary>
        /// 拒绝访问资源
        /// </summary>
        [Description("拒绝访问资源")]
        Forbidden,
        /// <summary>
        /// 用户锁定
        /// </summary>
        [Description("用户锁定")]
        User_Locked,
        /// <summary>
        /// 用户密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        User_Name_Or_Password_Error,
        /// <summary>
        /// 验证码验证失败
        /// </summary>
        [Description("验证码验证失败")]
        Verify_Code_Verification_Failed,
        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        User_Name_Repeat,
        /// <summary>
        /// 资源键值重复
        /// </summary>
        [Description("资源键值重复")]
        Resource_Key_Repeat,
        /// <summary>
        /// 刷新token不存在或已过期
        /// </summary>
        [Description("刷新token不存在或已过期")]
        Refreshtoken_No_Exist_Or_Expire,
        /// <summary>
        /// 未包含文件
        /// </summary>
        [Description("未包含文件")]
        No_Includ_File,
        /// <summary>
        /// 条件组中的操作类型错误
        /// </summary>
        [Description("条件组中的操作类型错误")]
        Filter_Group_Operate_Error,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("指定的属性“{0}”在类型“{1}”中不存在")]
        Field_In_Type_Not_Found,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("查询的值类型“{0}”未找到转换器")]
        Query_Value_Type_No_Find_Converter,
        /// <summary>
        /// 请求的地址无效
        /// </summary>
        [Description("请求的地址无效")]
        Request_Url_Is_Invalid,
        /// <summary>
        /// 刷新token不能用于鉴权
        /// </summary>
        [Description("刷新token不能用于鉴权")]
        Refreshtoken_Cannot_Used_In_Authentication,
        /// <summary>
        /// TOKEN无效
        /// </summary>
        [Description("TOKEN无效")]
        Token_Invalid,
        /// <summary>
        /// 客户端登录失败
        /// </summary>
        [Description("客户端登录失败")]
        Client_Login_Fail,
        /// <summary>
        /// 客户端未找到
        /// </summary>
        [Description("客户端未找到")]
        Client_No_Find,
        /// <summary>
        /// 时间戳已过期
        /// </summary>
        [Description("时间戳已过期")]
        Timespan_Is_Expired,
        /// <summary>
        /// 邮件服务器未找到
        /// </summary>
        [Description("邮件服务器未找到")]
        Email_Server_No_Find,
        /// <summary>
        /// 接口方法需要备注
        /// </summary>
        [Description("接口方法需要备注")]
        Controller_Need_Comment,
        /// <summary>
        /// SugarRepository 初始化错误
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
        /// <summary>
        /// 未找到数据
        /// </summary>
        [Description("未找到数据")]
        Data_Not_Find,
        /// <summary>
        /// 没有权限修改这个数据
        /// </summary>
        [Description("没有权限修改这个数据")]
        No_Permission_Modify_The_Data,
        /// <summary>
        /// 数据键“{0}”唯一性冲突
        /// </summary>
        [Description("数据键“{0}”唯一性冲突")]
        Data_Key_Uniqueness_Conflict,
        /// <summary>
        /// 任务调度不存在
        /// </summary>
        [Description("任务调度不存在")]
        Task_Not_Exist,
        /// <summary>
        /// 已存在同名任务调度
        /// </summary>
        [Description("已存在同名任务调度")]
        Task_Allready_Exist,
        /// <summary>
        /// 字段“{0}”不能为空
        /// </summary>
        [Description("字段“{0}”不能为空")]
        Field_Required,
        /// <summary>
        /// 字段“{0}”禁止修改
        /// </summary>
        [Description("字段“{0}”禁止修改")]
        Field_Cannot_Be_Modified,

    }
}
