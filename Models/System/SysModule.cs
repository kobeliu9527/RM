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
        /// 是否是系统预制,系统预制不可删除
        /// </summary>
        public bool IsSystem { get; set; }
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

    /// <summary>
    /// 表的权限
    /// </summary>
    public class TableRole: EntityBase
    {
        public string? TableName { get; set; }
        /// <summary>
        /// 具有添加权限的角色
        /// </summary>
        public List<Role>? InsertRole { get; set; }
        /// <summary>
        /// 具有删除权限的角色
        /// </summary>
        public List<Role>? DeleteRole { get; set; }
        /// <summary>
        /// 具有更新权限的角色
        /// </summary>
        public List<Role>? UpdateRole { get; set; }
        /// <summary>
        /// 具有查询权限的角色
        /// </summary>
        public List<Role>? SelectRole { get; set; }
    }
}
