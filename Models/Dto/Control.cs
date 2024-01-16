using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Dto
{
    /// <summary>
    /// 表示一个控件
    /// </summary>
    public class Control
    {
        #region 设计时属性
        [JsonIgnore]
        public bool IsSelect { get; set; }
        #endregion
        #region 按钮属性
        public Button? Button { get; set; }
        #endregion
        #region 输入框属性
        public InputText?  InputText { get; set; }
        #endregion
        /// <summary>
        /// 控件唯一Id
        /// </summary>
        [DisplayName("Id"), Description("用于数据绑定,筛选等唯一key")]
        public long Id { get; set; } 
        /// <summary>
        /// 控件唯一Id
        /// </summary>
        [DisplayName("Key"), Description("用于数据绑定,筛选等唯一key")]
        public string Key { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 控件的名字
        /// </summary>
        [DisplayName("名字"), Description("用于前台显示的名字")]
        public string DisplayName { get; set; } = "NA";
        /// <summary>
        /// 控件的类型
        /// </summary>
        public WidgetType CtrType { get; set; } = WidgetType.Bottom;
        /// <summary>
        /// 是否为容器控件,用于优化递归,只有容器空间才会递归其子控件
        /// </summary>
        public bool IsContainer { get; set; }
        /// <summary>
        /// Tab控件,表示当前页是否被激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Split控件,表示左右分割还是上下分割
        /// </summary>
        public bool IsVertical { get; set; }
        /// <summary>
        /// 控制层叠顺序
        /// </summary>
        [DisplayName("层叠顺序"), Description("系统有算法,根据先放的在下面,后方的在上面,若非必要,不要更改")]
        public int Zindex { get; set; }
        /// <summary>
        /// 控件高度
        /// </summary>
        [DisplayName("控件高度"), Description("逻辑:占用父级容器高度的百分比 0-100,自适应控件最小高度")]
        public int Height { get; set; } = 100;
        /// <summary>
        /// 最小高度
        /// </summary>
        [DisplayName("最小高度"), Description("")]
        public int MinHeight { get; set; } = 30;
        /// <summary>
        /// 最大高度
        /// </summary>
        [DisplayName("最大高度"), Description("")]
        public int MaxHeight { get; set; } = 100;
        /// <summary>
        /// 控件宽度
        /// </summary>
        [DisplayName("控件宽度"), Description("逻辑:1-12之间的值,父级容器被分成12分,这个值表示占用多少.一般放在Row控件下的子控件设置此属性")]
        public int Width { get; set; } = 4;
        /// <summary>
        /// 控件宽度
        /// </summary>
        [DisplayName("起始偏移"), Description("相对于前一个控件,间隔多少个单位")]
        public int Offset { get; set; } = 0;
        /// <summary>
        /// 当为Split的时候,表示第一个panel占比
        /// </summary>
        [DisplayName("FirstPanel占比"), Description("当为Split的时候,表示第一个panel占比,默认40")]
        public int Basis { get; set; } = 40;

        /// <summary>
        /// 子控件
        /// </summary>
        [DisplayName("子控件"), Description("容器控件的子控件")]
        public List<Control> Controls { get; set; } = new List<Control>();
        /// <summary>
        /// 数据源
        /// </summary>
        [DisplayName("数据源"), Description("获取数据,或者执行命令相关")]
        public DataSourse DataSourse { get; set; } = new DataSourse();
        /// <summary>
        /// 结果
        /// </summary>
        [DisplayName("结果"), Description("不同类型的结果")]
        public Values Values { get; set; } = new Values();
        /// <summary>
        /// 执行存储过程之后,返回给表格要赋值给哪一个表格
        /// </summary>
        [DisplayName("返回结果赋值给"), Description("执行存储过程之后,返回给表格要赋值给哪一个表格")]
        public string ReturnTable { get; set; } = "";
        /// <summary>
        /// 控件为表格控件的时候,需要的参数
        /// </summary>
        [DisplayName("表格控件信息"), Description("控件为表格控件的时候,需要的参数")]
        public TableInfo? TableInfo { get; set; }


        public Control()
        {
        }
        /// <summary>
        /// todo:新增了控件 拖动的时候会触发调用此构造函数
        /// </summary>
        /// <param name="type">元素类型</param>
        /// <param name="zindex">父元素的层级+1</param>
        public Control(WidgetType type, int zindex = 0)
        {
            CtrType = type;
            Zindex = zindex;
            switch (type)
            {
                case WidgetType.SplitH:
                case WidgetType.SplitV:
                    DisplayName = "分割面板";
                    IsContainer = true;
                    var first = new Control(WidgetType.FirstPanel) {  DisplayName = "第一个容器" };
                    first.Zindex = Zindex;
                    var second = new Control(WidgetType.SecondPanel) { DisplayName = "第二个容器" };
                    second.Zindex = Zindex;
                    Controls.Add(first);
                    Controls.Add(second);
                    break;
                case WidgetType.Row:
                    IsContainer = true;
                    DisplayName = "行元素";
                    Height = 10;
                    break;
                case WidgetType.Bottom:
                    Button = new Button();
                    DisplayName = "按钮";
                    break;
                case WidgetType.InputText:
                    InputText = new InputText();
                    DisplayName = "文本框";
                    break;
                case WidgetType.None:
                    break;
                case WidgetType.FirstPanel:
                    IsContainer = true;
                    break;
                case WidgetType.SecondPanel:
                    IsContainer = true;
                    break;
                case WidgetType.Table:
                    TableInfo = new TableInfo();
                    DisplayName = "表格";
                    break;
                case WidgetType.Tab:
                    DisplayName = "多页面组件";
                    var tab1 = new Control()
                    {
                        DisplayName = "页面1",
                        CtrType = WidgetType.TabItem,
                    };
                    var tab2 = new Control() { Key = "Tab2", DisplayName = "页面2", CtrType = WidgetType.TabItem };
                    tab1.Zindex = Zindex;
                    tab2.Zindex = Zindex;
                    Controls.Add(tab1);
                    Controls.Add(tab2);
                    break;
                default:
                    break;
            }
        }

    }
    public class Button
    {
        [DisplayName("大小"),Description("按钮等的大小")]
        public Size Size { get; set; } = Size.None;
        [DisplayName("按钮风格"), Description("按钮风格")]
        public ButtonStyle ButtonStyle { get; set; }
        [DisplayName("按钮图标"), Description("按钮图标")]
        public string Icon { get; set; } = "";
        [DisplayName("禁用"), Description("是否禁用此图标")]
        public bool IsDisabled { get; set; }
    }
    public class InputText
    {//IsTrim
        [DisplayName("自动去头尾空格"), Description("点击确认后,自动去头尾空格")]
        public bool IsTrim { get; set; }
        [DisplayName("边框颜色"), Description("边框颜色")]
        public Color Color { get; set; }
        [DisplayName("提示水印"), Description("文本框的提示信息")]
        public string PlaceHolder { get; set; } = "请输入";
        [DisplayName("禁用"), Description("是否禁用此图标")]
        public bool IsDisabled { get; set; }
        [DisplayName("自动获取焦点"), Description("是否自动获取焦点")]
        public bool IsAutoFocus { get; set; }
        [DisplayName("显示标签"), Description("是否显示标签")]
        public bool ShowLabel { get; set; }=true;
        [DisplayName("回车执行"), Description("文本框是否执行回车事件")]
        public bool EnterEnAble { get; set; }
        [DisplayName("回车执行的存储过程"), Description("按回车后执行的存储过程")]
        public string EnterStoreName { get; set; } = "";
        [DisplayName("回车执行的存储过程的参数"), Description("按回车后执行的存储过程需要的参数")]
        public List<string> EnterStoreParmeter { get; set; } =new List<string>();
        [DisplayName("返回执行"), Description("文本框是否执行Esc事件")]
        public bool EscEnAble { get; set; }
        [DisplayName("返回键执行的存储过程"), Description("按返回键后执行的存储过程")]
        public string EscStoreName { get; set; } = "";
        [DisplayName("返回键执行的存储过程的参数"), Description("按返回键后执行的存储过程需要的参数")]
        public IEnumerable<string>? EscStoreParmeter { get; set; }
    }
}
