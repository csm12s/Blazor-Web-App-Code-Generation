// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

namespace Gardener.Client.Models
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

        private T data;
        /// <summary>
        /// 数据
        /// </summary>
        public T Data
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
        public bool Successed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public object Errors { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object Extras { get; set; }
    }
}
