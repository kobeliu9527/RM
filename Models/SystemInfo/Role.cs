﻿using SqlSugar;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Models.SystemInfo
{
    /// <summary>
    /// 
    /// </summary>
    [SqlSugar.SugarTable("sys_"+nameof(Role))]
    public class Role : EntityBase, IEquatable<Role>, IComparable<Role>,IEqualityComparer
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

        public bool Equals(Role? x, Role? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Role obj)
        {
            throw new NotImplementedException();
        }

        public new bool Equals(object? x, object? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class RoleEquality : IEqualityComparer<Role>
    {
        public bool Equals(Role? x, Role? y)
        {
            return x?.Name==y?.Name;
        }

        public int GetHashCode([DisallowNull] Role obj)
        {
            return 1;
        }
    }
}
