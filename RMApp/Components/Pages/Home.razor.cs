using Models.SystemInfo;

namespace RMApp.Components.Pages
{
    public partial class Home
    {
        public bool createResult { get; set; }
        public string TableName { get; set; } = "";
        public List<User> userlist { get; set; }
        public void Create()
        {
            createResult = db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(typeof(User));
            db.CodeFirst.InitTables(typeof(Role));
            db.CodeFirst.InitTables(typeof(UserRole));
        }
        public void Get()
        {
            var res = db.DbMaintenance.GetTableInfoList();
            foreach (var item in res)
            {
                TableName += item.Name + "|";
            }
        }
        public void Insert()
        {
            User user = new User() { RealName=DateTime.Now.ToString(),Icon=""};
            db.Insertable<User>(user).ExecuteReturnSnowflakeId();
        }
        public void Select()
        {
            userlist = db.Queryable<User>().ToList();
           
        }
    }
}