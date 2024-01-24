using BootstrapBlazor.Components;
using SqlSugar;
using System.Diagnostics.CodeAnalysis;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 一个产品,都有自己的流程图
    /// </summary>
    public class Production
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ProductionId { get; set; }
        /// <summary>
        ///  工作流模板Id            
        /// </summary>
        public int WorkFlowTemplateId { get; set; }
        /// <summary>
        /// 1对1:一个产品对应一个工作流实例
        /// </summary>
        public int WorkFlowId { get; set; }
        /// <summary>
        /// 表示对应的工作流实例?
        /// </summary>
        [NotNull]
        [Navigate(NavigateType.OneToOne, nameof(WorkFlowId))]
        public WorkFlow? WorkFlow { get; set; }
        /// <summary>
        /// 产品SN
        /// </summary>
        public string? SN { get; set; }
        /// <summary>
        /// 这个产品所有经过的流程路径,可能重复 todo:抽象为类
        /// </summary>
        [NotNull]
        public List<string>? HistoryNodeId { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        public string? CurrentNodeId { get; set; }
        /// <summary>
        /// 传到下一个节点
        /// </summary>
        public bool Next()
        {
            //找到当前节点
            var nodes= WorkFlow.Nodes.FindAll(x=>x.Id==CurrentNodeId)!;

            foreach (var node in nodes)
            {
                switch (node.NodeType)
                {
                    case NodeType.Square:
                        break;
                    case NodeType.Diamond:
                        break;
                    case NodeType.Ellipse:
                        break;
                    case NodeType.Appoint:
                        break;
                    case NodeType.Other:
                        break;
                    default:
                        break;
                }
                HistoryNodeId.Add(node!.Id!);   
            }
            //
            throw new NotImplementedException();
        }
        /// <summary>
        /// 传到指定的下一个节点
        /// </summary>
        public bool Next(string id)
        {
            //update数据库中CurrentNodeId的字段
            //确保这个id在对应工作流中存在
            CurrentNodeId = id;

            throw new NotImplementedException();
        }
        /// <summary>
        /// 回传回去
        /// </summary>
        public bool Back()
        {
            throw new NotImplementedException();
        }
    }

    


    /// <summary>
    /// 多对多关系表
    /// </summary>
    public class ProductAndPart
    {
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int ProductId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int PartId { get; set; }
    }
}
