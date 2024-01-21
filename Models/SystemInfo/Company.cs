using SqlSugar;
using System.Text.Json.Serialization;

namespace Models.SystemInfo
{
    /// <summary>
    /// 公司
    /// </summary>
    [SqlSugar.SugarTable("sys_"+nameof(Company))]
    public class Company : EntityBase
    {
        /// <summary>
        /// 公司名字
        /// </summary>
        public string Name { get; set; } = "公司名字";
        /// <summary>
        /// 所有的工厂
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(Factory.CompanyId))]
        //[JsonIgnore]
        public List<Factory>? Factories { get; set; }
        /// <summary>
        /// 所属集团Id
        /// </summary>
        public long CompanyGroupId { get; set; }
    }
}
