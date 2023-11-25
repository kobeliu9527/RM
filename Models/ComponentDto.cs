using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 一行
    /// </summary>
    public class ComponentDto
    {
        /// <summary>
        /// 控件的名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public ComponentType ComponentType { get; set; }
        /// <summary>
        /// flex布局时候的属性
        /// </summary>
        public FlexInfo FlexInfo { get; set; } = new();
        /// <summary>
        /// 定位方式
        /// </summary>
        public PositionType Position { get; set; } = PositionType.Relative;
        /// <summary>
        /// 高度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 如果是父控件
        /// </summary>
        public List<ComponentDto> Child { get; set; } = new();
        //public EventCallback<MouseEventArgs> MainStateHasChanged { get; set; }
        public Action? MainStateHasChanged { get; set; }
    }
}
