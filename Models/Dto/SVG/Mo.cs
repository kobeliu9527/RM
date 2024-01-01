using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 工单
    /// </summary>
    public class Mo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long MoId { get; set; }
        /// <summary>
        /// 所有子工单的唯一父级
        /// </summary>
        public long RootId { get; set; }
        /// <summary>
        /// 工单名字
        /// </summary>
        public string? MoName { get; set; }
        /// <summary>
        /// 工单描述
        /// </summary>
        public string? MoDescription { get; set; }
        /// <summary>
        /// 工单数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 工单已经完成的数量
        /// </summary>
        public int CompleteCount { get; set; }
        /// <summary>
        /// 工单状态 todo:后续应该改成枚举
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime ActualStartTime { get; set; }
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime ActualEndTime { get; set; }
        /// <summary>
        /// 这个工单要生产的产品
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 1对1导航:每一个工单都会对应一个产品
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ProductId))]
        public Product? Product { get; set; }


        /// <summary>
        /// 这个工单要用什么样的工作流来生产
        /// </summary>
        public long WorkFlowTemplateId { get; set; }
        /// <summary>
        /// 这个工单要用什么样的工作流来生产
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkFlowTemplateId))]
        public WorkFlowTemplate? WorkFlowTemplate { get; set; }


        /// <summary>
        /// 用于实现产品的Tree形态,表示这个工单的父级工单
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 用于实现产品的Tree形态,表示这个工单的父级工单
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public Mo? Parent { get; set; }
        /// <summary>
        /// 用于实现产品的Tree形态,表示这个工单的子工单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Mo>? Mos { get; set; }


        /// <summary>
        /// 将一个工单平均拆分成多少份,生成多个子工单
        /// </summary>
        /// <param name="pcs">要拆分的份数</param>
        public void SplitAll(int pcs)
        {
            //
        }
        /// <summary>
        /// 将一个取出一些出来,生产一个子工单
        /// </summary>
        /// <param name="pcs">要拆分多少出来</param>
        public void SplitOne(int pcs)
        {
            //
        }
        /// <summary>
        /// 为工单生成LotSn
        /// </summary>
        public void GenerateLotSn() 
        {
          //实际就是生成Lot条码:工单一定对应的有产品,产品或者工单一定有条码规则,根据这个条码规则就能生成
          //每生成一个条码,都需要在FwHistory表中记录
          //1.
        }
    }
}