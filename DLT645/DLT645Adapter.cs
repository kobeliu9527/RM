using System;
using System.Collections.Generic;
using System.Text;
using TouchSocket.Core;
using UfoBase;

namespace DLT645
{
    public class DLT645Adapter : CustomDataHandlingAdapter<DLT645RequestInfo>
    {
        private readonly int headerLength;
        private readonly byte headerByte;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HeaderLength">前导字节个数,标准为4,厂家可能修改</param>
        /// <param name="Header">前导字节,默认0xFE</param>
        public DLT645Adapter(int HeaderLength=4, byte Header = 0XFE)
        {
            headerLength = HeaderLength;
            headerByte = Header;
        }
        protected override FilterResult Filter(in ByteBlock byteBlock, bool beCached, ref DLT645RequestInfo request, ref int tempCapacity)
        {
            //4字节前导+1字节固定68+6字节固定地址+1字节固定68+1字节控制码+1字节后续长度+数据标识(一般4字节,出错1字节)+1字节校验和+1字节固定16
            if (byteBlock.CanReadLen < 12+ headerLength)
            {
                return FilterResult.Cache;//当头部都无法解析时，直接缓存
            }
            int pos = byteBlock.Pos;//记录初始游标位置，防止本次无法解析时，回退游标。

            DLT645RequestInfo myRequestInfo = new DLT645RequestInfo();

            //此操作实际上有两个作用，
            //1.填充header
            //2.将byteBlock.Pos递增3的长度。
            byteBlock.Read(out byte[] header, 10 + headerLength);//填充header

            //因为第一个字节表示所有长度，而DataType、OrderType已经包含在了header里面。
            //所有只需呀再读取header[0]-2个长度即可。
            //数据域的长度,8表示8个字节
            byte bodyLength = (byte)(header[9+ headerLength]);

            if (bodyLength+2 > byteBlock.CanReadLen)
            {
                //body数据不足。
                byteBlock.Pos = pos;//回退游标
                return FilterResult.Cache;
            }
            else
            {
                //此操作实际上有两个作用，
                //1.填充body
                //2.将byteBlock.Pos递增bodyLength的长度。

                myRequestInfo.Length = header[9+ headerLength];
                myRequestInfo.ControlCode = header[8+ headerLength];
                Buffer.BlockCopy(header, 1 + headerLength, myRequestInfo.Address, 0, 6);

                //byte[] resultBytes = byteArray[startIndex..(startIndex + length)];
                byteBlock.Read(out byte[] body, bodyLength + 2);
                //考虑做校验和验证??
                myRequestInfo.IsOk = !myRequestInfo.ControlCode.GetBitValue(6);
                if (myRequestInfo.IsOk)
                {
                    var Body = new byte[bodyLength];
                    var DataId = new byte[4];
                    var Data = new byte[bodyLength-4];
                    Buffer.BlockCopy(body, 0, myRequestInfo.Body, 0, bodyLength);
                    Buffer.BlockCopy(body, 0, DataId, 0, 4);
                    Buffer.BlockCopy(body, 4, Data, 0, bodyLength - 4);
                    myRequestInfo.DataId=DataId.ForeachSub(0x33).Reverse();
                    myRequestInfo.Data = Data.ForeachSub(0x33).Reverse();
                }
                else
                {
                    myRequestInfo.ErrorCode = body[0];
                }
                request = myRequestInfo;//赋值ref
                return FilterResult.Success;//返回成功
            }
        }
    }
}
