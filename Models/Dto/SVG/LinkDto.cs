using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Routers;
using BootstrapBlazor.Components;
using SqlSugar;
using System.ComponentModel;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 表示一条线
    /// </summary>
    public class LinkDto
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long LinkModelId { get; set; }
        /// <summary>
        /// 支持手动推拽link
        /// </summary>
        [DisplayName("支持手动拖动")]
        public bool Segmentable { get; set; }
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
        public bool PathGenerator { get; set; }
        public bool Router { get; set; }
        //public LinkMarker? TargetMarker { get; set; }
        //public LinkMarker? SourceMarker { get; set; }
        public string? Color { get; set; }
        public string? SelectedColor { get; set; }
        [DisplayName("线条宽度")]
        public double Width { get; set; } = 1;
    }
    public class LinkLabel
    {
        public string? Content { get; set; }

        public double? Distance { get; set; }

        public Point? Offset { get; set; }
    }
}