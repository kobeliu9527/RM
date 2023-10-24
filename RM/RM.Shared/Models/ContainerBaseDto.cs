using System;

namespace RM.Shared.Models
{
    /// <summary>
    /// 容器Dto基类,有Id属性:<see cref="Guid"/>和Type属性:<see cref="ContainerType"/>
    /// </summary>
    public class ContainerBaseDto
    {

        public ContainerBaseDto()
        {
            Id = Guid.NewGuid();
            Type = ContainerType.Base;
        }
        public ContainerBaseDto(Guid id)
        {
            Id = id;
            Type = ContainerType.Base;
        }
        public ContainerBaseDto(Guid id, ContainerType type) : this(id)
        {
            Type = type;
        }
        public ContainerBaseDto(ContainerType type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }

        public Guid Id { get; private set; }
        public ContainerType Type { get; private set; }
    }
}
