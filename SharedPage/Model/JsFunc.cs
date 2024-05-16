using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// 非原创,来自于github上一个叫Blazor.ECharts项目
    /// </summary>
    public record JsFunc
    {
        /// <summary>
        /// 
        /// </summary>
        public JsFunc() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="raw"></param>
        public JsFunc(string raw)
        {
            RAW = raw;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";
    }
    /// <summary>
    /// 处理参数为数字和字符串的时候自动识别
    /// </summary>
    public class JsNumString
    {
        /// <summary>
        /// 处理参数为数字和字符串的时候自动识别
        /// </summary>
        /// <param name="raw"></param>
        public JsNumString(string raw)
        {
            RAW = raw;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";
    }
    /// <summary>
    /// 适配函数和数字
    /// </summary>
    public class JsFuncNum
    {
        /// <summary>
        /// 处理参数为数字和字符串的时候自动识别
        /// </summary>
        /// <param name="raw"></param>
        public JsFuncNum(string raw)
        {
            RAW = raw;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";
    }
    /// <summary>
    /// 适配函数和数字和字符串,如果是函数,必须要有function,如果是确实是数字的字符串用@符号
    /// </summary>
    public class JsFuncNumString
    {
        public JsType JsType { get; set; } = JsType.String;
        /// <summary>
        /// 处理参数为数字和字符串的时候自动识别
        /// </summary>
        /// <param name="raw"></param>
        public JsFuncNumString(string raw)
        {
            RAW = raw;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";
    }
    public enum JsType
    {
        /// <summary>
        /// 数字
        /// </summary>
        Num,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 函数
        /// </summary>
        Function,
        /// <summary>
        /// 数组
        /// </summary>
        Array,
        /// <summary>
        /// 对象
        /// </summary>
        Object,
        /// <summary>
        /// 字符串或者数组
        /// </summary>
        StringArray
    }
}
