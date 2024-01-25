using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 表示一个流程
    /// </summary>
    public class WorkFlow
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long WorkFlowId { get; set; }

        /// <summary>
        /// 这个工作流的模板
        /// </summary>
        public long WorkFlowTemplateId { get; set; }
        /// <summary>
        /// 这个工作流的模板
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkFlowTemplateId))]
        public WorkFlowTemplate? WorkFlowTemplate { get; set; }

        /// <summary>
        /// 这个流程所有的节点
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(AppearanceInfo.WorkFlowId))]
        public List<AppearanceInfo>? Nodes { get; set; }
        /// <summary>
        /// 这个流程所有的流向:所有节点之间的关系
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(LinkModel.WorkFlowId))]
        public List<LinkModel>? Links { get; set; } 


        public BlazorDiagramOptionsDto? Options { get; set; }
        public double Zoom { get; set; }
        public PathGenerator DefaultPathGenerator { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 当前位置
        /// </summary>
        public long Current { get; set; }
  
    }
    public enum PathGenerator
    {
        SmoothPathGenerator, StraightPathGenerator
    }
    public class BlazorDiagramOptionsDto
    {
        public bool AllowMultiSelection { get; set; }
        public BlazorDiagramZoomOptionsDto Zoom { get; set; }
        public BlazorDiagramLinkOptionsDto Links { get; set; }


        public BlazorDiagramGroupOptionsDto Groups { get; set; }


        public BlazorDiagramConstraintsOptionsDto Constraints { get; set; }


        public BlazorDiagramVirtualizationOptionsDto Virtualization { get; set; }
    }
}
