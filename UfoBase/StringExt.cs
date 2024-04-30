using System;
using System.Collections.Generic;
using System.Text;

namespace UfoBase
{
    public static class StringExt
    {
        /// <summary>
        /// 将16进制字符串,转换成字节数组
        /// </summary>
        /// <param name="hexString">大小写都可以</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ToByteArray(this string hexString)
        {
            // 移除字符串中的空格和其他分隔符
            hexString = hexString.Replace(" ", "").Replace("-", "");

            // 确保字符串长度为偶数
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("字符串必须是偶数个嘛");
            }

            // 创建一个字节数组来存储结果
            byte[] byteArray = new byte[hexString.Length / 2];

            // 使用 TryParse 转换字符串中的每两个字符为一个字节
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string byteString = hexString.Substring(i, 2);
                if (!byte.TryParse(byteString, System.Globalization.NumberStyles.HexNumber, null, out byteArray[i / 2]))
                {
                    throw new ArgumentException("字符必须是0-9 A-F之间的");
                }
            }

            return byteArray;
        }

    }
}
