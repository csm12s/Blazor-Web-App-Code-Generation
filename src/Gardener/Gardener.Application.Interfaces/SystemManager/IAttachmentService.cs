// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface IAttachmentService : IApplicationServiceBase<AttachmentDto, Guid>
    {
        Task<bool> Delete(Guid id);
        Task<bool> Deletes(Guid[] ids);
        Task<AttachmentDto> Insert(AttachmentDto input);
        Task<bool> Update(AttachmentDto input);
        Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file);
        /// <summary>
        /// 搜索附件
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        Task<PagedList<AttachmentDto>> Search(AttachmentSearchInput searchInput);
    }
}