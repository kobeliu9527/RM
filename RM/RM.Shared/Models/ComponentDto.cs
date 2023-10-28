using RM.Shared.Core;
using RM.Shared.Extensions;

namespace RM.Shared.Models
{
    /// <summary>
    /// 表示一个控件,有类型,lable,Parent,ChildContainers,还有占用多少列
    /// </summary>
    public class ComponentDto : ComponentBaseDto
    {
        /// <summary>
        /// </summary>
        public ComponentDto()
        {
            // Type = ComponentType.SingleLine;
            //Label = $"{Type.GetName()}-{Guid.NewGuid()}";
        }

        /// <summary>
        /// 根据控件类型创建控件 <see cref="PaletteWidgetDtoExtensions.CreateComponent(PaletteWidgetDto)"/>
        /// </summary>
        /// <param name="type"></param>
        public ComponentDto(ComponentType type)
        {
            InitializeComponent(type);
        }

        //public ComponentDto(Guid id, ComponentType type) : base(id)
        //{
        //    InitializeComponent(type);
        //}

        internal void InitializeComponent(ComponentType type)
        {
            Type = type;
            Label = $"{Type.GetName()}-{Guid.NewGuid()}";

            if (Type == ComponentType.Tabs)
            {
                ChildContainers = new List<ContainerDto>()
                {
                    new ContainerDto(ContainerType.Tab)
                    {
                        Label = "Tab 1"
                    }
                };
            }
        }

        /// <summary>
        /// 控件的属性
        /// </summary>
        public Dictionary<string, Property> Props { get; set; } = new();
        /// <summary>
        /// 控件的值信息,不同的控件,值类型不一样
        /// </summary>
        public ValueInfo  ValueInfo { get; set; }=new ValueInfo();
        public ComponentDto? Parent { get; set; }
        public ComponentType Type { get; set; }

        public string? Label { get; set; }

        //// component has row and column info if it is a widget
        //public int? Row { get; set; }
        //public int? Column { get; set; }

        //// component has order and title info if it is a tab widget
        //public int? Order { get; set; }
        //public string TabTitle { get; set; }

        public List<ContainerDto>? ChildContainers { get; set; }

        private int width = ComponentUtils.MaxColumnWidth;
        private int height=0;

        public int Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                {
                    width = 1;
                }
                else if (value > 12)
                {
                    width = 12;
                }
                else
                {
                    width = value;
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                height = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValueInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int IntValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StringValue { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public bool BoolValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double DoubleValue { get; set; }
    }
}
