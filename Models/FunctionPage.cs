using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionPage : EntityBase
    {
        /// <summary>
        /// 表示这个页面的布局
        /// </summary>
        [SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "nvarchar(max)")]
        [NotNull]
        public ContainerDto? ContainerData { get; set; }
    }
}
