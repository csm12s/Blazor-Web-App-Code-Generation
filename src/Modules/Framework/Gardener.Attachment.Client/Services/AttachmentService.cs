// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Dtos;
using Gardener.Attachment.Services;
using Gardener.Client.Base;
using Gardener.EntityFramwork.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Attachment.Client.Services
{
    [ScopedService]
    public class AttachmentService : ClientServiceBase<AttachmentDto, Guid>, IAttachmentService
    {

        private readonly static string controller = "attachment";
        private readonly IApiCaller apiCaller;

        public AttachmentService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<PagedList<AttachmentDto>> Search(int? businessType, int? fileType, string businessId, string order = "desc", int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"businessType",businessType },
                {"fileType",fileType },
                {"order",order }
            };
            return await apiCaller.GetAsync<PagedList<AttachmentDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
