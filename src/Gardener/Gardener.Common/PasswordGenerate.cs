// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Text;

namespace Gardener.Common
{
    public class PasswordGenerate
    {
        /// <summary>
        /// 生成字母数字密码
        /// </summary>
        /// <param name="length">密码长度</param>
        /// <returns></returns>
        public static string Create(int length)
        {
            int ran;
            byte[] b = new byte[length];
            Random rand = new Random((int)(DateTime.Now.Ticks % 1000000));
            for (int i = 0; i < length; i++)
            {
                do
                {
                    ran = rand.Next(48, 122);
                    b[i] = Convert.ToByte(ran);
                } while ((ran >= 58 && ran < 64) || (ran >= 91 && ran <= 96));
            }
            return Encoding.ASCII.GetString(b);
        }
    }
}
