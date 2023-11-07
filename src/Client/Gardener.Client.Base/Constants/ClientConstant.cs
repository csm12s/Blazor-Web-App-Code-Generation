// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


namespace Gardener.Client.Base.Constants
{
    /// <summary>
    /// 客户端常量配置
    /// </summary>
    public class ClientConstant
    {
        /// <summary>
        /// 本地化支持语言
        /// </summary>
        public readonly static string[] SupportedCultures = { "zh-CN", "en-US" };

        /// <summary>
        /// 本地化浏览器缓存key
        /// </summary>
        public readonly static string BlazorCultureKey = "BlazorCulture";

        /// <summary>
        /// 本地化默认语言
        /// </summary>
        public readonly static string DefaultCulture = "zh-CN";


        /// <summary>
        /// 时间显示格式化
        /// </summary>
        public readonly static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


        /// <summary>
        /// 时间显示格式化
        /// </summary>
        public readonly static string InputDateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss zzz";

        /// <summary>
        /// 每页数据量大小
        /// </summary>
        public readonly static int PageSize = 15;

        /// <summary>
        /// 通知消息使用MessageBox最大长度超出时，使用通知框
        /// </summary>
        public readonly static int ClientNotifierUseMessageMaxLength = 20;

        /// <summary>
        /// 通知消息弹出时长
        /// </summary>
        public readonly static int ClientNotifierMessageDuration = 3;

    }
}
