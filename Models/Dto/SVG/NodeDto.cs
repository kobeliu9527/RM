

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using BootstrapBlazor.Components;
using SqlSugar;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 一个节点模型Dto
    /// </summary>
    public class NodeDto
    {
        #region 外观参数
        /// <summary>
        /// 节点Id,系统全局生成,唯一Key
        /// </summary>
        [DisplayName("节点Id"), Description("节点Id")]
        public string? Id { get; set; }
        /// <summary>
        /// 节点名字
        /// </summary>
        [DisplayName("名字"), Description("节点名字")]
        public string NodeModelName { get; set; } = "节点名";
        /// <summary>
        /// 节点形状,外观,正方形,菱形
        /// </summary>
        [DisplayName("节点形状"), Description("节点形状")]
        public NodeType? NodeType { get; set; }
        [DisplayName("宽")]
        public double Width { get; set; } = NotEntity.Constants.Width;
        [DisplayName("高")]
        public double Height { get; set; }= NotEntity.Constants.Height;
        [DisplayName("半径")]
        public double Radius { get; set; } = NotEntity.Constants.Radius;
        [DisplayName("平行偏移")]
        public double Offset { get; set; } = 20;
        [DisplayName("圆角")]
        public int Corner { get; set; } = NotEntity.Constants.Corner;
        [DisplayName("边框颜色")]
        public string BorderColor { get; set; } = NotEntity.Constants.BorderColor;
        [DisplayName("填充颜色")]
        public string FillColor { get; set; } = NotEntity.Constants.FillColor;
        [DisplayName("文字颜色")]
        public string TextColor { get; set; } = NotEntity.Constants.TextColor;
        [DisplayName("边框宽度")]
        public int BorderWidth { get; set; } = NotEntity.Constants.BorderWidth;
        [DisplayName("字体大小")]
        public int FontSize { get; set; } = 16;
        /// <summary>
        /// 节点位置
        /// </summary>
        [DisplayName("位置"), Description("位置")]
        public Point? Position { get; set; }
        [DisplayName("顺序"), Description("顺序")]
        public int Order { get; set; }
        #endregion
        
        [SugarColumn(IsPrimaryKey = true)]
        public long NodeModelId { get; set; }

        /// <summary>
        /// 1对多关系:
        /// </summary>
        public int WorkFlowId { get; set; }
        /// <summary>
        /// 这个节点所拥有的连接口,后续可以用这个字段优化代码
        /// </summary>
        [DisplayName("节点的连接口"), Description("节点的连接口")]
        public List<PortAlignment> Ports { get; set; } = new();
        /// <summary>
        /// 当前节点所有可能的结果
        /// </summary>
        public List<NodeResult>? Output { get; set; }
        /// <summary>
        /// 进入此节点时候的结果
        /// </summary>  
        public List<NodeResult>? Input { get; set; }
        /// <summary>
        /// 这个节点能够流向的目的节点
        /// </summary>
        public List<string>? TargetsIdList { get; set; }
        /// <summary>
        /// 能够流向到这个节点的源节点
        /// 1.必须这些节点都完成,才能进行下一个节点
        /// 2.这些节点有一个完成就能进行下一个节点
        /// 3.指定的一个或者多个节点完成就能进行下一个节点(条件)
        /// </summary>
        public List<string>? SoursesIdList { get; set; } 
        
    }
}