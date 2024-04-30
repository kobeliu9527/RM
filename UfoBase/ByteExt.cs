using System;

namespace UfoBase
{
    public static class ByteExt
    {
        #region BCD转换
        /// <summary>
        /// 当获取到的字节数据是BCD码的时候,调用此方法
        /// </summary>
        /// <param name="bcdArray"></param>
        /// <returns></returns>
        public static int BcdByteArrayToDecimal(this byte[] bcdArray)
        {
            int result = 0;
            for (int i = 0; i < bcdArray.Length; i++)
            {
                byte bcdByte = bcdArray[i];
                result = result * 100 + ((bcdByte >> 4) * 10) + (bcdByte & 0x0F);
            }
            return result;
        }

        #endregion
        /// <summary>
        /// 将字节数组的每一个值都增加或者减少
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="value">为负数表示减法</param>
        public static byte[] ForeachAdd(this byte[] bs, byte value)
        {

            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] += value;
            }
            return bs;
        }
        /// <summary>
        /// 将字节数组的每一个值都增加或者减少
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="value">为负数表示减法</param>
        public static byte[] ForeachSub(this byte[] bs, byte value)
        {
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] -= value;
            }
            return bs;
        }
        /// <summary>
        /// 将字节数组倒序
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static byte[] Reverse(this byte[] byteArray)
        {
            int length = byteArray.Length;
            int halfLength = length / 2;
            for (int i = 0; i < halfLength; i++)
            {
                byte temp = byteArray[i];
                byteArray[i] = byteArray[length - i - 1];
                byteArray[length - i - 1] = temp;
            }
            return byteArray;
        }
        #region 转16进制
        private static readonly char[] HexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
};
        /// <summary>
        /// 转换成16进制的字符串
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return string.Empty;
            }

            char[] hexChars = new char[byteArray.Length * 2];

            for (int i = 0; i < byteArray.Length; i++)
            {
                int value = byteArray[i];
                var hexChar = value >> 4;
                hexChars[i * 2] = HexDigits[value >> 4];
                hexChars[i * 2 + 1] = HexDigits[value & 0x0F];
            }
            return new string(hexChars);
        }
        /// <summary>
        /// 转换成16进制的字符串
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] byteArray, string separator)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return string.Empty;
            }
            int byteArrayLength = byteArray.Length;
            int separatorLength = separator.Length;
            int totalLength = byteArrayLength * 2 + (byteArrayLength - 1) * separatorLength;

            char[] hexChars = new char[totalLength];
            int index = 0;

            // 在循环之外预先计算分隔符的总长度
            int separatorTotalLength = (byteArrayLength - 1) * separatorLength;

            // 将分隔符长度检查移到循环之外
            if (separatorLength > 0)
            {
                for (int i = 0; i < byteArrayLength; i++)
                {
                    int value = byteArray[i];
                    hexChars[index++] = HexDigits[value >> 4];
                    hexChars[index++] = HexDigits[value & 0x0F];

                    // 添加分隔符
                    if (i < byteArrayLength - 1)
                    {
                        for (int j = 0; j < separatorLength; j++)
                        {
                            hexChars[index++] = separator[j];
                        }
                    }
                }
            }
            else
            {
                // 无分隔符时的快速路径
                for (int i = 0; i < byteArrayLength; i++)
                {
                    int value = byteArray[i];
                    hexChars[index++] = HexDigits[value >> 4];
                    hexChars[index++] = HexDigits[value & 0x0F];
                }
            }

            return new string(hexChars);
        }
        


        #endregion 转16进制

        public static bool GetBitValue(this byte b, int index)
        {
            byte mask = (byte)(1 << index);
            return (b & mask) != 0;
        }

        public static bool GetBitValue(this sbyte b, int index)
        {
            sbyte mask = (sbyte)(1 << index);
            return (b & mask) != 0;
        }

        public static bool GetBitValue(this short s, int index)
        {
            short mask = (short)(1 << index);
            return (s & mask) != 0;
        }

        public static bool GetBitValue(this ushort us, int index)
        {
            ushort mask = (ushort)(1 << index);
            return (us & mask) != 0;
        }

        public static bool GetBitValue(this int i, int index)
        {
            int mask = 1 << index;
            return (i & mask) != 0;
        }

        public static bool GetBitValue(this uint ui, int index)
        {
            uint mask = (uint)(1 << index);
            return (ui & mask) != 0;
        }

        public static bool GetBitValue(this long l, int index)
        {
            long mask = 1L << index;
            return (l & mask) != 0;
        }

        public static bool GetBitValue(this ulong ul, int index)
        {
            ulong mask = 1UL << index;
            return (ul & mask) != 0;
        }

        public static bool[] GetBitValues(this byte b)
        {
            bool[] boolArray = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                boolArray[i] = (b & (1 << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this sbyte b)
        {
            bool[] boolArray = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                boolArray[i] = (b & (1 << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this short s)
        {
            bool[] boolArray = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                boolArray[i] = (s & (1 << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this int i)
        {
            bool[] boolArray = new bool[32];
            for (int j = 0; j < 32; j++)
            {
                boolArray[j] = (i & (1 << j)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this long l)
        {
            bool[] boolArray = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                boolArray[i] = (l & (1L << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this ushort us)
        {
            bool[] boolArray = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                boolArray[i] = (us & (1 << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this uint ui)
        {
            bool[] boolArray = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                boolArray[i] = (ui & (1 << i)) != 0;
            }
            return boolArray;
        }

        public static bool[] GetBitValues(this ulong ul)
        {
            bool[] boolArray = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                boolArray[i] = (ul & (1UL << i)) != 0;
            }
            return boolArray;
        }
    }
}