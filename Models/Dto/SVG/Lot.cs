using Models.NotEntity;
using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 产品条码表
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long LotId { get; set; }
        /// <summary>
        /// 产品条码
        /// </summary>
        public string? LotSN { get; set; }

        /// <summary>
        /// 这个产品SN属于那个工单
        /// </summary>
        public long MoId { get; set; }
        /// <summary>
        /// 这个产品SN属于那个工单
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MoId))]
        public Mo? Mo { get; set; }

        /// <summary>
        /// 这个产品SN属于那个工单,原始工单
        /// </summary>
        public long MoMainId { get; set; }
        /// <summary>
        /// 这个产品SN属于那个工单,原始工单
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MoMainId))]
        public Mo? MoMain { get; set; }
        /// <summary>
        /// 产品Id:表示这个SN是什么产品
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 1对1导航:表示这个SN是什么产品
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ProductId))]
        public Product? Product { get; set; }

        /// <summary>
        /// 当前产品对应的工作流实例的Id
        /// </summary>
        public long WorkFlowTemplateId { get; set; }
        /// <summary>
        /// 当前产品对应的工作流实例
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WorkFlowTemplateId))]
        public WorkFlowTemplate? WorkFlowTemplate { get; set; }
        /// <summary>
        /// 上一个节点的Id:多个节点执行完才到下一个节点的时候,这里设计就不合适了
        /// </summary>
        public long PreNodeModelId { get; set; }
        /// <summary>
        /// 当前节点的Id
        /// </summary>
        public long CurrentNodeModelId { get; set; }
        /// <summary>
        /// 下一个节点的Id:当有多个节点的时候,这里设计就不合适了
        /// </summary>
        public long NextNodeModelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long GetNextNodeModelId()
        {
          //  WorkFlowTemplate.GetNextNodeModelId();
          throw new NotImplementedException();
        }
        /// <summary>
        /// 传送到指定节点
        /// </summary>
        public Result<string> GoByNodeModelId(long NodeModelId)
        {
            //WorkFlowTemplate.GetNextNodeModelId();
            //1.获取要传送的目的节点NodeModelId
            //2.获取传送到这个目的节点所需要的条件,一般也就是指必须经过了那些节点
            //3.在history中,寻找这个节点所经过的节点,根第二步骤得到的做比较,合格,就把lot表中的当前节点位置更新为NodeModelId,并在history中记录;不合格,就返回差那些节点,同时后期也会在history中记录,但是结果为失败,表示请求过,但是失败了
            //
            FwHistory history = new FwHistory() {
                CreateTime = DateTime.Now,
                LotSn = LotSN,
                Result = false
            };
            throw new NotImplementedException();
        }
    }
}