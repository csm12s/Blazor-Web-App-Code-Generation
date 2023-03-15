// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 验证码输入
    /// </summary>
    public abstract class VerifyCodeInput: VerifyCodeBase
    {
        /// <summary>
        /// 创建code参数
        /// 不传时，使用配置文件中配置
        /// </summary>
        public CharacterCodeCreateParam? CreateCodeParam { get; set; }
    }
}
