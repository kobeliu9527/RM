using SqlSugar;

namespace Models.SystemInfo
{
    /// <summary>
    /// 
    /// </summary>
    [SqlSugar.SugarTable("sys_"+nameof(Role))]
    public class Role : EntityBase, IEquatable<Role>, IComparable<Role>
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 这个角色里面所有的人员
        /// </summary>
        [Navigate(typeof(UserRole), nameof(UserRole.RoleId), nameof(UserRole.UserId))]
        public List<User>? Users { get; set; }

        public bool Equals(Role? other)
        {
            return other?.Name == Name;
        }

        public int CompareTo(Role? other)
        {
            return 1;
        }
    }
}
