using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{

    public class Control
    {
        /// <summary>
        /// 控件的名字
        /// </summary>
        [DisplayName("名字"), Description("用于数据绑定,筛选等唯一key")]
        public string Name { get; set; } = "NA";
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
            this.Name = CtrType.ToString();
        }
        /// <summary>
        /// todo:新增了控件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="zindex"></param>
        public Control(WidgetType type, int zindex = 0)
        {
            CtrType = type;
            Zindex = zindex;
            Name = type.ToString();
            switch (type)
            {
                case WidgetType.SplitH:
                case WidgetType.SplitV:
                    IsContainer = true;
                    var first = new Control(WidgetType.FirstPanel) { Name = "FirstPanel" };
                    first.Zindex = Zindex;
                    var second = new Control(WidgetType.SecondPanel) { Name = "SecondPanel" };
                    second.Zindex = Zindex;
                    Controls.Add(first);
                    Controls.Add(second);
                    Name = "Split";
                    break;
                case WidgetType.Row:
                    IsContainer = true;
                    break;
                case WidgetType.Bottom:
                    break;
                case WidgetType.InputText:
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
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 控件类型
    /// </summary>
    public enum WidgetType
    {
        None,
        SplitH,
        SplitV,
        Table,
        FirstPanel,
        SecondPanel,
        Row,
        Bottom,
        InputText,
        Tab
    }
    public class TableInfo
    {
        /// <summary>
        /// 获取数据源的存储过程名字
        /// </summary>
        public string DataSourse { get; set; } = "GetTableDemo";
        /// <summary>
        /// 存储过程需要的参数名字
        /// </summary>
        public string Parameter { get; set; } = "";
        /// <summary>
        /// 每一页显示多少条
        /// </summary>
        public int PageItems { get; set; } = 10;
        /// <summary>
        /// 一共有多少条
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前显示第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 一共有多少页
        /// </summary>
        public int PageCount { get; set; }
    }
    /// <summary>
    /// 获取数据途径 todo:后续增加从Api,设备等获取
    /// </summary>
    public class DataSourse
    {
        public string StoreName { get; set; } = "";
        public string Parameter { get; set; } = "";
    }
    public class Values
    {
        private object value;

        public DbType DbType { get; set; }
        public int ValueInt { get; set; }
        public string? ValueString { get; set; }
        public float ValueFloat { get; set; }
        public double Valuedouble { get; set; }
        public decimal Valuedecimal { get; set; }
        public object? Value
        {
            get {
                switch (DbType)
                {
                    case DbType.AnsiString:
                        break;
                    case DbType.Binary:
                        break;
                    case DbType.Byte:
                        break;
                    case DbType.Boolean:
                        break;
                    case DbType.Currency:
                        break;
                    case DbType.Date:
                        break;
                    case DbType.DateTime:
                        break;
                    case DbType.Decimal:
                        break;
                    case DbType.Double:
                        break;
                    case DbType.Guid:
                        break;
                    case DbType.Int16:
                        break;
                    case DbType.Int32:
                        return ValueInt;
                    case DbType.Int64:
                        break;
                    case DbType.Object:
                        break;
                    case DbType.SByte:
                        break;
                    case DbType.Single:
                        break;
                    case DbType.String:
                        break;
                    case DbType.Time:
                        break;
                    case DbType.UInt16:
                        break;
                    case DbType.UInt32:
                        break;
                    case DbType.UInt64:
                        break;
                    case DbType.VarNumeric:
                        break;
                    case DbType.AnsiStringFixedLength:
                        break;
                    case DbType.StringFixedLength:
                        break;
                    case DbType.Xml:
                        break;
                    case DbType.DateTime2:
                        break;
                    case DbType.DateTimeOffset:
                        break;
                    default:
                        break;
                }
                return ValueString;
            }
        }

    }
    public class MainPage
    {
        public Action? StateHasChanged { get; set; }
        /// <summary>
        /// 根控件
        /// </summary>
        public Control Controlmain { get; set; } = new() { Name = "主页面", CtrType = WidgetType.None, Zindex = 1 };
        public Control? SelectControl { get; set; }
        public WidgetType SelectedWidgetType { get; set; }
        public void StateHasChangedInvoke()
        {
            //var json = System.Text.Json.JsonSerializer.Serialize(Controlmain);
            StateHasChanged?.Invoke();
        }
        public Task SetSelectedControlByBoxAsync(WidgetType data)
        {
            SelectControl = null;//这里考虑不用置空
            SelectedWidgetType = data;
            return Task.CompletedTask;
        }
        public Task SetSelectControlByDesignerAsync(Control data)
        {
            // SelectControlByBox = data.Adapt<Control>();
            SelectControl = data;
            // SelectedWidgetType = null;
            StateHasChanged?.Invoke();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 递归找到第一个符合条件的
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control? FindFirst(string name)
        {
            return FindFirst(Controlmain.Controls, (s) => { return s.Name == name; });
        }

        public static Control? FindFirst(IEnumerable<Control> collection, Func<Control, bool> math)
        {
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    return item;
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindFirst(item.Controls, math);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return default;
        }
        public static List<Control> FindAll(IEnumerable<Control> collection, Func<Control, bool> math)
        {
            List<Control> list = new List<Control>();
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    list.Add(item);
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindAll(item.Controls, math);
                    list.AddRange(control);
                }
            }
            return list;
        }

    }
}
