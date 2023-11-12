using BootstrapBlazor.Components;
using RM.Shared.Core;
using RM.Shared.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RM.Shared.Models
{
    /// <summary>
    /// 表示一个控件,有类型,lable,Parent,ChildContainers,还有占用多少列
    /// </summary>
    public class ComponentDto
    {

        #region Public Constructors

        /// <summary>
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
        /// 唯一值
        /// </summary>
        [DisplayName("页面唯一标识"), Required(ErrorMessage = "控件的名字,必须全局唯一")]
        public string Id { get; set; }
        /// <summary>
        /// 父级容器的ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 外观颜色
        /// </summary>
        [DisplayName("外观颜色")]
        public Color Color { get; set; }

        /// <summary>
        /// 控件的类型,用于区分是文本框还是下拉框等等--
        /// </summary>

        public ComponentType ComponentType { get; set; }

        ///  <summary>
        /// 前置标签显示文本(应该支持多语言显示)
        /// </summary>
        /// public int DisplayText { get; set; }

        /// <summary>
        /// 多语言
        /// </summary>
        public MutLanguage MutLanguage { get; set; } = new();

        /// <summary>
        /// 是否显示前置标签
        /// </summary>
        [DisplayName("是否显示标题")] public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// 是否禁用
        /// </summary>
        [DisplayName("是否禁用")] public bool IsDisabled { get; set; }

        ///// <summary>
        ///// 是否自动获取焦点
        ///// </summary>
        //[DisplayName("是否自动获取焦点")] public bool IsAutoFocus { get; set; }

        ///// <summary>
        ///// 获得焦点后自动选择输入框内所有字符串
        ///// </summary>
        //[DisplayName("是否默认选中所有文本")] public bool IsSelectAllTextOnFocus { get; set; } = true;

        /// <summary>
        /// 控件的属性
        /// </summary>
        public Dictionary<string, Property> Props { get; set; } = new();
        /// <summary>
        /// 回车后是否执行存储过程
        /// </summary>
        [DisplayName("回车后是否执行存储过程")] public bool IsExecuteForEnter { get; set; }
        /// <summary>
        /// 要执行的存储过程名
        /// </summary>
        [DisplayName("要执行的存储过程名")] public string StoreName { get; set; } = "";
        /// <summary>
        /// 执行存储过程需要的参数
        /// </summary>
        [DisplayName("执行存储过程需要的参数")] public List<string> Parameters { get; set; } = new();
       
        /// <summary>
        /// 控件的值信息,不同的控件,值类型不一样
        /// </summary>
        public ValueInfo ValueInfo { get; set; } = new ValueInfo();

        public ComponentDto? Parent { get; set; }

        /// <summary>
        /// 前置标签显示文本(应该支持多语言显示)
        /// </summary>
        public string? DisplayText { get; set; }

        /// <summary>
        /// 如果这个组件是容器组件,那么他还有子组件
        /// </summary>
        public List<ContainerDto>? ChildContainers { get; set; }
        /// <summary>
        /// shifou
        /// </summary>
        public bool IsAbsPosition { get; set; }
        public int Zindex { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        /// <summary>
        /// 宽,参考bootstarp中设计,1-12
        /// </summary>
        /// <remarks>
        ///<para><see cref="ComponentUtils.GetComponentColumnCssClasses"/>方法,根据组件的with计算大小</para>
        /// </remarks>
        [DisplayName("宽度:参考bootstarp中设计,值范围1-12")]
        public int Width
        {
            get { return width; }
            set
            {
              
                    width = 1;
            
                    width = value;
               
            }
        }

        /// <summary>
        /// 小于等于0,会被转换为auto
        /// </summary>
        /// <remarks>
        ///<para>如果大于0,会转换为带px的像素,否则auto</para>
        /// </remarks>
        [DisplayName("高度:0表示系统自动,否则就是对应像素")]
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
            ComponentType = type;
            //DisplayText = $"{Type.GetName()}-{Guid.NewGuid()}";
            MutLanguage.zh_CN = $"{ComponentType.GetName()}";
            if (ComponentType == ComponentType.Tabs)
            {
                ChildContainers = new List<ContainerDto>()
                {
                    new ContainerDto(ContainerType.Tab)
                    {
                MutLanguage = new MutLanguage() { zh_CN = "Tab 1" }

                    }
                };
            }
        }

        #endregion Internal 方法

    }

    /// <summary>
    /// </summary>
    public class ValueInfo
    {

        #region 属性

        /// <summary>
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// </summary>
        public string StringValue { get; set; } = "";

        /// <summary>
        /// </summary>
        public bool BoolValue { get; set; }

        /// <summary>
        /// </summary>
        public double DoubleValue { get; set; }
        public DataTable? DataTable { get; set; }

        #endregion 属性

    }
}
