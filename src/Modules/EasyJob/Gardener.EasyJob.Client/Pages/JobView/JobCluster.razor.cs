// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EventBus;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JobCluster : ListOperateTableBase<SysJobClusterDto, int, JobClusterEdit, EasyJobLocalResource>
    {
    }
}
