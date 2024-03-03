using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class Ext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="secrity"></param>
        /// <returns></returns>
        public static string ToHashPassword(this string password, string secrity = "ufo")
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 将密码字符串转换为字节数组
                if (!string.IsNullOrEmpty(secrity))
                {
                    password += secrity;
                }
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // 将字节数组转换为十六进制字符串表示
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
