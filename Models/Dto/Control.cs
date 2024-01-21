using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Models.Tools;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
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
        [Inject]
        [NotNull]
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ISqlSugarClient? db { get; set; }
        #region 设计时属性
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsSelect { get; set; }
        #endregion
        #region 按钮属性
        public Button? Button { get; set; }
        #endregion
        #region 输入框属性
        public InputText? InputText { get; set; }
        #endregion
        #region 表格属性
        [NotNull]
        public Table? TableInfo { get; set; }
        #endregion
        #region 通用属性

        /// <summary>
        /// 控件唯一Id
        /// </summary>
        [DisplayName("Id:"), Description("用于数据绑定,筛选等唯一key:")]
        public long Id { get; set; }
        /// <summary>
        /// 控件唯一Id
        /// </summary>
        [DisplayName("Key:"), Description("用于数据绑定,筛选等唯一key:")]
        public string Key { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 控件的名字
        /// </summary>
        [DisplayName("标题:"), Description("用于前台显示的名字:")]
        public string DisplayName { get; set; } = "NA";
        /// <summary>
        /// 绑定字段名
        /// </summary>
        [DisplayName("绑定字段名:"), Description("用于绑定数据库中的字段或者参数:")]
        public string FieldName { get; set; } = "Na";
        /// <summary>
        /// 参数类型,output?
        /// </summary>
        [DisplayName("是否为输入输出参数:"), Description("用于绑定数据库中的字段或者参数:")]
        public ParameterDirection ParameterType { get; set; }
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
        [DisplayName("层叠顺序:"), Description("系统有算法,根据先放的在下面,后方的在上面,若非必要,不要更改:")]
        public int Zindex { get; set; }
        /// <summary>
        /// 控件高度
        /// </summary>
        [DisplayName("控件高度:"), Description("逻辑:占用父级容器高度的百分比 0-100,自适应控件最小高度:")]
        public int Height { get; set; } = 100;
        /// <summary>
        /// 最小高度
        /// </summary>
        [DisplayName("最小高度:"), Description("占用父级容器的最小高度,只有父级元素是行容器的时候有效")]
        public int MinHeight { get; set; } = 30;
        /// <summary>
        /// 最小高度
        /// </summary>
        [DisplayName("最小高度:"), Description("像素/%")]
        public string MinHeightUnit { get; set; } = "px";
        /// <summary>
        /// 最大高度
        /// </summary>
        [DisplayName("最大高度:"), Description(":")]
        public int MaxHeight { get; set; } = 100;
        /// <summary>
        /// 控件宽度
        /// </summary>
        [DisplayName("控件宽度:"), Description("逻辑:1-12之间的值,父级容器被分成12分,这个值表示占用多少.一般放在Row控件下的子控件设置此属性:")]
        public int Width { get; set; } = 4;
        /// <summary>
        /// 控件宽度
        /// </summary>
        [DisplayName("起始偏移:"), Description("相对于前一个控件,间隔多少个单位:")]
        public int Offset { get; set; } = 0;
        /// <summary>
        /// 当为Split的时候,表示第一个panel占比
        /// </summary>
        [DisplayName("FirstPanel占比:"), Description("当为Split的时候,表示第一个panel占比,默认40:")]
        public int Basis { get; set; } = 40;

        /// <summary>
        /// 子控件
        /// </summary>
        [DisplayName("子控件:"), Description("容器控件的子控件:")]
        public List<Control> Controls { get; set; } = new List<Control>();
        /// <summary>
        /// 数据源
        /// </summary>
        [DisplayName("数据源:"), Description("获取数据,或者执行命令相关:")]
        public DataSourse DataSourse { get; set; } = new DataSourse();
        /// <summary>
        /// 结果
        /// </summary>
        [DisplayName("结果:"), Description("不同类型的结果:")]
        public Values Values { get; set; } = new Values();
        /// <summary>
        /// 执行存储过程之后,返回给表格要赋值给哪一个表格
        /// </summary>
        [DisplayName("返回结果赋值给:"), Description("执行存储过程之后,返回给表格要赋值给哪一个表格:")]
        public string ReturnTable { get; set; } = "";


        #endregion
        /// <summary>
        /// 复选框信息
        /// </summary>
        public CheckBox? CheckBox { get; set; }
        /// <summary>
        /// 用于其他控件更新自己
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Action? UpdateSelf { get; set; }


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
                    var first = new Control(WidgetType.FirstPanel) { DisplayName = "第一个容器" };
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
                    TableInfo = new Table();
                    DisplayName = "表格";
                    break;
                case WidgetType.Tab:
                    DisplayName = "多页面组件";
                    var tab1 = new Control()
                    {
                        DisplayName = "页面1",
                        CtrType = WidgetType.TabItem,
                    };
                    var tab2 = new Control()
                    {
                        DisplayName = "页面2",
                        CtrType = WidgetType.TabItem
                    };
                    tab1.Zindex = Zindex;
                    tab2.Zindex = Zindex;
                    Controls.Add(tab1);
                    Controls.Add(tab2);
                    break;
                case WidgetType.CheckBox:
                    Values.DbType = System.Data.DbType.Boolean;
                    DisplayName = "复选框";
                    CheckBox = new CheckBox();
                    break;
                default:
                    break;
            }
        }

    }
    public class ControInfoBase
    {
        [DisplayName("禁用:"), Description("是否禁用此图标:")]
        public bool IsDisabled { get; set; }
        [DisplayName("是否显示标题名:"), Description("是否显示标签:")]
        public bool ShowLabel { get; set; } = true;
        [DisplayName("外观颜色:"), Description("边框或者整体颜色:")]
        public Color Color { get; set; } = Color.Primary;
        [DisplayName("自动获取焦点:"), Description("是否自动获取焦点:")]
        public bool IsAutoFocus { get; set; }
        [DisplayName("大小:"), Description("按钮等的大小:")]
        public Size Size { get; set; } = Size.None;
        #region 值改变后
        /// <summary>
        /// 值改变后执行允许
        /// </summary>
        [DisplayName("值改变后执行允许:"), Description("值改变后执行允许:")]
        public bool ChangeEnAble { get; set; }
        /// <summary>
        /// 执行的存储过程名
        /// </summary>
        [DisplayName("执行的存储过程名:"), Description("执行的存储过程名:")]
        public string ChangeStoreName { get; set; } = "";
        /// <summary>
        /// 执行存储过程需要的参数
        /// </summary>
        [DisplayName("执行存储过程需要的参数:"), Description("执行存储过程需要的参数:")]
        public List<string>? ChangeStoreParmeter { get; set; } = new();///
        #endregion
        #region 回车后,或者按钮点击后
        [DisplayName("回车执行:"), Description("文本框是否执行回车事件:")]
        public bool EnterEnAble { get; set; }
        /// <summary>
        /// 按回车后执行的存储过程
        /// </summary>
        [DisplayName("回车执行的存储过程:"), Description("按回车后执行的存储过程:")]
        public string EnterStoreName { get; set; } = "";
        [DisplayName("回车执行的存储过程的参数:"), Description("按回车后执行的存储过程需要的参数:")]
        public List<string> EnterStoreParmeter { get; set; } = new List<string>();
        #endregion
    }
    public class Button : ControInfoBase
    {
        [DisplayName("异步:"), Description("启用后,后台任务没完成时候,按钮不可用:")]
        public bool IsAsync { get; set; }
        [DisplayName("外边框线:"), Description("是否显示外边框线:")]
        public bool IsOutline { get; set; }
        [DisplayName("按钮风格:"), Description("按钮风格:")]
        public ButtonStyle ButtonStyle { get; set; }
        [DisplayName("按钮图标:"), Description("按钮图标:")]
        public string Icon { get; set; } = "";
        /// <summary>
        /// 如有父级主键的话,需要指定父级表的名字(内部绑定的是唯一Key)
        /// </summary>
        [DisplayName("父级表:"), Description("如有父级主键的话,需要指定父级表的名字(内部绑定的是唯一Key):")]
        public string RequestParentTable { get; set; } = "";
        /// <summary>
        /// 如有父级主键的话,绑定表中的哪一个字段,需要手动输入,内部逻辑:每次点击行都会保存 
        /// 表key+字段key
        /// </summary>
        [DisplayName("父级表主键参数名:"), Description("父级表中哪一个字段的名字作为主键Id:")]
        public string RequestParentTableIdName { get; set; } = "Id";
    }
    public class InputText : ControInfoBase
    {
        [DisplayName("自动去头尾空格:"), Description("点击确认后,自动去头尾空格:")]
        public bool IsTrim { get; set; }
        [DisplayName("提示水印信息:"), Description("文本框的提示信息:")]
        public string PlaceHolder { get; set; } = "请输入";
        #region 按ESC键后
        [DisplayName("按ESC键后执行允许:"), Description("文本框是否执行Esc事件:")]
        public bool EscEnAble { get; set; }
        [DisplayName("返回键执行的存储过程:"), Description("按返回键后执行的存储过程:")]
        public string EscStoreName { get; set; } = "";
        [DisplayName("返回键执行的存储过程的参数:"), Description("按返回键后执行的存储过程需要的参数:")]
        public IEnumerable<string>? EscStoreParmeter { get; set; }
        #endregion

    }
    public class Table : ControInfoBase
    {
        /// <summary>
        /// 表格大小
        /// </summary>
        public TableSize TableSize { get; set; } = TableSize.Compact;
        
        [DisplayName("需要绑定字段名集合:"), Description("这个表对应的字段名:逗号分割")]
        public string TableFieldNames { get; set; } = "";
        /// <summary>
        /// 这个表格的列信息
        /// </summary>
        [DisplayName("字段名集合:"), Description("这个表对应的字段名:逗号分割")]
        public IEnumerable<TableCol> TableFields { get; set; } = new List<TableCol>();
        /// <summary>
        /// 全部数据,只有第一次或者手动刷新才会更新这个
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DataTable UserData = new DataTable();
        /// <summary>
        /// 分页数据,根据UserData得到
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DataTable PageDataTable = new();
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<SelectedItem>? PageItemsSource { get; set; } = [
            new("10", "10条/页"),
            new("20", "20条/页")];
        /// <summary>
        /// 获取数据源的存储过程名字
        /// </summary>
        [DisplayName("请求数据地址:"), Description("一般是存储过程的名字:")]
        public string RequestAddress { get; set; } = "GetTableDemo";
        /// <summary>
        /// 执行的存储过程(url)需要的参数名
        /// </summary>
        [DisplayName("请求数据需要参数名:"), Description("执行的存储过程(url)需要的参数名(key):")]
        public List<string> RequestParmeter { get; set; } = new List<string>();
        /// <summary>
        /// 如有父级主键的话,需要指定父级表的名字(内部绑定的是唯一Key)
        /// </summary>
        [DisplayName("父级表:"), Description("如有父级主键的话,需要指定父级表的名字(内部绑定的是唯一Key):")]
        public string RequestParentTable { get; set; } = "";
        /// <summary>
        /// 父级表中哪一个字段的名字作为主键Id
        /// </summary>
        [DisplayName("父级表主键Id名:"), Description("父级表中哪一个字段的名字作为主键Id:")]
        public string RequestParentTableIdName { get; set; } = "Id";

        /// <summary>
        /// 过期,不建议使用
        /// </summary>
        [DisplayName("绑定那些字段:"), Description("表格中那些字段需要跟界面控件绑定:")]
        public List<string> FieldNameList { get; set; } = new List<string>();
        /// <summary>
        /// 存储过程需要的参数名字 todo :这个参数没有用,可以删除
        /// </summary>
        //public string Parameter { get; set; } = "";
        /// <summary>
        /// 每一页显示多少条
        /// </summary>
        public int PageItems { get; set; } = 15;
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
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
        //public TableSize TableSize { get; set; } = TableSize.Compact;
    }
    /// <summary>
    /// 表格中列信息
    /// </summary>
    public class TableCol
    {
        /// <summary>
        /// 用于绑定数据库中的字段或者参数
        /// </summary>
        [DisplayName("绑定字段名:"), Description("用于绑定数据库中的字段或者参数:")]
        public string FieldName { get; set; } = "Na";
        [DisplayName("前台显示:"), Description("前台显示,后续应该支持多语言")]
        public string DisplayName { get; set; } = "DisplayName";
        [DisplayName("绑定字段名:"), Description("用于绑定数据库中的字段或者参数:")]
        public System.Data.DbType DbType { get; set; } = System.Data.DbType.String;
        [DisplayName("是否可编辑:"), Description("是否可编辑:")]
        public bool Editable { get; set; } =true;
        [DisplayName("是否显示:"), Description("是否显示:")]
        public bool Visible { get; set; } =true;
        /// <summary>
        /// 这一列是否要用来筛选
        /// </summary>
        [DisplayName("是否为筛选列:"), Description("是否为筛选列:")]
        public bool Filterable { get; set; } = true;
        /// <summary>
        /// 选中列的值
        /// </summary>
        [DisplayName("选中列的值:"), Description("选中列的值:")]
        public object? val { get; set; } 
        /// <summary>
        /// 这一列要跟那些控件做绑定
        /// </summary>
        [DisplayName("绑定哪些控件"), Description("绑定哪些控件:")]
        public List<string> FieldNameList { get; set; } = new List<string>();
    }
    public class CheckBox : ControInfoBase
    {

    }
}
