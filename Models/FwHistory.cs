using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [SugarIndex("index_SN_", nameof(FwHistory.LotSn), OrderByType.Asc)]
    [SugarIndex("index_SN_CreateTime_", nameof(FwHistory.LotSn), OrderByType.Asc,nameof(FwHistory.CreateTime),OrderByType.Asc)]
    [SplitTable(SplitType.Day)]
    [SugarTable("FwHistory_{year}{month}{day}")]
    public class FwHistory
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 产品条码
        /// </summary>
        public string? LotSn { get; set; }
        /// <summary>
        /// 传送结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        public long NodeModelId { get; set; }
        /// <summary>
        /// 节点名字
        /// </summary>
        public string? NodeModelName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SplitField] 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime UpdateTime { get; set; }
    }
}
