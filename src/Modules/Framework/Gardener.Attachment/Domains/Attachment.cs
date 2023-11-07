// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attachment.Dtos;
using Gardener.Base.Entity;

namespace Gardener.Attachment.Domains
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment : AttachmentDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
       
    }
}
