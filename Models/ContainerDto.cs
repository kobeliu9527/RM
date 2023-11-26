using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 容器数据对象
    /// </summary>
    /// <remarks>
    ///<para>ID:Id标识</para>
    ///<para>Type:容器类型,枚举<see cref="Models.ContainerType"/></para>
    ///<para>Rows:表示所有的行的集合,<see cref="ComponentDto"/>表示一个控件,<see cref="list{ContainerDto}"/>表示一行控件,<see cref="list{list{ComponentDto}}"/>表示Rows集合</para>
    /// </remarks>
    public class ContainerDto
    {
        /// <summary>
        /// 容器的名字,必须全局唯一
        /// </summary>
        [DisplayName("唯一标识"), Required(ErrorMessage = "容器的名字,必须全局唯一")]
        public string Id { get; set; }

        /// <summary>
        /// 容器的类型
        /// </summary>
        public ContainerType ContainerType { get; set; }
        //private List<List<ComponentDto>> rows=new List<List<ComponentDto>>();
        private List<RowDto> rows = new();
        /// <summary>
        /// 控件的属性
        /// </summary>
        public Dictionary<string, Property> Props { get; set; } = new();
        /// <summary>
        /// 多语言
        /// </summary>
        public MutLanguage MutLanguage { get; set; } = new();
        #region Public Constructors
        /// <summary>
        /// 
        /// </summary>
        public ContainerDto()
        {
            //InitEmptyContainer(ref rows);
        }


        public ContainerDto(ContainerType type)
        {
            ContainerType = type;
            InitEmptyContainer(ref rows);
        }

        public ContainerDto(List<RowDto> rows)
        {
            Rows = rows;
        }

        #endregion Public Constructors

        #region 属性

        /// <summary>
        /// 容器中,所有行的集合
        /// </summary>
        //public List<List<ComponentDto>> Rows { get => rows; set => rows = value; }
        public List<RowDto> Rows { get => rows; set => rows = value; }



        #endregion 属性

        #region Internal 方法
        /// <summary>
        /// 初始化一个空容器
        /// </summary>
        /// <param name="rows"></param>
        internal void InitEmptyContainer(ref List<RowDto> rows)
        {
            //rows = new List<List<ComponentDto>>()
            //{
            //    new List<ComponentDto>()
            //};
            rows = new List<RowDto>() { new RowDto() };
        }

        #endregion Internal 方法

    }


    public class RowListDto
    {
        public List<RowDto> Rows { get; set; }
    }
    /// <summary>
    /// 表示一行
    /// </summary>
    public class RowDto
    {
        [DisplayName("唯一标识"), Required(ErrorMessage = "必须全局唯一")]
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<ComponentDto> ComponentDto { get; set; }=new ();
    }
}
