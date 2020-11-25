// -----------------------------------------------------------------------------
// 文件头
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
