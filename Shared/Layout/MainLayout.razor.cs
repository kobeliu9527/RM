using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.SystemInfo;
using Shared.Page;
using System.Collections.Generic;

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

        private bool IsFixedFooter { get; set; }

        private bool IsFullSide { get; set; }

        private bool ShowFooter { get; set; }

        private List<MenuItem>? Menus { get; set; }

        [CascadingParameter(Name = "router")]
        public List<FunctionGroup>? FunctionGroups { get; set; }

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
            List<MenuItem> Root = new List<MenuItem>();
            var PageManger = new MenuItem("页面配置");
            List<MenuItem> pgs = new List<MenuItem>();
            pgs.Add(new("页面管理", "/FunctionPageManger"));
            pgs.Add(new("功能组管理", "/FunctionGroupManger"));
            pgs.Add(new("人员管理", "/UserManger"));
            pgs.Add(new("角色管理", "/RoleManger"));
            PageManger.Items = pgs;
            Root.Add(PageManger);
            var PageManger2 = new MenuItem("设计器");
            List<MenuItem> pgs2 = new List<MenuItem>();
            pgs2.Add(new("普通界面设计", "/DesiginerPro"));
            pgs2.Add(new("流程图设计", "/WorkFlowDesigner"));
            PageManger2.Items = pgs2;
            Root.Add(PageManger2);

            if (FunctionGroups != null)
            {
                //MenuItem list0 = new MenuItem();
                foreach (var item in FunctionGroups)
                {
                    var menu = new MenuItem() { Text = item.Name };
                    if (item.FunctionPages != null)
                    {
                        List<MenuItem> list = new List<MenuItem>();
                        foreach (var item2 in item.FunctionPages)
                        {
                            var menu2 = new MenuItem() { Text = item2.Name, Url = "/runing/" + item2.Id.ToString() };
                            list.Add(menu2);
                        }
                        menu.Items = list;
                    }
                    Root.Add(menu);
                }
            }
            Menus = Root;
            base.OnInitialized();

        }
        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                //List<MenuItem> list = new List<MenuItem>();

                //if (FunctionPages != null)
                //{
                //    foreach (var item in FunctionPages)
                //    {
                //        list.Add(new MenuItem() { Text = item.Name, Url = item.Id.ToString() });
                //    }
                //}
                //Menus = list;
            }

            base.OnAfterRender(firstRender);
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
                new() { Text = "流程设计", Icon = "fa-solid fa-fw fa-flag", Url = "WorkFlowDesigner" , Match = NavLinkMatch.All},
                new() { Text = "设计程序Pro", Icon = "fa-solid fa-fw fa-flag", Url = "DesiginerPro" , Match = NavLinkMatch.All},
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
