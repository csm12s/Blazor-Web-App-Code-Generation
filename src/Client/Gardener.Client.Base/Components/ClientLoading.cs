// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 客户端加载中标记
    /// </summary>
    public class ClientLoading
    {   
        /// <summary>
        /// 加载中
        /// </summary>
        public bool Value { get; set; }=false;
        /// <summary>
        /// 加载中次数
        /// </summary>
        private int loadingCount = 0;

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            loadingCount++;
            if (!Value)
            {
                Value = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            loadingCount--;
            if (Value && loadingCount<=0)
            {
                Value = false;
                return true;
            }
            return false;
        }
    }
}
