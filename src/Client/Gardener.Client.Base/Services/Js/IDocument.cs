// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public interface IDocument
    {
        Task SetTitle(string title);

        Task DownloadFile(string url);
        /// <summary>
        /// 如果有滚动条滚动到最下面
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        Task ScrollBarToBottom(string boxId);
        /// <summary>
        /// 赋值text到粘贴板
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        Task copyTextToClipboard(string text);

    }
}