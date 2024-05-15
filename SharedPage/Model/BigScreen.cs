using EntityStore;
using EntityStore.SystemTable;
using Microsoft.AspNetCore.Components;
using SharedPage.Components;
using SqlSugar;
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
    [SugarTable("sys_" + nameof(BigScreen), IsCreateTableFiledSort = true)]
    //[SugarIndex("{table}_DisplayName",
    //                            nameof(BigScreen.DisplayName), OrderByType.Asc,
    //                            nameof(BigScreen.ModifyTime), OrderByType.Asc,
    //                            nameof(BigScreen.GroupName), OrderByType.Asc,
    //                            nameof(BigScreen.Imgurl), OrderByType.Asc,
    //                            nameof(BigScreen.OrderNum), OrderByType.Asc,
    //                            nameof(BigScreen.Css), OrderByType.Asc,
    //                            nameof(BigScreen.InterVal), OrderByType.Asc,
    //                            nameof(BigScreen.CreateTime), OrderByType.Asc)]
    //[SugarIndex("{table}_GroupName",
    //                            nameof(BigScreen.GroupName), OrderByType.Asc,
    //                            nameof(BigScreen.ModifyTime), OrderByType.Asc,
    //                            nameof(BigScreen.DisplayName), OrderByType.Asc,
    //                            nameof(BigScreen.Imgurl), OrderByType.Asc,
    //                            nameof(BigScreen.OrderNum), OrderByType.Asc,
    //                            nameof(BigScreen.Css), OrderByType.Asc,
    //                            nameof(BigScreen.InterVal), OrderByType.Asc,
    //                            nameof(BigScreen.CreateTime), OrderByType.Asc)]
    public class BigScreen : EntityBaseIdTime
    {
        /// <summary>
        /// 大屏名字
        /// </summary>
        [DisplayName("大屏名字"), SugarColumn(Length = Constants.NameLen)]
        public string DisplayName { get; set; } = "大屏名字";
        /// <summary>
        /// 缩略图路径
        /// </summary>
        [DisplayName("缩略图路径"), Description("在保存一个页面的时候,获取到当时的页面图像,保存下来,上传到服务器,名字为这个页面的id"), SugarColumn(Length = 50)]
        public string Imgurl { get; set; } = "./img/dp.jpg";
        /// <summary>
        /// 用于表示他属于那个组的
        /// </summary>
        [DisplayName("所属组名")]
        public string GroupName { get; set; } = "默认组";
        /// <summary>
        /// 在页面中的位置,越大越靠后
        /// </summary>
        [DisplayName("排序"), Description("在页面中的位置,越大越靠后")]
        public int OrderNum { get; set; }
        /// <summary>
        /// 设计的时候,画布的布局,运行时候全屏,只会用到背景色
        /// </summary>
        [SugarColumn(IsJson = true,ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public Css Css { get; set; } = new Css() { background_color = "#2B2C2D", top = "1%", left = "1%", width = "960px", height = "540px" };
        /// <summary>
        /// 整个页面后台多少秒请求一次
        /// </summary>
        public int InterVal { get; set; } = 5;
        /// <summary>
        /// 这个大屏的所有Echart图表
        /// </summary>
        [SugarColumn(IsJson = true,ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public List<ComponentInfo> ChartList { get; set; } = new();
        /// <summary>
        /// 表示哪些组可以看到这个屏幕
        /// </summary>
        [Navigate(typeof(BigScreenRoleReview), nameof(BigScreenRoleReview.BigScreenId), nameof(BigScreenRoleReview.RoleId))]
        public List<Role> ReviewRoles { get; set; }
        /// <summary>
        /// 表示哪些组编辑
        /// </summary>
        [Navigate(typeof(BigScreenRoleDesigner), nameof(BigScreenRoleDesigner.BigScreenId), nameof(BigScreenRoleDesigner.RoleId))]
        public List<Role> DesignerRoles { get; set; }

        [JsonIgnore, SugarColumn(IsIgnore = true)] public List<ComponentInfo> SelectedList { get; set; } = new();
        [JsonIgnore, SugarColumn(IsIgnore = true)] public ComponentInfo? SelectedByDesigner { get; set; }
        [JsonIgnore, SugarColumn(IsIgnore = true)] public ComponentType SelectedByToolBox { get; set; }
        [JsonIgnore, SugarColumn(IsIgnore = true)] public bool IsSelectedByToolBox { get; set; }
        [JsonIgnore, SugarColumn(IsIgnore = true)] public List<ComponentInfo> CopyList { get; set; } = new();
        /// <summary>
        /// 要执行刷新大屏委托
        /// </summary>
        [JsonIgnore, SugarColumn(IsIgnore = true)] public Action<bool>? RefreshHandel { get; set; }
        [JsonIgnore, SugarColumn(IsIgnore = true)] public EDataSource? DataSet { get; set; }


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
        public Action<DataTable?>? SetOption { get; set; }
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
    /// 大屏观看关系表
    /// </summary>
    [SugarTable("sys_" + nameof(BigScreenRoleReview))]
    public class BigScreenRoleReview : EntityBase
    {
        /// <summary>
        /// 大屏id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long BigScreenId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long RoleId { get; set; }
    }
    /// <summary>
    /// 大屏设计权限关系表
    /// </summary>
    [SugarTable("sys_" + nameof(BigScreenRoleDesigner))]
    public class BigScreenRoleDesigner : EntityBase
    {
        /// <summary>
        /// 大屏id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long BigScreenId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long RoleId { get; set; }
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
        [DisplayName("背景色"), Description("点击前面按钮选择颜色,如果需要透明度,选择颜色后在后面输入00-FF之间的值")]
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
            return sb.ToString().Replace('_', '-');
        }
    }
}
