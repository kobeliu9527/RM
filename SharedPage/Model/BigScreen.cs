using Microsoft.AspNetCore.Components;
using SharedPage.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// 表示一个大屏
    /// </summary>
    public class BigScreen
    {
        public long Id { get; set; }
        public bool IsDesigner { get; set; }
        [DisplayName("大屏名字")]
        public string Name { get; set; } = "大屏名字";
        /// <summary>
        /// 用于表示他属于那个组的
        /// </summary>
        [DisplayName("所属组名")]
        public string GroupName { get; set; } = "默认组";
        [DisplayName("排序"),Description("在页面中的位置,越大越靠后")]
        public int Order { get; set; }

        public Css Css { get; set; } = new Css() { background_color= "#2B2C2D" ,top="1%",left="1%",width="960px",height="540px"};
        /// <summary>
        /// 整个页面多少秒请求一次
        /// </summary>
        public int InterVal { get; set; } = 5000;
        /// <summary>
        /// 这个大屏的所有Echart图表
        /// </summary>
        public List<ComponentInfo> ChartList { get; set; } = new();

        [JsonIgnore] public List<ComponentInfo> SelectedList { get; set; } = new();
        [JsonIgnore] public ComponentInfo? SelectedByDesigner { get; set; }
        [JsonIgnore] public ComponentType? SelectedByToolBox { get; set; }
        [JsonIgnore] public List<ComponentInfo> CopyList { get; set; } = new();
        /// <summary>
        /// 要执行刷新大屏委托
        /// </summary>
        [JsonIgnore] public Action<bool>? RefreshHandel { get; set; }

        public EDataSource? DataSet { get; set; }


    }
    /// <summary>
    /// 组件信息,表示一个echart组件,或者一个table组件或者其他组件
    /// </summary>
    public class ComponentInfo
    {
        [DisplayName("组件Id"), Description("一定要确保全局唯一")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DisplayName("组件类型"), Description("组件类型")]
        public ComponentType ComponentType { get; set; }
        [DisplayName("配置项"), Description("如果为Echart组件,所对应的Option")]
        public EOption? Option { get; set; }
        [DisplayName("Y轴坐标"), Description("左上角为原点")]
        public double Top { get; set; }
        [DisplayName("X轴坐标"), Description("左上角为原点")]
        public double Left { get; set; }
        [DisplayName("宽度"), Description("占屏幕宽度的比例;范围0-100")]
        public double Width { get; set; } = 30;
        [DisplayName("高度"), Description("占屏幕高度的比例;范围0-100")]
        public double Height { get; set; } = 40;
        [DisplayName("角度"), Description("旋转的角度")]
        public double Angle { get; set; }
        /// <summary>
        /// 多少秒请求一次
        /// </summary>
        [DisplayName("请求间隔"), Description("多少秒请求一次,单位毫秒;负数表示只请求一次")]
        public int InterVal { get; set; } = 0;

        [DisplayName("数据来源"), Description("数据来源")]
        public DataSourceType DataSourceType { get; set; }

        [DisplayName("数据源名字"), Description("数据在统一请求的时候,一次返回多个数据集,用这个标识取哪一个")]
        public string DataName { get; set; } = "";

        /// <summary>
        /// 更新这个控件的委托
        /// </summary>
        [JsonIgnore]
        public Action<List<object[]>?>? SetOption { get; set; }
        [JsonIgnore]
        public MoveInfo MoveInfo { get; set; } = new();

        /// <summary>
        /// 利用json序列化实现,并且改了主键id;但是没有处理组件的内部组件的Id
        /// </summary>
        /// <returns></returns>
        public ComponentInfo Clone()
        {
            var json = JsonSerializer.Serialize(this);
            var res = JsonSerializer.Deserialize<ComponentInfo>(json);

            if (res != null)
            {
                res.Id = Guid.NewGuid().ToString();
                res.Top = Top + 10;
                res.Left = Left + 10;
                res.Id = Guid.NewGuid().ToString();
                if (res.Top > 90)
                {
                    res.Top = Top - 10;
                }
                if (res.Left > 90)
                {
                    res.Left = Left - 10;
                }
            }
            return res;
        }
    }
    public class MoveInfo
    {
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double MoveX { get; set; }
        public double MoveY { get; set; }
    }
    public enum DataSourceType
    {
        /// <summary>
        /// 来自于存储过程
        /// </summary>
        StoreProcess,
        /// <summary>
        /// 来自于Api接口;需要能对接上EdataSet
        /// </summary>
        [Description("web Api 的接口")]
        WebApi,
        /// <summary>
        /// 静态的
        /// </summary>
        [Description("静态的数据源")]
        StaticJson
    }

    /// <summary>
    /// 整个屏幕的数据源
    /// </summary>
    public class EDataSource
    {

    }
    public class Css
    {
        [DisplayName("宽度"), Description("有效的值为: 100px  20% ")]
        public string? width { get; set; }
        [DisplayName("高度"), Description("有效的值为: 100px  20% ")]
        public string? height { get; set; }
        [DisplayName("距离顶部"), Description("有效的值为: 100px  20% ")]
        public string? top { get; set; }
        [DisplayName("距离左边"), Description("有效的值为: 100px  20% ")]
        public string? left { get; set; }
        [DisplayName("背景色"),Description("点击前面按钮选择颜色,如果需要透明度,选择颜色后在后面输入00-FF之间的值")]
        public string? background_color { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //if (!string.IsNullOrWhiteSpace(width))
            //{
            //    sb.Append(nameof(width) + ":" + width +";"); ;
            //}
            ///todo 为了性能,后续可以采用全手动拼接  2.在增加颜色属性值是否有效
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string? value = property.GetValue(this)?.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    sb.Append(property.Name + ":" + value + ";");
                }
            }
            return sb.ToString().Replace('_','-');
        }
    }
}
