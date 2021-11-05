// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Components;
using Gardener.Client.Base.Model;
using Gardener.Email.Dtos;
using System;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailTemplate : TableBase<EmailTemplateDto, Guid, EmailTemplateEdit>
    {
        public EmailTemplate() : base(new DrawerSettings { Width = 800 })
        { 
        
        }
    }
}
