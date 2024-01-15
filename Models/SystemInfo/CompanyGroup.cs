using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SystemInfo
{
    /// <summary>
    /// 集团 架构顶层
    /// </summary>
    public class CompanyGroup : EntityBase
    {
        /// <summary>
        /// 集团名字
        /// </summary>
        public string Name { get; set; } = "集团公司";
        [Navigate(NavigateType.OneToMany, nameof(Company.CompanyGroupId))]
        public List<Company>? Companies { get; set; }
    }
}
