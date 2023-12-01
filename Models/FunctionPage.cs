using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionPage : EntityBase
    {
        /// <summary>
        /// 页面名字
        /// </summary>
        [SqlSugar.SugarColumn(Length = 20)]
        public string Name { get; set; } = "";
        /// <summary>
        /// 表示这个页面的布局
        /// </summary>
        [SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "nvarchar(max)")]
        [NotNull]
        public ContainerDto? ContainerData { get; set; }

        public ShowMsgType ShowMsgType { get; set; } = ShowMsgType.None;
    }
    public enum ShowMsgType
    {
        /// <summary>
        /// 没有
        /// </summary>
        None,
        /// <summary>
        /// 文本框
        /// </summary>
        Console,
        /// <summary>
        /// 窗体形式的文本框
        /// </summary>
        ConsoleBox,
        /// <summary>
        /// 轻量弹出框,一定时间后自动消失
        /// </summary>
        Toast,
        /// <summary>
        /// 模态弹窗
        /// </summary>
        SweetAlert

    }
}
