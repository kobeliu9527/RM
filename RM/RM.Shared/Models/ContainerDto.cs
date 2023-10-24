using System;
using System.Collections.Generic;

namespace RM.Shared.Models
{
    /// <summary>
    /// 容器:有属性 <see cref="Rows"/> is <see cref="List { ComponentDto }"/>
    /// </summary>
    /// <remarks>一个容器有多行<see cref="Rows"/>,一行有多个控件<see cref="List{ComponentDto}"/></remarks>
    public class ContainerDto : ContainerBaseDto
    {
        public ContainerDto()
        {
            InitEmptyContainer(ref Rows);
        }

        public ContainerDto(Guid id) : base(id)
        {
            InitEmptyContainer(ref Rows);
        }

        public ContainerDto(Guid id, List<List<ComponentDto>> rows) : base(id)
        {
            Rows = rows;
        }

        public ContainerDto(Guid id, ContainerType type) : base(id, type)
        {
            InitEmptyContainer(ref Rows);
        }

        public ContainerDto(Guid id, ContainerType type, List<List<ComponentDto>> rows)
            : base(id, type)
        {
            Rows = rows;
        }

        public ContainerDto(ContainerType type) : base(type)
        {
            InitEmptyContainer(ref Rows);
        }

        public ContainerDto(List<List<ComponentDto>> rows)
        {
            Rows = rows;
        }

        internal void InitEmptyContainer(ref List<List<ComponentDto>> rows)
        {
            rows = new List<List<ComponentDto>>()
            {
                new List<ComponentDto>()
            };
        }

        public string Label { get; set; }
        /// <summary>
        /// 容器中,所有行的集合
        /// </summary>
        public readonly List<List<ComponentDto>> Rows;
    }
}
