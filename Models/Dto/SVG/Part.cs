using SqlSugar;

namespace Models.Dto.SVG
{
    /// <summary>
    /// 零件表:一个零件可能由多个零件组成
    /// </summary>
    public class Part
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long PartId { get; set; }
        /// <summary>
        /// 零件名字
        /// </summary>
        public string? PartName { get; set; }
        /// <summary>
        /// 零件描述
        /// </summary>
        public string? PartDescription { get; set; }
        ///// <summary>
        ///// 产品组成
        ///// </summary>
        //[Navigate(typeof(ProductAndPart), nameof(ProductAndPart.PartId), nameof(ProductAndPart.ProductId))]//BookA表中的studenId
        //public List<Product> Products { get; set; }
        /// <summary>
        /// 产品组成,Tree形态
        /// </summary>
        public long ParentId { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public Part? Parent { get; set; } 
        [SugarColumn(IsIgnore = true)]
        public List<Part>? Parts { get; set; }
        
    }
}
