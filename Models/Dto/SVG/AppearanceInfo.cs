

using Blazor.Diagrams.Core.Geometry;
using SqlSugar;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 一个节点模型Dto
    /// </summary>
    public class AppearanceInfo
    {
        #region 外观参数
        public int Width { get; set; } = 100;
        public int Height { get; set; }=100;
        public int Corner { get; set; } = 10;
        public string BorderColor { get; set; } = "#98F5FF";
        #endregion
        /// <summary>
        /// 节点Id,系统全局生成,唯一Key
        /// </summary>
        [DisplayName("节点Id"),Description("节点Id")]
        public string? Id { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public long NodeModelId { get; set; }
        /// <summary>
        /// 节点名字
        /// </summary>
        [DisplayName("名字"),Description("节点名字")]
        public string? NodeModelName { get; set; }
        /// <summary>
        /// 节点描述
        /// </summary>
        [DisplayName("描述"), Description("节点")] 
        public string? NodeModelDescription { get; set; }
        /// <summary>
        /// 节点位置
        /// </summary>
        [DisplayName("位置"), Description("位置")]
        public Point? Position { get; set; }
        /// <summary>
        /// 1对多关系:
        /// </summary>
        public int WorkFlowId { get; set; }
        /// <summary>
        /// 节点形状,外观,正方形,菱形
        /// </summary>
        [DisplayName("节点形状"), Description("节点形状")]
        public NodeType? NodeType { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 节点的连接口
        /// </summary>
        [DisplayName("节点的连接口"), Description("节点的连接口")]
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
}