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

                new() { Text = "首页", Icon = "fa-solid fa-fw fa-flag", Url = "/" , Match = NavLinkMatch.All},
                new() { Text = "设计器", Icon = "fa-solid fa-fw fa-check-square", Url = "/counter" },

                new() { Text = "Table", Icon = "fa-solid fa-fw fa-table", Url = "/table" },
                new() { Text = "运行界面", Icon = "fa-solid fa-fw fa-table", Url = "/table" , Items=
                new  List<MenuItem>(){
                new() { Text = "示例1", Icon = "fa-solid fa-fw fa-table", Url = "/runing/1" },
                new() { Text = "示例2", Icon = "fa-solid fa-fw fa-table", Url = "/runing/2" },
                new() { Text = "示例3", Icon = "fa-solid fa-fw fa-table", Url = "/runing/3" }
                }

                },
                new() { Text = "流程设计", Icon = "fa-solid fa-fw fa-flag", Url = "WorkFlowTemplate" , Match = NavLinkMatch.All},
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
