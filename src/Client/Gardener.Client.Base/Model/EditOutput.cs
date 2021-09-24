// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    public class EditOutput<TKey>
    {
        public bool Succeeded { get; set; }

        public TKey Id { get; set; }

        public EditOutputType Type { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EditOutput<TKey> Succeed(TKey id)
        {
            return new EditOutput<TKey>() {Id= id ,Succeeded=true,Type=EditOutputType.Succeeded};
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static EditOutput<TKey> Fail()
        {
            return new EditOutput<TKey>() { Succeeded=false, Type = EditOutputType.Failed };
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        public static EditOutput<TKey> Cancel()
        {
            return new EditOutput<TKey>() { Succeeded = false, Type = EditOutputType.Canceled };
        }
    }

    public enum EditOutputType
    {
        Succeeded=0,
        Failed,
        Canceled
    }
}
