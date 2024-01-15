using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    /// <summary>
    /// 表示一个控件
    /// </summary>
    public class Control
    {
        /// <summary>
        /// 控件的名字
        /// </summary>
        [DisplayName("Key"), Description("用于数据绑定,筛选等唯一key")]
        public string Key { get; set; } = "NA";
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
        [DisplayName("控件高度"), Description("逻辑:占用父级容器高度的百分比 0-100")]
        public int Height { get; set; } = 100;
        /// <summary>
        /// 控件宽度
        /// </summary>
        [DisplayName("控件宽度"), Description("逻辑:1-12之间的值,父级容器被分成12分,这个值表示占用多少.一般放在Row控件下的子控件设置此属性")]
        public int Width { get; set; } = 4;
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
            Key = CtrType.ToString();
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
            Key = type.ToString();
            switch (type)
            {
                case WidgetType.SplitH:
                case WidgetType.SplitV:
                    DisplayName = "分割面板";
                    IsContainer = true;
                    var first = new Control(WidgetType.FirstPanel) { Key = "FirstPanel", DisplayName = "第一个容器" };
                    first.Zindex = Zindex;
                    var second = new Control(WidgetType.SecondPanel) { Key = "SecondPanel", DisplayName = "第二个容器" };
                    second.Zindex = Zindex;
                    Controls.Add(first);
                    Controls.Add(second);
                    Key = "Split";
                    break;
                case WidgetType.Row:
                    IsContainer = true;
                    DisplayName = "行元素";
                    Height = 10;
                    break;
                case WidgetType.Bottom:
                    DisplayName = "按钮";
                    break;
                case WidgetType.InputText:
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
                        Key = "Tab1",
                        DisplayName = "页面1"
                    };
                    var tab2 = new Control() { Key = "Tab2", DisplayName = "页面2" };
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
}
