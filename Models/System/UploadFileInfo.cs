using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class UploadFileInfo:EntityBase
    {
        /// <summary>
        /// 文件来源人
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 文件来源Ip地址
        /// </summary>
        public string? SrcIpAddress { get; set; }
        /// <summary>
        /// 为了安全,重新定义的名字
        /// </summary>
        public string? NewName { get; set; }
        /// <summary>
        /// 文件上传进来时候的名字
        /// </summary>
        public string? FileName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string? ContentType { get; set; }
    }
}
