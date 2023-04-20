// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mapster;
using System.IO;
using System;
using Furion.FriendlyException;
using Gardener.Enums;
using Gardener.Attachment.Dtos;
using Gardener.FileStore;
using Gardener.Attachment.Core;
using Gardener.Common;
using Gardener.EntityFramwork;
using System.Collections.Generic;
using Gardener.Attachment.Enums;
using Gardener.Authentication.Core;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Attachment.Services
{
    /// <summary>
    /// 附件服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class AttachmentService : ServiceBase<Domains.Attachment, AttachmentDto, Guid>, IAttachmentService
    {
        private readonly IFileStoreService fileStoreService;
        private readonly IRepository<Domains.Attachment> repository;
        private readonly IIdentityService identityService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="fileStoreService"></param>
        /// <param name="identityService"></param>
        public AttachmentService(IRepository<Domains.Attachment> repository, IFileStoreService fileStoreService, IIdentityService identityService) : base(repository)
        {
            this.fileStoreService = fileStoreService;
            this.repository = repository;
            this.identityService = identityService;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <remarks>
        /// 上传单个附件
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="file"></param>
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
            string savePartialPath = $"{input.BusinessType}/{DateTime.Now.ToString("yyyMMdd")}/".ToLower();
            attachment.Path = savePartialPath;

            // save file
            string url = await fileStoreService.Save(file.OpenReadStream(), savePartialPath + fileName);
            if (!string.IsNullOrEmpty(input.FileSavePath))
            {
                await fileStoreService.SaveToLocal(file.OpenReadStream(), input.FileSavePath);
            }
            else if (!string.IsNullOrEmpty(input.FileSaveFolder))
            {
                var fileExt = Path.GetExtension(file.FileName);
                var originName = file.FileName.Replace(fileExt, "");
                var newName = originName
                    + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
                    + "_" + IdHelper.GetGuid32()
                    + fileExt;

                await fileStoreService.Save(file.OpenReadStream(),
                    Path.Combine(input.FileSaveFolder, newName));
            }

            attachment.Url = url;
            attachment.CreatedTime = DateTime.Now;
            var entity = await base.Insert(attachment);
            uploadOutput.Url = url;
            uploadOutput.Id = entity.Id;
            return uploadOutput;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<AttachmentDto> Insert(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<bool> Update(AttachmentDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除
        /// </remarks>
        /// <param name="id"></param>
        public override async Task<bool> Delete(Guid id)
        {
            Domains.Attachment attachment = await repository.FindAsync(id);
            if (attachment == null) return false;
            await repository.DeleteAsync(attachment);
            fileStoreService.Delete(Path.Combine(attachment.Path, attachment.Name));
            return true;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据主键批量删除
        /// </remarks>
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
        /// 获取远程图片
        /// </summary>
        /// <param name="remoteFilePath"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> GetRemoteImage([FromBody] string remoteFilePath)
        {
            var image64 = ImageHelper.ImageToBase64(remoteFilePath);
            return Task.FromResult(image64);
        }
        /// <summary>
        /// 获取我的某一类型附件数据
        /// </summary>
        /// <param name="attachmentBusinessType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AttachmentDto>> GetMyAttachments(AttachmentBusinessType attachmentBusinessType)
        {
            var identity = identityService.GetIdentity();
            if (identity == null)
            {
                return new AttachmentDto[0];
            }

            return await repository.AsQueryable(false)
                 .Where(x => x.BusinessType.Equals(attachmentBusinessType) && identity.Id.Equals(x.CreateBy) && identity.IdentityType.Equals(x.CreateIdentityType))
                 .OrderBy(x => x.CreatedTime)
                 .Select(x => x.Adapt<AttachmentDto>())
                 .ToListAsync();
        }
    }
}
