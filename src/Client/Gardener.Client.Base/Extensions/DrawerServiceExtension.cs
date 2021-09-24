// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public static class DrawerServiceExtension
    {

        /// <summary>
        /// 关闭并返回参数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="feedback"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task CloseAsync<TResult>(this IFeedbackRef feedback,TResult result)
        {
            await (feedback as DrawerRef<TResult>)!.CloseAsync(result);
        }
    }
}
