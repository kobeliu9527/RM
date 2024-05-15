using EntityStore.JsonConvert;
using SharedPage.JsonConvert;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SharedPage.Model
{
    /// <summary>
    /// 坐标轴的配置
    /// </summary>
    public class ExAxis
    {

        /// <summary>
        /// id              
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? show { get; set; }
        /// <summary>
        /// 表示这个轴在哪个grid上,默认第一个
        /// </summary>
        public int? gridIndex { get; set; }
        /// <summary>
        /// 在多个 x 轴为数值轴的时候，可以开启该配置项自动对齐刻度。只对'value'和'log'类型的轴有效。
        /// </summary>
        public bool? alignTicks { get; set; }
        /// <summary>
        /// 默认 grid 中的第一个 x 轴在 grid 的下方（'bottom'），第二个 x 轴视第一个 x 轴的位置放在另一侧。
        /// 注：若未将 xAxis.axisLine.onZero 设为 false , 则该项无法生效
        /// </summary>
        public EPosition? position { get; set; }
        /// <summary>
        /// X 轴相对于默认位置的偏移，在相同的 position 上有多个 X 轴的时候有用。
        ///注：若未将 xAxis.axisLine.onZero 设为 false , 则该项无法生效
        /// </summary>
        public int? offset { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EAxisType? type { get; set; } //= "category";
        /// <summary>
        /// 坐标轴名称。
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 坐标轴名称显示位置。
        /// </summary>
        public ENameLocation? nameLocation { get; set; }
        /// <summary>
        /// 坐标轴的文字设置
        /// </summary>
        public ETextStyle? nameTextStyle { get; set; }
        /// <summary>
        /// 坐标轴名称与轴线之间的距离。
        /// </summary>
        public double? nameGap { get; set; }
        /// <summary>
        /// 坐标轴名字旋转，角度值。
        /// </summary>
        public double? nameRotate { get; set; }
        /// <summary>
        /// 坐标轴名字的截断。
        /// </summary>
        public ENameTruncate? nameTruncate { get; set; }
        /// <summary>
        /// 是否是反向坐标轴
        /// </summary>
        public bool? inverse { get; set; }
        /// <summary>
        /// boolean Array ['20%', '20%']坐标轴两边留白策略，类目轴和非类目轴的设置和表现不一样。        类目轴中 boundaryGap 可以配置为 true 和 false。默认为 true，这时候刻度只是作为分隔线，标签和数据点都会在两个刻度之间的带(band)中间。非类目轴，包括时间，数值，对数轴，boundaryGap是一个两个值的数组，分别表示数据最小值和最大值的延伸范围，可以直接设置数值或者相对的百分比，在设置 min 和 max 后无效。 示例：https://echarts.apache.org/zh/option.html#xAxis.boundaryGap
        /// </summary>
        public object? boundaryGap { get; set; }
        /// <summary>
        /// number string Function坐标轴刻度最小值。 可以设置成特殊值 'dataMin'，此时取数据在该轴上的最小值作为最小刻度。不设置时会自动计算最小值保证坐标轴刻度的均匀分布。在类目轴中，也可以设置为类目的序数（如类目轴 data: ['类A', '类B', '类C'] 中，序数 2 表示 '类C'。也可以设置为负数，如 -3）。当设置成 function 形式时，可以根据计算得出的数据最大最小值设定坐标轴的最小值。如：min: function(value){return value.min - 20;} 其中 value 是一个包含 min 和 max 的对象，分别表示数据的最大最小值，这个函数可返回坐标轴的最小值，也可返回 null/undefined 来表示“自动计算最小值”（返回 null/undefined 从 v4.8.0 开始支持）。
        /// </summary>
        public object? min { get; set; }
        /// <summary>
        /// 参考min
        /// </summary>
        public object? max { get; set; }
        /// <summary>
        /// 只在数值轴中（type: 'value'）有效。        是否是脱离 0 值比例。设置成 true 后坐标刻度不会强制包含零刻度。在双数值轴的散点图中比较有用。在设置 min 和 max 之后该配置项无效
        /// </summary>
        public bool? scale { get; set; }
        /// <summary>
        /// 坐标轴的分割段数，需要注意的是这个分割段数只是个预估值，最后实际显示的段数会在这个基础上根据分割后坐标轴刻度显示的易读程度作调整。        在类目轴中无效。
        /// </summary>
        public int? splitNumber { get; set; }
        /// <summary>
        /// 自动计算的坐标轴最小间隔大小。        例如可以设置成1保证坐标轴分割刻度显示成整数。只在数值轴或时间轴中（type: 'value' 或 'time'）有效。
        /// </summary>
        public int? minInterval { get; set; }
        /// <summary>
        /// 自动计算的坐标轴最大间隔大小。        例如，在时间轴（（type: 'time'））可以设置成 3600 * 24 * 1000 保证坐标轴分割刻度最大为一天。{    maxInterval: 3600 * 24 * 1000}    只在数值轴或时间轴中（type: 'value' 或 'time'）有效。
        /// </summary>
        public int? maxInterval { get; set; }
        /// <summary>
        /// 强制设置坐标轴分割间隔。        因为 splitNumber 是预估的值，实际根据策略计算出来的刻度可能无法达到想要的效果，这时候可以使用 interval 配合 min、max 强制设定刻度划分，一般不建议使用。无法在类目轴中使用。在时间轴（type: 'time'）中需要传时间戳，在对数轴（type: 'log'）中需要传指数值。
        /// </summary>
        public int? interval { get; set; }
        /// <summary>
        /// 对数轴的底数，只在对数轴中（type: 'log'）有效。
        /// </summary>
        public int? logBase { get; set; }
        /// <summary>
        /// 坐标轴是否是静态无法交互。
        /// </summary>
        public int? silent { get; set; }
        /// <summary>
        /// 坐标轴的标签是否响应和触发鼠标事件，默认不响应。
        /// </summary>
        public int? triggerEvent { get; set; }
        /// <summary>
        /// 坐标轴轴线相关设置。
        /// </summary>
        public EAxisLine? axisLine { get; set; }
        /// <summary>
        /// 坐标轴刻度相关设置。
        /// </summary>
        public EAxisTick? axisTick { get; set; }
        /// <summary>
        /// 坐标轴次刻度线相关设置 注意：次刻度线无法在类目轴（type: 'category'）中使用。
        /// </summary>
        public EMinorTick? minorTick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EaxisLabel? axisLabel { get; set; }
        public EsplitLine? splitLine { get; set; }
        public EminorSplitLine? minorSplitLine { get; set; }
        public EsplitArea? splitArea { get; set; }
        public List<object>? data { get; set; } //= new();
        public EaxisPointer? axisPointer { get; set; }
        /// <summary>
        /// 是否开启动画。
        /// </summary>
        public bool? animation { get; set; }
        /// <summary>
        /// 是否开启动画的阈值，当单个系列显示的图形数量大于这个阈值时会关闭动画
        /// </summary>
        public int? animationThreshold { get; set; }
        /// <summary>
        /// number Function  初始动画的时长，支持回调函数，可以通过每个数据返回不同的时长实现更戏剧的初始动画效果：如下示例：(越往后的数据延迟越大): function(idx) {  return idx * 100; }
        /// </summary>
        public object? animationDuration { get; set; }
        /// <summary>
        /// 初始动画的缓动效果。
        /// </summary>
        [DisplayName("初始动画效果"), Description("图表初始化时候的动画效果")]
        public EanimationEasing? animationEasing { get; set; }
        /// <summary>
        /// number Function 初始动画的延迟，支持回调函数，可以通过每个数据返回不同的 delay 时间实现更戏剧的初始动画效果。如下示例：(越往后的数据延迟越大): function(idx) {  return idx * 100; }
        /// </summary>
        public JsFunc animationDelay { get; set; } = new JsFunc("0");

        /// <summary>
        /// number Function 数据更新动画的时长。支持回调函数，可以通过每个数据返回不同的时长实现更戏剧的更新动画效果：
        /// </summary>
        public object? animationDurationUpdate { get; set; }
        /// <summary>
        /// 数据更新动画的缓动效果。
        /// </summary>
        public EanimationEasing? animationEasingUpdate { get; set; }
        /// <summary>
        /// number Function 数据更新动画的延迟，支持回调函数，可以通过每个数据返回不同的 delay 时间实现更戏剧的更新动画效果。如下示例：(越往后的数据延迟越大): function(idx) {  return idx * 100; }
        /// </summary>
        public object? animationDelayUpdate { get; set; }
        /// <summary>
        /// X 轴所有图形的 zlevel 值。 zlevel用于 Canvas 分层，不同zlevel值的图形会放置在不同的 Canvas 中，Canvas 分层是一种常见的优化手段。我们可以把一些图形变化频繁（例如有动画）的组件设置成一个单独的zlevel。需要注意的是过多的 Canvas 会引起内存开销的增大，在手机端上需要谨慎使用以防崩溃。zlevel 大的 Canvas 会放在 zlevel 小的 Canvas 的上面。
        /// </summary>
        public int? zlevel { get; set; }
        /// <summary>
        /// X轴组件的所有图形的z值。控制图形的前后顺序。z值小的图形会被z值大的图形覆盖。z相比zlevel优先级更低，而且不会创建新的 Canvas。
        /// </summary>
        public int? z { get; set; }
    }
}
