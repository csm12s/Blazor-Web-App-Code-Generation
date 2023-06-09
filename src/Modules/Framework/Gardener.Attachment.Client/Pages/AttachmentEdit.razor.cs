// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Attachment.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Attachment.Client.Pages
{
    public partial class AttachmentEdit : EditOperationDialogBase<AttachmentDto, Guid, AttachmentLocalResource>
    {

        [Required(ErrorMessage = "业务类型不能为空")]
        private string _currentEditModelBusinessType
        {
            get
            {
                return _editModel.BusinessType.ToString();
            }
            set
            {
                _editModel.BusinessType = (AttachmentBusinessType)Enum.Parse(typeof(AttachmentBusinessType), value);
            }
        }

        [Required(ErrorMessage = "文件类型不能为空")]
        private string _currentEditModelFileType
        {
            get
            {
                return _editModel.FileType.ToString();
            }
            set
            {
                _editModel.FileType = (AttachmentFileType)Enum.Parse(typeof(AttachmentFileType), value);
            }
        }
    }
}
