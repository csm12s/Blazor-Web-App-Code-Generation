﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Email.Dtos;
using System;
using System.Collections.Generic;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailServerConfigEdit : EditOperationDialogBase<EmailServerConfigDto, Guid>
    {
        private IEnumerable<string> _tags
        {
            get
            {
                return _editModel.Tags?.Split(",")??new string[0];
            }
            set
            {
                _editModel.Tags = string.Join(",", value);

            }
        }

    }
}
