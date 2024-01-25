namespace Models.Dto.SVG
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 矩形节点
        /// </summary>
        Square,
        /// <summary>
        /// 菱形节点
        /// </summary>
        Diamond,//菱形
        /// <summary>
        /// 椭圆节点
        /// </summary>
        Ellipse,//椭圆
        /// <summary>
        /// 指定的的节点完成
        /// </summary>
        Appoint,
        /// <summary>
        /// 其他情况
        /// </summary>
        Other
    }
}