// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.EventBus;
using Gardener.EntityFramwork.Event;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gardener.Audit.Core.Subscribe
{
    public class GardenerDbContextSavedSubscriber
    {

        public static void Subscribe()
        {
            MessageCenter.Subscribe<GardenerDbContextSavedChangesEvent>(nameof(GardenerDbContextSavedChangesEvent), (i, p) =>
            {
                if (p is GardenerDbContextSavedChangesEvent e)
                {
                    IAuditService auditService = App.GetService<IAuditService>();
                    auditService.SavedChangesEvent(e.Data as SaveChangesCompletedEventData);
                }
            });

            MessageCenter.Subscribe<GardenerDbContextSavingChangesEvent>(nameof(GardenerDbContextSavingChangesEvent), (i, p) =>
            {
                if (p is GardenerDbContextSavingChangesEvent e)
                {
                    IAuditService auditService = App.GetService<IAuditService>();
                    auditService.SavingChangesEvent(e.Data as SaveChangesCompletedEventData);
                }
            });
        }
    }
}
