using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Models.SystemInfo;
using System.Diagnostics.CodeAnalysis;
using Models.Dto;
using System.Security.Claims;

namespace RMApp.Components.Pages
{
    public partial class Home
    {
        [Inject, NotNull]
        private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        public bool createResult { get; set; }
        public string TableName { get; set; } = "";
        public List<User>? userlist { get; set; }
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
            User user = new User() { RealName=DateTime.Now.ToString()};
            db.Insertable<User>(user).ExecuteReturnSnowflakeId();
        }
        public void Select()
        {
            userlist = db.Queryable<User>().ToList();
        }

        public async Task Delete()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState?.User;
            if (user?.Identity is not null && user.Identity.IsAuthenticated)
            {
                var modname = user.Claims.FirstOrDefault(x => x.Type.ToLower() == "modulename")?.Value ?? "";
            }
        }
    }
}