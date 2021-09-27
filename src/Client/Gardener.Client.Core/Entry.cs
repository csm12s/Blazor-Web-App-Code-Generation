// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Reflection;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 入口
    /// </summary>
    public static class Entry
    {
        private static readonly Assembly[] assemblies =
         {
            typeof(Entry).Assembly,
            typeof(Base.Entry).Assembly,
            typeof(UserCenter.Client.Entry).Assembly,
            typeof(Attachment.Client.Entry).Assembly,
            typeof(Audit.Client.Entry).Assembly,
            typeof(Authentication.Client.Entry).Assembly,
            typeof(CodeGeneration.Client.Entry).Assembly,
            typeof(Swagger.Client.Entry).Assembly,
            typeof(ImageVerifyCode.Client.Entry).Assembly,
        };

        public static Assembly[] GetAssemblies()
        {
            return assemblies;
        }
    }
}
