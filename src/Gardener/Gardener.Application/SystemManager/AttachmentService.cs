// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mapster;
using System.IO;
using Gardener.Core.FileStore;
using System;
using Furion.FriendlyException;
using Gardener.Enums;
using Gardener.Common;
using Gardener.Application.Interfaces;
using System.Linq;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 附件服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class AttachmentService : ServiceBase<Attachment, AttachmentDto, Guid>, IAttachmentService
    {
        private readonly IFileStoreService fileStoreService;
        private readonly IRepository<Attachment> repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="fileStoreService"></param>
        public AttachmentService(IRepository<Attachment> repository, IFileStoreService fileStoreService) : base(repository)
        {
            this.fileStoreService = fileStoreService;
            this.repository = repository;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="input"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<UploadAttachmentOutput> Upload([FromForm] UploadAttachmentInput input, IFormFile file)
        {
            if (file == null) throw Oops.Oh(ExceptionCode.NO_INCLUD_FILE);

            UploadAttachmentOutput uploadOutput = new UploadAttachmentOutput();
            AttachmentDto attachment = new AttachmentDto();
            input.Adapt(attachment);
            attachment.ContentType = file.ContentType;
            attachment.FileType = FileTypeDistinguishHelper.GetAttachmentFileType(file.ContentType);
            attachment.OriginalName = file.FileName;
            attachment.Size = file.Length;
            attachment.Suffix = Path.GetExtension(file.FileName).ToLower();
            string fileName = (Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)).ToLower();
            attachment.Name = fileName;
            string path = $"{input.BusinessType}/{DateTime.Now.ToString("yyyMMdd")}/".ToLower();
            attachment.Path = path;
            string url = await fileStoreService.Save(file.OpenReadStream(), path + fileName);
            attachment.Url = url;
            attachment.CreatedTime = DateTime.Now;
            var entity = await base.Insert(attachment);
            uploadOutput.Url = url;
            uploadOutput.Id = entity.Id;
            return uploadOutput;
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AttachmentDto> Insert(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        public override async Task<bool> Delete(Guid id)
        {
            Attachment attachment = await repository.FindAsync(id);
            if (attachment == null) return false;
            await attachment.DeleteAsync();
            fileStoreService.Delete(Path.Combine(attachment.Path, attachment.Name));
            return true;
        }
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        [HttpPost]
        public override async Task<bool> Deletes(Guid[] ids)
        {
            foreach (Guid id in ids)
            {
                if (!await Delete(id)) { return false; }
            }
            return true;
        }
        /// <summary>
        /// 搜索附件
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpPost]
        [NonValidation]
        public async Task<PagedList<AttachmentDto>> Search(AttachmentSearchInput searchInput)
        {

            IQueryable<Attachment> queryable = repository.Where(x => x.IsDeleted == false);
            AttachmentDto attachment = searchInput.SearchData;
            if (attachment != null)
            {
                queryable = queryable
                    .Where(!string.IsNullOrEmpty(attachment.BusinessId), x => x.BusinessId.Equals(attachment.BusinessId))
                    .Where(attachment.BusinessType.HasValue, x => x.BusinessType.Equals(attachment.BusinessType.Value))
                    .Where(attachment.FileType.HasValue, x => x.FileType.Equals(attachment.FileType.Value));
            }
            return await queryable.OrderConditions(searchInput.OrderConditions).Select(x => x.Adapt<AttachmentDto>()).ToPagedListAsync(searchInput);

        }
    }
}
