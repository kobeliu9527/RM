using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Shared.Page;

namespace Shared.Layout
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }
        [CascadingParameter(Name = "router")]
        public Models.System.SysModule? SysModule { get; set; }

        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }
        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new() { Text = "返回组件库", Icon = "fa-solid fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new() { Text = "Index", Icon = "fa-solid fa-fw fa-flag", Url = "/" , Match = NavLinkMatch.All},
                new() { Text = "Counter", Icon = "fa-solid fa-fw fa-check-square", Url = "/counter" },
                new() { Text = "Weather", Icon = "fa-solid fa-fw fa-database", Url = "/weather" },
                new() { Text = "Table", Icon = "fa-solid fa-fw fa-table", Url = "/table" },
                new() { Text = "run", Icon = "fa-solid fa-fw fa-table", Url = "/table" , Items=
                new  List<MenuItem>(){
                 new() { Text = "1", Icon = "fa-solid fa-fw fa-table", Url = "/runing/1" },
                new() { Text = "2", Icon = "fa-solid fa-fw fa-table", Url = "/runing/2" },
                new() { Text = "3", Icon = "fa-solid fa-fw fa-table", Url = "/runing/3" }
                }

                },

                new() { Text = "花名册", Icon = "fa-solid fa-fw fa-users", Url = "/users" }
            };

            return menus;
        }
        private async Task CloseButtonShow()
        {
            var option = new DialogOption()
            {
                Title = "Close the popup with DialogCloseButton",
                Component = BootstrapDynamicComponent.CreateComponent<Login>()
            };
            await DialogService.Show(option);
        }
        public async Task CloseDialogByCodeShow()
        {
            var option = new DialogOption()
            {
                Title = "Close the popup with code",
            };
            option.Component = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
            {
                [nameof(Button.Text)] = "Click to close the popup",
                [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, option.CloseDialogAsync)
            });
            await DialogService.Show(option);
        }
    }
}
