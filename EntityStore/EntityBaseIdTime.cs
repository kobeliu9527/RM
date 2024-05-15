using SqlSugar;
using System.ComponentModel;

namespace EntityStore
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class EntityBase { }
    /// <summary>
    /// 具有主键和创建,修改时间的类
    /// </summary>
    public abstract class EntityBaseIdTime: EntityBase
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(CreateTableFieldSort = 100)]
        public DateTime CreateTime { get; set; }= DateTime.Now;
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [SugarColumn(CreateTableFieldSort = 101)]
        public DateTime? ModifyTime { get; set; }=DateTime.Now; 
    }
}
//索引占位符 普通表：{table}  分表：{split_table} 数据库{db} 分表{split_table}
