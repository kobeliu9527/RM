namespace RM.Shared.Models
{
    /// <summary>
    /// 容器数据对象
    /// </summary>
    /// <remarks>
    ///<para>ID:Id标识</para>
    ///<para>Type:容器类型,枚举<see cref="ContainerType"/></para>
    ///<para>Rows:表示所有的行的集合,<see cref="ComponentDto"/>表示一个控件,<see cref="list{ComponentDto}"/>表示一行控件,<see cref="list{list{ComponentDto}}"/>表示Rows集合</para>
    /// </remarks>
    public class ContainerDto 
    {

        public Guid Id { get; private set; }
        public ContainerType Type { get; private set; }
        private List<List<ComponentDto>> rows=new List<List<ComponentDto>>();
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

        //public ContainerDto(Guid id) : base(id)
        //{
        //    InitEmptyContainer(ref rows);
        //}

        //public ContainerDto(Guid id, List<List<ComponentDto>> rows) : base(id)
        //{
        //    Rows = rows;
        //}

        //public ContainerDto(Guid id, ContainerType type) : base(id, type)
        //{
        //    InitEmptyContainer(ref rows);
        //}

        //public ContainerDto(Guid id, ContainerType type, List<List<ComponentDto>> rows)
        //    : base(id, type)
        //{
        //    Rows = rows;
        //}

        public ContainerDto(ContainerType type) 
        {
            Type = type;
            InitEmptyContainer(ref rows);
        }

        public ContainerDto(List<List<ComponentDto>> rows)
        {
            Rows = rows;
        }

        #endregion Public Constructors

        #region 属性

        /// <summary>
        /// 容器中,所有行的集合
        /// </summary>
        public List<List<ComponentDto>> Rows { get => rows; set => rows = value; }

        public string? Label { get; set; }

        #endregion 属性

        #region Internal 方法
        /// <summary>
        /// 初始化一个空容器
        /// </summary>
        /// <param name="rows"></param>
        internal void InitEmptyContainer(ref List<List<ComponentDto>> rows)
        {
            rows = new List<List<ComponentDto>>()
            {
                new List<ComponentDto>()
            };
        }

        #endregion Internal 方法

    }
}
