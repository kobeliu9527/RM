using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Models;
using Models.Dto.SVG;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Components.Svg
{
    /// <summary>
    /// 自定义的节点
    /// </summary>
    public class CustomNode : SvgNodeModel
    {
        /// <summary>
        /// 这个节点的信息:外观等等...
        /// </summary>
        public NodeDto NodeData { get; set; }=new NodeDto();
        /// <summary>
        /// 1.必须经过的节点
        /// </summary>
        public List<NodeResult> MustThroughNodes { get; set; } = new();
        /// <summary>
        /// 可以不经过,但是经过后一定要是ok的
        /// </summary>
        public List<NodeResult> MustOkNodes { get; set; } = new();
        /// <summary>
        /// 拖动生成
        /// </summary>
        /// <param name="position"></param>
        public CustomNode(Point? position = null) : base(position) 
        {
            NodeData.Id = base.Id;
        }
        public CustomNode(string id, Point? position = null) :base(id, position)
        {
            NodeData.Id = base.Id;
        }
        /// <summary>
        /// 传到下一个节点
        /// </summary>
        public bool Next()
        {
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
}
