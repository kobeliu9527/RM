using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
   
    public class ContainerDto
    {
        /// <summary>
        /// 容器的名字,必须全局唯一
        /// </summary>
        [DisplayName("唯一标识"), Required(ErrorMessage = "容器的Id,必须全局唯一"), ReadOnly(true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("名字"), Required(ErrorMessage = "容器的名字,最好全局唯一")]
        public string Name { get; set; } = "跟容器";
        /// <summary>
        /// 容器的类型
        /// </summary>
        public ContainerType ContainerType { get; set; }
        private List<RowDto> rows=new();
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

        }
        public ContainerDto(ContainerType type)
        {
            ContainerType = type;
            InitEmptyContainer(ref rows);
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

    /// <summary>
    /// 表示一行
    /// </summary>
    public class RowDto
    {
        /// <summary>
        /// 容器的名字,必须全局唯一
        /// </summary>
        [DisplayName("唯一标识"), Required(ErrorMessage = "行的Id,必须全局唯一"), ReadOnly(true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("名字"), Required(ErrorMessage = "行的名字,最好全局唯一")]
        public string Name { get; set; } = "行";
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// 这一行包含的所有的组件
        /// </summary>
        public List<ComponentDto> ComponentList { get; set; } =new();
        public RowDto()
        {
            ComponentList = new();
        }
    }
}
