// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Enums
{
    /// <summary>
    /// 异常Code定义
    /// </summary>
    public enum EasyJobExceptionCode 
    {
        /// <summary>
        /// 代码编译失败
        /// </summary>
        Script_Code_Compile_Fail,
        /// <summary>
        /// 脚本代码中，需要定义 [JobDetail] 特性
        /// </summary>
        Script_Code_JobDetail_Not_Find,
        /// <summary>
        /// 脚本代码中，[JobDetail('jobId')]与输入JobId不一致
        /// </summary>
        Script_Code_JobId_Inconsistency
    }
}
