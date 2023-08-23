// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.Schedule;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 动态作业编译
    /// </summary>
    public class DynamicJobCompiler : ISingleton
    {
        /// <summary>
        /// 编译代码并返回其中实现 IJob 的类型
        /// </summary>
        /// <param name="script">动态编译的作业代码</param>
        /// <returns></returns>
        public Type? BuildJob(string script)
        {
            var jobAssembly = Schedular.CompileCSharpClassCode(script);
            var jobType = jobAssembly.GetTypes().FirstOrDefault(u => typeof(IJob).IsAssignableFrom(u));
            return jobType;
        }
    }
}
