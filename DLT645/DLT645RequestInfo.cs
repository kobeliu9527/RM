using System;
using System.Collections.Generic;
using System.Text;
using TouchSocket.Core;

namespace DLT645
{
    public class DLT645RequestInfo : IRequestInfo
    {
        /// <summary>
        /// 电表的地址
        /// </summary>
        public byte[] Address { get;  set; }=new byte[6];
        /// <summary>
        /// 协议中的 ControlCode
        /// </summary>
        public byte ControlCode { get;   set; }
        /// <summary>
        /// 协议中的数据长度,不包括校验和字节和结束字节
        /// </summary>
        public byte Length { get;   set; }
        /// <summary>
        /// 是否为ok的应答帧
        /// </summary>
        public bool IsOk { get; set; }
        /// <summary>
        /// 是否还有后续数据
        /// </summary>
        public bool HasData { get; set; }
        /// <summary>
        /// 数据标识结构
        /// </summary>
        public byte[] DataType { get;  set; } = new byte[4];
        /// <summary>
        /// 实际类容
        /// </summary>
        public byte[]? Body { get;  set; }
        /// <summary>
        /// 数据标识,减了33之后的,也倒序了的
        /// </summary>
        public byte[]? DataId { get;  set; }
        /// <summary>
        /// 真实的数据,减了33之后的,也倒序了的
        /// </summary>
        public byte[]? Data { get; set; }
        /// <summary>
        /// 当返回错误的时候
        /// </summary>
        public byte ErrorCode { get;  set; }

    }
   
}
