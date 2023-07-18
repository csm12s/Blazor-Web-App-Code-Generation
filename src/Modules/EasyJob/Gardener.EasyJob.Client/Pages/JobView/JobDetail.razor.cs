using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务详情列表页
    /// </summary>
    public partial class JobDetail : ListOperateTableBase<SysJobDetailDto, int, JobDetailEdit, EasyJobLocalResource>
    {
    }
}
