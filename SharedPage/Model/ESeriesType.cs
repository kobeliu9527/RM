using SharedPage.JsonConvert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    [JsonConverter(typeof(EnumConvert<ESeriesType>))]
    public enum ESeriesType
    {
        line,
        pie
    }
    /// <summary>
    /// 坐标轴类型
    /// </summary>
    [JsonConverter(typeof(EnumConvert<EAxisType>))]
    public enum EAxisType
    {
        /// <summary>
        /// 数值轴，适用于连续数据。
        /// </summary>
        value,

        /// <summary>
        /// 类目轴，适用于离散的类目数据。为该类型时类目数据可自动从 series.data 或 dataset.source 中取，或者可通过 xAxis.data 设置类目数据。
        /// </summary>
        category,
        /// <summary>
        /// 时间轴，适用于连续的时序数据，与数值轴相比时间轴带有时间的格式化，在刻度计算上也有所不同，例如会根据跨度的范围来决定使用月，星期，日还是小时范围的刻度。
        /// </summary>
        time,
        /// <summary>
        /// 对数轴。适用于对数数据。对数轴下的堆积柱状图或堆积折线图可能带来很大的视觉误差，并且在一定情况下可能存在非预期效果，应避免使用。
        /// </summary>
        log
    }
    [JsonConverter(typeof(EnumConvert<EPosition>))]
    public enum EPosition
    {
        [Description("上面")]
        top,
        [Description("下面")]
        bottom
    }
    [JsonConverter(typeof(EnumConvert<ENameLocation>))]
    public enum ENameLocation
    {
        end,
        start,
        center
    }
    [JsonConverter(typeof(EnumConvert<EFontStyle>))]
    public enum EFontStyle
    {
        normal,
        italic,
        oblique
    }
    [JsonConverter(typeof(EnumConvert<EFontWeight>))]
    public enum EFontWeight
    {
        normal,
        bold,
        bolder,
        lighter
    }
    [JsonConverter(typeof(EnumConvert<EAlign>))]
    public enum EAlign
    {
        left,
        center,
        right
    }
    [JsonConverter(typeof(EnumConvert<EVerticalAlign>))]
    public enum EVerticalAlign
    {
        top,
        middle,
        bottom
    }
    [JsonConverter(typeof(EnumConvert<EBorderType>))]
    public enum EBorderType
    {
        solid,
        dashed,
        dotted
    }
    [JsonConverter(typeof(EnumConvert<EOverflow>))]
    public enum EOverflow
    {
        truncate,
        @break,
        breakAll
    }
    /// <summary>
    /// 动画的缓动效果
    /// </summary>
    [JsonConverter(typeof(EnumConvert<EanimationEasing>))]
    public enum EanimationEasing
    {
        linear,
        quadraticIn,
        quadraticOut,
        quadraticInOut,
        cubicIn,
        cubicOut,
        cubicInOut,
        quarticIn,
        quarticOut,
        quarticInOut,
        quinticIn,
        quinticOut,
        quinticInOut,
        sinusoidalIn,
        sinusoidalOut,
        sinusoidalInOut,
        exponentialIn,
        exponentialOut,
        exponentialInOut,
        circularIn,
        circularOut,
        circularInOut,
        elasticIn,
        elasticOut,
        elasticInOut,
        backIn,
        backOut,
        backInOut,
        bounceIn,
        bounceOut,
        bounceInOut
    }

    [JsonConverter(typeof(EnumConvert<symbol>))]
    public enum symbol
    {
        [Description("无")]
        none,
        [Description("圆")]
        circle,
        [Description("矩形")]
        rect,
        [Description("矩形圆")]
        roundRect,
        [Description("三角形")]
        triangle,
        [Description("菱形")]
        diamond,
        [Description("大头")]
        pin,
        [Description("箭头")]
        arrow
    }

}
