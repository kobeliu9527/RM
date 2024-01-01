using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 产品定义表:一个产品由多个产品(零件)组成组成
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long ProductId { get; set; }

        /// <summary>
        /// 一个产品由一个或多个产品组成,那么这多个产品公用一个RootId,表示一个真实成品. todo:在生成一个表,专门存放成品
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// 产品名字
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string? ProductDescription { get; set; }


        #region 导航属性

        /// <summary>
        /// 1对1导航:对应零件表中哪一个零件.每一个子产品都来自于零件表
        /// </summary>
        public long PartId { get; set; }
        /// <summary>
        /// 1对1导航:每一个子产品的组成都来自于零件表
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PartId))]
        public Part? Part { get; set; }

        #endregion 导航属性



        /// <summary>
        /// 用于实现产品的Tree形态,表示这个子产品所属于的父产品Id,成品的父产品是其本身Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 用于实现产品的Tree形态,表示这个子产品所属于的父产品
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public Product? Parent { get; set; }
        /// <summary>
        /// 用于实现产品的Tree形态,表示这个产品的子产品
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Product>? Products { get; set; }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Lot New()
        {
            Lot o = new Lot();
            o.LotSN = "根据产品的序列号规则生成";
            o.ProductId = ProductId;


            return o;
        }
    }
}