using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// 
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
        public JsFuncNumString(JsType type)
        {
            JsType = type;
        }
        public JsFuncNumString(string raw, JsType type)
        {
            RAW = raw;
            JsType = type;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";

        public bool IsOk()
        {
            switch (this.JsType)
            {
                case JsType.Num:
                    if (decimal.TryParse(RAW, out decimal num))
                    {
                        return true;
                    }
                    break;
                case JsType.String:
                    return true;
                case JsType.Function:
                    if (RAW.IndexOf("function", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return true;
                    }
                    break;
                case JsType.Array:
                    if (RAW.StartsWith("[") && RAW.EndsWith("]"))
                    {
                        return true;
                    }
                    break;
                case JsType.Object:
                    if (RAW.StartsWith("{") && RAW.EndsWith("}"))
                    {
                        return true;
                    }
                    break;
                case JsType.Bool:
                    if (bool.TryParse(RAW, out bool B))
                    {
                        return true;
                    }
                    break;
                case JsType.StringArray:
                    break;
                default:
                    break;
            }
            return false;
        }
    }
    public enum JsType
    {
        /// <summary>
        /// 
        /// </summary>
        Bool,
        /// <summary>
        /// 数字
        /// </summary>
        [Description("数字")] Num,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 函数
        /// </summary>
        [Description("函数")] Function,
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
