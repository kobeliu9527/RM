using SqlSugar;

namespace Models.System
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SysModule : EntityBase
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 所属的工厂Id
        /// </summary>
        public int FactoryId { get; set; }
        /// <summary>
        /// 功能组
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(FunctionGroup.ModuleId))]
        public List<FunctionGroup>? FunctionGroups { get; set; }

        public FunctionPage? Find( string id)
        {
            if (this.FunctionGroups != null)
            {
                foreach (var fg in this.FunctionGroups)
                {
                    if (fg.FunctionPages != null)
                    {
                        foreach (var fp in fg.FunctionPages)
                        {
                            if (fp.Id.ToString() == id)
                            {
                                return fp;
                            }
                        }
                    }

                }
            }
            return null;
        }
    }
}
