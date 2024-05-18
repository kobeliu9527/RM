using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using SharedPage.Model;

using System;

namespace SharedPage.Model
{
    /// <summary>
    ///直角坐标系grid中的x轴，一般情况下单个grid组件最多只能放上下两个x轴，多于两个x轴需要通过配置offset属性防止同个位置多个x轴的重叠。 
    /// </summary>
    public class xAxis
    {
        /// <summary>
        ///组件ID。默认不指定。指定则可用于在option或者API中引用组件。 
        /// </summary>
        public JsVal? id { get; set; }
        /// <summary>
        ///是否显示x轴。 
        /// </summary>
        public JsVal? show { get; set; }
        /// <summary>
        ///x轴所在的grid的索引，默认位于第一个grid。 
        /// </summary>
        public JsVal? gridIndex { get; set; }
        /// <summary>
        ///从v5.3.0开始支持在多个x轴为数值轴的时候，可以开启该配置项自动对齐刻度。只对'value'和'log'类型的轴有效。 
        /// </summary>
        public JsVal? alignTicks { get; set; }
        /// <summary>
        ///x轴的位置。可选：默认grid中的第一个x轴在grid的下方（'bottom'），第二个x轴视第一个x轴的位置放在另一侧。注：若未将xAxis.axisLine.onZero设为false,则该项无法生效 
        /// </summary>
        public JsVal? position { get; set; }
        /// <summary>
        ///X轴相对于默认位置的偏移，在相同的position上有多个X轴的时候有用。注：若未将xAxis.axisLine.onZero设为false,则该项无法生效 
        /// </summary>
        public JsVal? offset { get; set; }
        /// <summary>
        ///坐标轴类型。可选：'value'数值轴，适用于连续数据。'category'类目轴，适用于离散的类目数据。为该类型时类目数据可自动从series.data或dataset.source中取，或者可通过xAxis.data设置类目数据。'time'时间轴，适用于连续的时序数据，与数值轴相比时间轴带有时间的格式化，在刻度计算上也有所不同，例如会根据跨度的范围来决定使用月，星期，日还是小时范围的刻度。'log'对数轴。适用于对数数据。对数轴下的堆积柱状图或堆积折线图可能带来很大的视觉误差，并且在一定情况下可能存在非预期效果，应避免使用。 
        /// </summary>
        public JsVal? type { get; set; }
        /// <summary>
        ///坐标轴名称。 
        /// </summary>
        public JsVal? name { get; set; }
        /// <summary>
        ///坐标轴名称显示位置。可选： 
        /// </summary>
        public JsVal? nameLocation { get; set; }
        /// <summary>
        ///坐标轴名称与轴线之间的距离。 
        /// </summary>
        public JsVal? nameGap { get; set; }
        /// <summary>
        ///坐标轴名字旋转，角度值。 
        /// </summary>
        public JsVal? nameRotate { get; set; }
        /// <summary>
        ///是否是反向坐标轴。 
        /// </summary>
        public JsVal? inverse { get; set; }
        /// <summary>
        ///坐标轴两边留白策略，类目轴和非类目轴的设置和表现不一样。类目轴中boundaryGap可以配置为true和false。默认为true，这时候刻度只是作为分隔线，标签和数据点都会在两个刻度之间的带(band)中间。非类目轴，包括时间，数值，对数轴，boundaryGap是一个两个值的数组，分别表示数据最小值和最大值的延伸范围，可以直接设置数值或者相对的百分比，在设置min和max后无效。示例： 
        /// </summary>
        public JsVal? boundaryGap { get; set; }
        /// <summary>
        ///坐标轴刻度最小值。可以设置成特殊值'dataMin'，此时取数据在该轴上的最小值作为最小刻度。不设置时会自动计算最小值保证坐标轴刻度的均匀分布。在类目轴中，也可以设置为类目的序数（如类目轴data:['类A','类B','类C']中，序数2表示'类C'。也可以设置为负数，如-3）。当设置成function形式时，可以根据计算得出的数据最大最小值设定坐标轴的最小值。如：其中value是一个包含min和max的对象，分别表示数据的最大最小值，这个函数可返回坐标轴的最小值，也可返回null/undefined来表示“自动计算最小值”（返回null/undefined从v4.8.0开始支持）。 
        /// </summary>
        public JsVal? min { get; set; }
        /// <summary>
        ///坐标轴刻度最大值。可以设置成特殊值'dataMax'，此时取数据在该轴上的最大值作为最大刻度。不设置时会自动计算最大值保证坐标轴刻度的均匀分布。在类目轴中，也可以设置为类目的序数（如类目轴data:['类A','类B','类C']中，序数2表示'类C'。也可以设置为负数，如-3）。当设置成function形式时，可以根据计算得出的数据最大最小值设定坐标轴的最小值。如：其中value是一个包含min和max的对象，分别表示数据的最大最小值，这个函数可返回坐标轴的最大值，也可返回null/undefined来表示“自动计算最大值”（返回null/undefined从v4.8.0开始支持）。 
        /// </summary>
        public JsVal? max { get; set; }
        /// <summary>
        ///只在数值轴中（type:'value'）有效。是否是脱离0值比例。设置成true后坐标刻度不会强制包含零刻度。在双数值轴的散点图中比较有用。在设置min和max之后该配置项无效。 
        /// </summary>
        public JsVal? scale { get; set; }
        /// <summary>
        ///坐标轴的分割段数，需要注意的是这个分割段数只是个预估值，最后实际显示的段数会在这个基础上根据分割后坐标轴刻度显示的易读程度作调整。在类目轴中无效。 
        /// </summary>
        public JsVal? splitNumber { get; set; }
        /// <summary>
        ///自动计算的坐标轴最小间隔大小。例如可以设置成1保证坐标轴分割刻度显示成整数。只在数值轴或时间轴中（type:'value'或'time'）有效。 
        /// </summary>
        public JsVal? minInterval { get; set; }
        /// <summary>
        ///自动计算的坐标轴最大间隔大小。例如，在时间轴（（type:'time'））可以设置成3600*24*1000保证坐标轴分割刻度最大为一天。只在数值轴或时间轴中（type:'value'或'time'）有效。 
        /// </summary>
        public JsVal? maxInterval { get; set; }
        /// <summary>
        ///强制设置坐标轴分割间隔。因为splitNumber是预估的值，实际根据策略计算出来的刻度可能无法达到想要的效果，这时候可以使用interval配合min、max强制设定刻度划分，一般不建议使用。无法在类目轴中使用。在时间轴（type:'time'）中需要传时间戳，在对数轴（type:'log'）中需要传指数值。 
        /// </summary>
        public JsVal? interval { get; set; }
        /// <summary>
        ///对数轴的底数，只在对数轴中（type:'log'）有效。 
        /// </summary>
        public JsVal? logBase { get; set; }
        /// <summary>
        ///坐标轴是否是静态无法交互。 
        /// </summary>
        public JsVal? silent { get; set; }
        /// <summary>
        ///坐标轴的标签是否响应和触发鼠标事件，默认不响应。事件参数如下： 
        /// </summary>
        public JsVal? triggerEvent { get; set; }
        /// <summary>
        ///是否开启动画。 
        /// </summary>
        public JsVal? animation { get; set; }
        /// <summary>
        ///是否开启动画的阈值，当单个系列显示的图形数量大于这个阈值时会关闭动画。 
        /// </summary>
        public JsVal? animationThreshold { get; set; }
        /// <summary>
        ///初始动画的时长，支持回调函数，可以通过每个数据返回不同的时长实现更戏剧的初始动画效果： 
        /// </summary>
        public JsVal? animationDuration { get; set; }
        /// <summary>
        ///初始动画的缓动效果。不同的缓动效果可以参考缓动示例。 
        /// </summary>
        public JsVal? animationEasing { get; set; }
        /// <summary>
        ///初始动画的延迟，支持回调函数，可以通过每个数据返回不同的delay时间实现更戏剧的初始动画效果。如下示例：也可以看该示例 
        /// </summary>
        public JsVal? animationDelay { get; set; }
        /// <summary>
        ///数据更新动画的时长。支持回调函数，可以通过每个数据返回不同的时长实现更戏剧的更新动画效果： 
        /// </summary>
        public JsVal? animationDurationUpdate { get; set; }
        /// <summary>
        ///数据更新动画的缓动效果。 
        /// </summary>
        public JsVal? animationEasingUpdate { get; set; }
        /// <summary>
        ///数据更新动画的延迟，支持回调函数，可以通过每个数据返回不同的delay时间实现更戏剧的更新动画效果。如下示例：也可以看该示例 
        /// </summary>
        public JsVal? animationDelayUpdate { get; set; }
        /// <summary>
        ///X轴所有图形的zlevel值。zlevel用于Canvas分层，不同zlevel值的图形会放置在不同的Canvas中，Canvas分层是一种常见的优化手段。我们可以把一些图形变化频繁（例如有动画）的组件设置成一个单独的zlevel。需要注意的是过多的Canvas会引起内存开销的增大，在手机端上需要谨慎使用以防崩溃。zlevel大的Canvas会放在zlevel小的Canvas的上面。 
        /// </summary>
        public JsVal? zlevel { get; set; }
        /// <summary>
        ///X轴组件的所有图形的z值。控制图形的前后顺序。z值小的图形会被z值大的图形覆盖。z相比zlevel优先级更低，而且不会创建新的Canvas。 
        /// </summary>
        public JsVal? z { get; set; }
    }
}
