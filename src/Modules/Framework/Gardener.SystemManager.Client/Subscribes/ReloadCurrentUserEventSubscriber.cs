// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;
using Gardener.SystemManager.Services;
using Gardener.SystemManager.Utils;

namespace Gardener.SystemManager.Client.Subscribes
{
    /// <summary>
    /// 重载用户后
    /// </summary>
    [TransientService]
    public class ReloadCurrentUserEventSubscriber : EventSubscriberBase<ReloadCurrentUserEvent>
    {
        private readonly ICodeTypeService codeTypeService;
        /// <summary>
        /// 登录成功后
        /// </summary>
        /// <param name="codeTypeService"></param>
        public ReloadCurrentUserEventSubscriber(ICodeTypeService codeTypeService)
        {
            this.codeTypeService = codeTypeService;
        }

        public override Task CallBack(ReloadCurrentUserEvent e)
        {
            return CodeUtil.InitAllCode(codeTypeService);
        }
    }
}
