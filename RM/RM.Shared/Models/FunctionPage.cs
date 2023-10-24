using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RM.Shared.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionPage: EntityBase
    {


        [SqlSugar.SugarColumn(IsJson =true, ColumnDataType = "nvarchar(max)")]
        public ContainerDto? ContainerData { get; set; }
    }
    /// <summary>
    /// 只有主键的基类
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
    }
}
