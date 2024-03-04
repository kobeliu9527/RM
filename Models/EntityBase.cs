using SqlSugar;
using System.ComponentModel;

namespace Models
{
    /// <summary>
    /// 只有主键的基类
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标"),Description("系统采用知名的fontawesome图标,网址:https://fontawesome.com.cn/v5"),SugarColumn( IsNullable =true )]
        public string? Icon { get; set; }
    }
}
