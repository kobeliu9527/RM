using Easy.Shared.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easy.Shared.Models
{
    public class ComponentDto
    {
        #region 属性

        /// <summary>
        /// </summary>
        [DisplayName("页面唯一标识"), Required(ErrorMessage = "控件的名字,必须全局唯一")]
        public string? Id { get; set; }

        /// <summary>
        /// 父级容器的ID
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// 控件的类型,用于区分是文本框还是下拉框等等--
        /// </summary>
        public ComponentType ComponentType { get; set; }
        #endregion 属性
    }
}
