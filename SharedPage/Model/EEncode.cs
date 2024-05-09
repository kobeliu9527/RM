namespace SharedPage.Model
{
    public class EEncode
    {
        /// <summary>
        /// 使用 “名为 product 的维度” 和 “名为 score 的维度” 的值在 tooltip 中显示;tooltip: ['product', 'score']
        /// </summary>
        ///
        public List<string>? tooltip { get; set; }

        /// <summary>
        /// 使用第一个维度和第三个维度的维度名连起来作为系列名。（有时候名字比较长，这可以避免在 series.name 重复输入这些名字）        seriesName: [1, 3],
        /// </summary>
        ///
        public List<int>? seriesName { get; set; }
        /// <summary>
        /// 表示使用第二个维度中的值作为 id。这在使用 setOption 动态更新数据时有用处，可以使新老数据用 id 对应起来，从而能够产生合适的数据更新动画。        itemId: 2,
        /// </summary>
        ///
        public int? itemId { get; set; }
        /// <summary>
        /// 指定数据项的名称使用第三个维度在饼图等图表中有用，可以使这个名字显示在图例（legend）中。 itemName: 3,
        /// </summary>
        ///
        public int? itemName { get; set; }
        /// <summary>
        /// 指定数据项的组 ID (groupId)。当全局过渡动画功能开启时，setOption 前后拥有相同 groupId 的数据项会进行动画过渡。 itemGroupId: 4,
        /// </summary>
        ///
        public int? itemGroupId { get; set; }
        /// <summary>
        /// 指定数据项对应的子数据组 ID (childGroupId)，用于实现多层下钻和聚合。详见 childGroupId。
        /// 从 v5.5.0 开始支持 itemChildGroupId: 5
        /// </summary>
        ///
        public int? itemChildGroupId { get; set; }
    }
}
