using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Models;
using Models.Dto.SVG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Components.Svg
{
    /// <summary>
    /// 表示一个工作流节点
    /// </summary>
    public class NodeModelFW : SvgNodeModel
    {
        /// <summary>
        /// 数据  
        /// </summary>
        public Models.Dto.SVG.NodeModel? Data { get; set; } = new Models.Dto.SVG.NodeModel();
        /// <summary>
        /// 1.必须经过的节点
        /// </summary>
        public List<NodeResult> MustThroughNodes { get; set; }
        /// <summary>
        /// 可以不经过,但是经过后一定要是ok的
        /// </summary>
        public List<NodeResult> MustOkNodes { get; set; }

        public NodeModelFW(Point? position = null) : base(position) 
        {
            Data.Id = Id;
        }
        public NodeModelFW(string id, Point? position = null) :base(id, position)
        {
            Data.Id = Id;
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
