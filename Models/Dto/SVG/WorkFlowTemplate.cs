using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 模板:表示一个流程(工作流)的定义
    /// </summary>
    public class WorkFlowTemplate: EntityBase
    {
        /// <summary>
        /// 当一个工作流有多个版本的时候,用这个字段表示同一类工作流
        /// </summary>
        public long WorkFlowTemplateRootId { get; set; }
        /// <summary>
        /// 工作流名字
        /// </summary>
        public string? WorkFlowTemplateName { get; set; }
        /// <summary>
        /// 工作流描述
        /// </summary>
        public string? WorkFlowTemplateDescription { get; set; }
        /// <summary>
        /// 工作流版本
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// 这个流程所有的节点
        /// </summary>
        //[Navigate(NavigateType.OneToMany, nameof(NodeModel.WorkFlowId))]
        [SugarColumn(IsJson =true, ColumnDataType = "nvarchar(max)")]
        public List<NodeDto>? Nodes { get; set; }
        /// <summary>
        /// 这个流程所有的流向:所有节点之间的关系
        /// </summary>
        [SugarColumn(IsJson =true, ColumnDataType = "nvarchar(max)")]
        //[Navigate(NavigateType.OneToMany, nameof(LinkModel.WorkFlowId))]
        public List<LinkDto>? Links { get; set; }

        /// <summary>
        /// 判断一个条码能否流到下一个节点
        /// </summary>
        /// <param name="NodeModel"></param>
        public void IsNext(long NodeModel)
        { 
        
        }
    }
}
