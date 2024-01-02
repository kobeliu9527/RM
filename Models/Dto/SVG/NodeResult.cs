namespace Models.Dto.SVG
{
    /// <summary>
    /// 结果信息:指示为x时候应该流向那些节点
    /// 
    /// </summary>
    /// <remarks>
    /// <para>
    /// 
    /// </para>
    /// </remarks>
    public class NodeResult
    {
        /// <summary>
        /// 节点名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 节点结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        public List<string>? Ids { get; set; }
       
    }
}