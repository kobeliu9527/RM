using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Models
{
    /// <summary>
    /// 表示一个组件 按钮/文本框/选中框等
    /// </summary>
    public class PaletteWidgetDto
    {
        /// <summary>
        /// 组件的名字,必须全局唯一
        /// </summary>
        [DisplayName("唯一标识"), Required(ErrorMessage = "组件的Id,必须全局唯一"), ReadOnly(true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("名字"), Required(ErrorMessage = "组件的名字,最好全局唯一")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 控件的类型,用于区分是文本框还是下拉框等等
        /// </summary>
        public ComponentType ComponentType { get; set; }
        /// <summary>
        /// 控件属于什么种类:普通控件/容器控件/图表控件/通讯控件
        /// </summary>
        public GroupType GroupType { get; set; }
        public string Description { get; set; } = "组件描述";
        public string Icon { get; set; } = "组件图标";
        public bool Visible { get; set; }

        /// <summary>
        /// 这个控件所拥有的所有附加属性
        /// </summary>
        public Dictionary<string, Property> Props { get; set; } = new();
    }
    public enum GroupType
    {
        普通控件,
        容器控件,
        图标控件,
        通讯控件
    }
    public class MutilProperty
    {
        public string ContainerName { get; set; }
        public List<string> StringListValue { get; set; }
    }
    /// <summary>
    /// 属性信息
    /// </summary>
    public class Property
    {
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// 属性类型,用于控制在属性面板中的展示方式
        /// </summary>
        public PType PType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool BoolVal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IntlVal { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string StringValue { get; set; } = "";
        /// <summary>
        /// 属性值
        /// </summary>
        public IEnumerable<string> StringListValue { get; set; } 
        /// <summary>
        /// 多个参数值
        /// </summary>
        public List<MutilProperty> MutilProperties { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public string DataSourse { get; set; } = "";



    }

    /// <summary>
    /// 属性类型
    /// </summary>
    public enum PType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,

        /// <summary>
        /// 颜色
        /// </summary>
        SingleColor,

        /// <summary>
        /// 布尔量
        /// </summary>
        Bool,

        /// <summary>
        /// 整数
        /// </summary>
        Int,

        /// <summary>
        /// 小数
        /// </summary>
        Double,
        /// <summary>
        /// 数据源
        /// </summary>
        DataSourse,
        /// <summary>
        /// string集合
        /// </summary>
        StringList,
        /// <summary>
        /// 下拉选择框
        /// </summary>
        Select,
        /// <summary>
        /// 多参数
        /// </summary>
        MutilPropter
    }
}

//let singleLine = {
//  "name": localization.localize('wpsSingleLineName'),
//  "templateUrl": "single-line-property",
//  "description": localization.localize('wpsSingleLineDesc'),
//  "propertyDataType": entityModel.PropertyDataType.SingleLine,
//  "propertyType": entityModel.WidgetType.SINGLELINE,
//  "buttonWidgetType": -1,
//  "containerType": vm.defaultContainerType,
//  "order": 1,
//  "icon": "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 50 20'><g><path d='M 1,19 1,1 49,1 49,19 Z M 0,0 0,20 50,20 50,0 Z' style='fill:#000000'/></g><path style='fill:#000000;fill-opacity:1;stroke:none;stroke-width:0.5;stroke-miterlimit:4;stroke-dasharray:none;stroke-opacity:1' d='m 5.4382875,15.181544 3.838384,0 0,0.707071 -3.838384,0 z m -0.112826,-11.259453 3.838384,0 0,0.707071 -3.838384,0 z m 2.429338,11.975342 -1.062012,0 0,-11.889647 1.062012,0 z'/></svg>",
//  "icon_mini": "<svg width='50' height='50' xmlns='http://www.w3.org/2000/svg'><g><title>background</title><rect fill='none' id='canvas_background' height='402' width='582' y='-1' x='-1'/></g><g><title>Layer 1</title><rect stroke='#444444' id='svg_2' height='26.440659' width='49' y='12.918262' x='0.520249' stroke-width='3' fill='none'/><path fill='#444444' stroke-width='1.5' stroke-miterlimit='4' id='path8' d='m7.209435,33.24191l3.83838,0l0,0.9917l-3.83838,0l0,-0.9917zm-0.11283,-15.79186l3.83839,0l0,0.9917l-3.83839,0l0,-0.9917zm2.42934,16.79593l-1.06201,0l0,-16.67574l1.06201,0l0,16.67574z'/><line stroke='#444444' stroke-linecap='null' stroke-linejoin='null' id='svg_8' y2='18.484717' x2='12.159199' y1='18.484717' x1='5.707173' stroke-width='2' fill='none'/><line stroke='#444444' stroke-linecap='null' stroke-linejoin='null' id='svg_10' y2='33.286426' x2='12.28571' y1='33.286426' x1='5.833683' stroke-width='2' fill='none'/><line stroke-linecap='null' stroke-linejoin='null' id='svg_11' y2='34.045488' x2='9.122952' y1='18.864248' x1='8.996441' stroke-width='2' stroke='#444444' fill='none'/></g></svg>",
//  "template": "<div class=\"form-group\">\r\n        <label ng-if=\"!properties.labelHidden\"\r\n      style=\"text-align: right;\"         ng-class=\"{ \'control-label--required\': properties.required }\"\r\n               class=\"control-label col-xs-{{ !properties.labelHidden ? properties.labelWidth : 12 }}\">\r\n            {{ properties.label }}\r\n        <\/label>\r\n        <div class=\"col-xs-{{ 12 - (!properties.labelHidden ? properties.labelWidth : 0) }}\">\r\n\t\t\t\t <div dx-text-box=\"properties.dxOptions\"></div>        <\/div>\r\n    <\/div>\r\n<\/div>",
//  "visible": true
//};
