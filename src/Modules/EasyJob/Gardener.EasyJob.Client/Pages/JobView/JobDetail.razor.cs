﻿using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务详情列表页
    /// </summary>
    public partial class JobDetail : ListOperateTableBase<SysJobDetailDto, int, JobDetailEdit, EasyJobLocalResource>
    {
        [Inject]
        public ISysJobDetailService sysJobDetailService { get; set; } = null!;

        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 1000;
            base.SetOperationDialogSettings(dialogSettings);
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task Pause(int id)
        {
            bool result = await sysJobDetailService.Pause(id);
            if (result)
            {
                //成功
                await base.ReLoadTable();
                MessageService.Success(Localizer.Combination(nameof(EasyJobLocalResource.Pause), nameof(SharedLocalResource.Success)));
            }
            else
            {
                //失败
                MessageService.Error(Localizer.Combination(nameof(EasyJobLocalResource.Pause), nameof(SharedLocalResource.Fail)));
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task Start(int id)
        {
            bool result = await sysJobDetailService.Start(id);
            if (result)
            {
                //成功
                MessageService.Success(Localizer.Combination(nameof(EasyJobLocalResource.Start), nameof(SharedLocalResource.Success)));
                await base.ReLoadTable();
            }
            else
            {
                //失败
                MessageService.Error(Localizer.Combination(nameof(EasyJobLocalResource.Start), nameof(SharedLocalResource.Fail)));
            }
        }
        /// <summary>
        /// 打开日志控制台
        /// </summary>
        /// <param name="jobId"></param>
        private Task OpenJobLogConsole(SysJobDetailDto detail)
        {
            OperationDialogSettings operationDialogSettings = base.GetOperationDialogSettings();

            operationDialogSettings.ModalMaximizable = true;

            return base.OpenOperationDialogAsync<JobLogConsole, JobLogConsoleInput, bool>(detail.Description ?? detail.JobId, new JobLogConsoleInput(detail.JobId), operationDialogSettings: operationDialogSettings);
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnClickRun(int id)
        {
            if (await ConfirmService.YesNo(EasyJobLocalResource.Run) == ConfirmResult.Yes)
            {
                _actionBtnLoading.Start("run" + id);

                bool result = await sysJobDetailService.Run(id);
                if (result)
                {
                    //成功
                    MessageService.Success(Localizer.Combination(nameof(EasyJobLocalResource.Run), nameof(SharedLocalResource.Success)));
                    await base.ReLoadTable();
                }
                else
                {
                    //失败
                    MessageService.Error(Localizer.Combination(nameof(EasyJobLocalResource.Run), nameof(SharedLocalResource.Fail)));
                }
                _actionBtnLoading.Stop("run" + id);
            }
        }
    }
}
