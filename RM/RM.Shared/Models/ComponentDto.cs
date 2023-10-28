using RM.Shared.Core;
using RM.Shared.Extensions;

namespace RM.Shared.Models
{
    /// <summary>
    /// 表示一个控件,有类型,lable,Parent,ChildContainers,还有占用多少列
    /// </summary>
    public class ComponentDto
    {
        #region Public Constructors
        /// <summary>
        /// 
        /// </summary>
        public ComponentDto()
        {

        }

        /// <summary>
        /// 根据控件类型创建控件 <see cref="PaletteWidgetDtoExtensions.CreateComponent(PaletteWidgetDto)"/>
        /// </summary>
        /// <param name="type"></param>
        public ComponentDto(ComponentType type)
        {
            InitializeComponent(type);
        }

        #endregion Public Constructors

        //public ComponentDto(Guid id, ComponentType type) : base(id)
        //{
        //    InitializeComponent(type);
        //}

        #region 字段

        private int width = ComponentUtils.MaxColumnWidth;

        private int height = 0;

        #endregion 字段

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get;  set; }

        /// <summary>
        /// 页面唯一名字
        /// </summary>
        public string Name { get;  set; } = "";

        /// <summary>
        /// 控件的类型,用于区分是文本框还是下拉框等等
        /// </summary>
        public ComponentType Type { get; set; }

        ///// <summary>
        ///// 前置标签显示文本(应该支持多语言显示)
        ///// </summary>
        //public int DisplayText { get; set; }

        /// <summary>
        /// 多语言
        /// </summary>
        public MutLanguage MutLanguage { get; set; } = new();

        /// <summary>
        /// 是否显示前置标签
        /// </summary>
        public bool ShowLabel { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 是否自动获取焦点
        /// </summary>
        public bool IsAutoFocus { get; set; }

        /// <summary>
        /// 获得焦点后自动选择输入框内所有字符串
        /// </summary>
        public bool IsSelectAllTextOnFocus { get; set; }

        /// <summary>
        /// 控件的属性
        /// </summary>
        public Dictionary<string, Property> Props { get; set; } = new();

        /// <summary>
        /// 控件的值信息,不同的控件,值类型不一样
        /// </summary>
        public ValueInfo ValueInfo { get; set; } = new ValueInfo();

        public ComponentDto? Parent { get; set; }
        /// <summary>
        /// 前置标签显示文本(应该支持多语言显示)
        /// </summary>
        public string? DisplayText { get; set; }

        public List<ContainerDto>? ChildContainers { get; set; }
        /// <summary>
        /// 宽,参考bootstarp中设计,1-12
        /// </summary>
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
        /// <summary>
        /// 小于等于0,会被转换为auto
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
            }
        }

        #endregion 属性

        #region Internal 方法

        internal void InitializeComponent(ComponentType type)
        {
            Type = type;
            DisplayText = $"{Type.GetName()}-{Guid.NewGuid()}";

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

        #endregion Internal 方法
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValueInfo
    {
        #region 属性

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

        #endregion 属性
    }
}
