using Gardener.Client.Base.PrintingJs;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Services
{
    /// <summary>
    /// 打印服务
    /// </summary>
    public interface IPrintingService
    {
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task Print(PrintOptions options);
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printable"></param>
        /// <param name="printType"></param>
        /// <returns></returns>
        Task Print(string printable, PrintType printType = PrintType.Pdf);
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printable"></param>
        /// <param name="showModal"></param>
        /// <param name="printType"></param>
        /// <returns></returns>
        Task Print(string printable, bool showModal, PrintType printType = PrintType.Pdf);
    }
}