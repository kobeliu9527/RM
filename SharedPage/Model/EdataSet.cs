using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// https://echarts.apache.org/zh/option.html#dataset
    /// </summary>
    public class EdataSet
    {
        ///
        public string? Id { get; set; }
        ///
        public List<object[]>? source { get; set; } = new();
        ///
        public List<EDimension>? dimensions { get; set; }//sourceHeader
        ///
        public bool? sourceHeader { get; set; } = true;//sourceHeader
    }
}
