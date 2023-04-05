// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Common
{
    /// <summary>
    /// RESTful 风格结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? StatusCode { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        private T? data;
        /// <summary>
        /// 数据
        /// </summary>
        public T? Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value ?? default(T);
            }
        }

        /// <summary>
        /// 执行成功
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object? Errors { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public object? ErrorCode { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object? Extras { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

    }
}
