﻿using SharedPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.Base
{
    [JsonDerivedType(typeof(ESerieBase),typeDiscriminator:"base")]
    [JsonDerivedType(typeof(SeriePie), typeDiscriminator: "SeriePie")]
    [JsonDerivedType(typeof(SerieLine), typeDiscriminator: "SerieLine")]
    
    public class ESerieBase
    {
        public virtual string? type { get; set; } = "line";
        public virtual string? name { get; set; }
        /// <summary>
        /// 正常来说,数据格式是[['AA', 332], ['CC', 124], ['FF', 412], ... ],其中['AA', 332]表示一个数据,但是,更多时候我们会简写成[20，14，32....],省略掉了AA BB CC ...,因为类目轴已经指定了,不需要每次都来这里指定
        /// </summary>
        public virtual List<object> data { get; set; } = new();
        /// <summary>
        /// 数据源为dataset的时候,只能维度是按照列还是行来
        /// </summary>
        public virtual string? seriesLayoutBy { get; set; } = "column";
        /// <summary>
        /// 备用
        /// </summary>
        //public string? id { get; set; }
    }
}
