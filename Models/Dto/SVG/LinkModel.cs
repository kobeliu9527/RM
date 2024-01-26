using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using BootstrapBlazor.Components;
using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 表示一条线
    /// </summary>
    public class LinkModel
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long LinkModelId { get; set; }
        /// <summary>
        /// 1对多
        /// </summary>
        public int WorkFlowId { get; set; }
        public string? Id { get; set; }
        public string? SourceId { get;   set; }
        public PortAlignment SourceAlignment { get; set; }
        public PortAlignment TargetAlignment { get; set; }
        public string? Source { get;   set; }
        public string? TargetId { get;   set; }
        public string? Target { get;   set; }
        public List<LinkLabel> Labels { get; set; } = new();
    }
    public class LinkLabel
    {
        public string? Content { get; set; }

        public double? Distance { get; set; }

        public Point? Offset { get; set; }
    }
}