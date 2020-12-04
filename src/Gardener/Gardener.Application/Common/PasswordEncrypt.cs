// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DataEncryption;

namespace Gardener.Application
{
    /// <summary>
    /// 加密密码
    /// </summary>
    public class PasswordEncrypt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string Encrypt (string password,string encryptKey)
        {
            var encryptedPassword = MD5Encryption.Encrypt(password+ encryptKey);
            return encryptedPassword;
        }
    }
}
