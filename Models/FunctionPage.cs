using Models.System;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
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
        /// 页面类型 todo:如果一个页面既是网页也是移动端,会被限制,考虑放到下级?
        /// </summary>
        public PageType PageType { get; set; }
        /// <summary>
        /// 表示这个页面的布局
        /// </summary>
        [SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "nvarchar(max)")]
        [NotNull]
        //[JsonIgnore] 
        public ContainerDto? ContainerData { get; set; }
        /// <summary>
        /// 消息框的类型
        /// </summary>
        public ShowMsgType ShowMsgType { get; set; } = ShowMsgType.None;
        /// <summary>
        /// 这个功能属于那些功能组
        /// </summary>
        [Navigate(
            typeof(RelationFunctionAndFunctionGroup),
            nameof(RelationFunctionAndFunctionGroup.FunctionPageId),
            nameof(RelationFunctionAndFunctionGroup.FunctionGroupId))]
        public List<FunctionGroup>? FunctionGroups { get; set; }
        [Navigate(typeof(RoleFunction), nameof(RoleFunction.FunctionPageId), nameof(RoleFunction.RoleId))]
          public List<Role>? Roles { get; set; }
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
    /// <summary>
    /// 生成的界面类型
    /// </summary>
    public enum PageType
    {
        网页,
        /// <summary>
        /// 
        /// </summary>
        Windows桌面,
        /// <summary>
        /// 
        /// </summary>
        移动端,
    }
}
