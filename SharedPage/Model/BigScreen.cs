using SharedPage.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// 表示一个大屏
    /// </summary>
    public class BigScreen
    {
        public int Id { get; set; }
        public bool IsDesigner { get; set; }
        public string? Name { get; set; }
        /// <summary>
        /// 整个页面多少秒请求一次
        /// </summary>
        public int InterVal { get; set; } = 5000;
        /// <summary>
        /// 这个大屏的所有Echart图表
        /// </summary>
        public List<ComponentInfo> ChartList { get; set; } = new();
        public List<ComponentInfo> SelectedList { get; set; } = new();


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
        public float Top { get; set; }
        [DisplayName("X轴坐标"), Description("左上角为原点")]
        public float Left { get; set; }
        [DisplayName("宽度"), Description("占屏幕宽度的比例;范围0-100")]
        public float Width { get; set; } = 30;
        [DisplayName("高度"), Description("占屏幕高度的比例;范围0-100")]
        public float Height { get; set; } = 40;
        public float Angle { get; set; }
        /// <summary>
        /// 多少秒请求一次
        /// </summary>
        [DisplayName("请求间隔"), Description("多少秒请求一次,单位毫秒;负数表示只请求一次")]
        public int InterVal { get; set; } = 0;

        [DisplayName("数据来源"), Description("数据来源")]
        public DataSourceType DataSourceType { get; set; }

        [DisplayName("数据源名字"), Description("数据在统一请求的时候,一次返回多个数据集,用这个标识取哪一个")]
        public string DataName { get; set; } = "";

        public MoveInfo? MoveInfo { get; set; } = new();

        public ComponentInfo Clone()
        {
            var json = JsonSerializer.Serialize(this);
            var res = JsonSerializer.Deserialize<ComponentInfo>(json);
            if (res != null)
            {
                res.Top = Top + 10;
                res.Left = Left + 10;
                res.Id= Guid.NewGuid().ToString();
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
    public enum ComponentType
    {
        Echarts,
        Other
    }
}
