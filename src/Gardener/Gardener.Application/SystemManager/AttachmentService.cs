// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core.Entites;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 附件服务
    /// </summary>
    public class AttachmentService : ServiceBase<Attachment, AttachmentDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AttachmentService(IRepository<Attachment> repository) : base(repository)
        {
        }
    }
}
