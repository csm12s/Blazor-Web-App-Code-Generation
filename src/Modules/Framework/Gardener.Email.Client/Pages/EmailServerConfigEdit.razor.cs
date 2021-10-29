// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Enums;
using System;
using System.Collections.Generic;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailServerConfigEdit : EditDrawerBase<EmailServerConfigDto, int>
    {
        private IEnumerable<string> _tags
        {
            get
            {
                return _editModel.Tags?.Split(",");
            }
            set
            {
                _editModel.Tags = string.Join(",", value);

            }
        }

    }
}
