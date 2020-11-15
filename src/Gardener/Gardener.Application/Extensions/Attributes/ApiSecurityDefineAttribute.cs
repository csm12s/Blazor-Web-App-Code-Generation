// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// api 安全定义特效
    /// </summary>
    public class ApiSecurityDefineAttribute : SecurityDefineAttribute
    {
        /// <summary>
        /// api 安全定义特效
        /// </summary>
        /// <param name="apiName">api名称</param>
        public ApiSecurityDefineAttribute(string apiName) : base(null)
        {
            this.ApiName = apiName;
        }
        /// <summary>
        /// api名称
        /// </summary>
        public string ApiName { get; set; }
    }
}
