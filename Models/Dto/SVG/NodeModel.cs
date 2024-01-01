

using Blazor.Diagrams.Core.Geometry;
using SqlSugar;
using System.Diagnostics.CodeAnalysis;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 1v1节点,这个节点只有一个父级节点
        /// </summary>
        Single,
        /// <summary>
        /// 有多个父级节点,父级有一个完成就能进入
        /// </summary>
        AnyOne,
        /// <summary>
        /// 有多个父级节点,父级全部完成才能进入
        /// </summary>
        All,
        /// <summary>
        /// 指定的的节点完成
        /// </summary>
        Appoint,
        /// <summary>
        /// 其他情况
        /// </summary>
        Other
    }
    /// <summary>
    /// 一个节点模型
    /// </summary>
    public class NodeModel
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long NodeModelId { get; set; }
        /// <summary>
        /// 节点名字
        /// </summary>
        public string? NodeModelName { get; set; }
        /// <summary>
        /// 节点描述
        /// </summary>
        public string? NodeModelDescription { get; set; }
        /// <summary>
        /// 1对多关系:
        /// </summary>
        public int WorkFlowId { get; set; }

        public NodeType NodeType { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// 节点位置
        /// </summary>
        public Point? Position { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string? Title { get; set; }
        public List<PortPosition>? Ports { get; set; } 
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
    /// <summary>
    /// 结果信息:指示为x时候应该流向那些节点
    /// 
    /// </summary>
    /// <remarks>
    /// <para>
    /// 
    /// </para>
    /// </remarks>
    public class NodeResult
    {
        /// <summary>
        /// 节点结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        public List<string>? Ids { get; set; }
       
    }
    public enum PortPosition
    {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }
}