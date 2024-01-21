﻿using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.SystemInfo;
using Shared.Page;
using System.Collections.Generic;
using Models.NotEntity;
namespace Shared.Layout
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainLayout:IDisposable
    {
        
        [Inject]
        [NotNull]
        private IDispatchService<Msg>? DispatchService { get; set; }
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
        private async Task Notify(DispatchEntry<Msg> payload)
        {
            if (payload.Entry != null)
            {
                var option = new ToastOption()
                {
                    Category = ToastCategory.Information,
                    Title = "通知",
                    Content =payload.Entry.Message
                };
                await ToastService.Show(option);
            }
        }
        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            DispatchService.Subscribe(Notify);
            List<MenuItem> Root = new List<MenuItem>();
            var PageManger = new MenuItem("页面配置", "fas fa-house-crack");
            List<MenuItem> pgs = new List<MenuItem>();
            pgs.Add(new("页面管理", "/FunctionPageManger", "fas fa-boxes-stacked"));
            pgs.Add(new("功能组管理", "/FunctionGroupManger", "fas fa-school-circle-xmark"));
            pgs.Add(new("人员管理", "/UserManger", "fas fa-tent-arrow-turn-left"));
            pgs.Add(new("角色管理", "/RoleManger", "fas fa-warehouse"));
            PageManger.Items = pgs;
            Root.Add(PageManger);
            var PageManger2 = new MenuItem("设计器", "fas fa-cubes");
            List<MenuItem> pgs2 = new List<MenuItem>();
            pgs2.Add(new("普通界面设计", "/DesiginerPro", "fas fa-hill-avalanche"));
            pgs2.Add(new("流程图设计", "/WorkFlowDesigner", "fas fa-plant-wilt"));
            PageManger2.Items = pgs2;
            Root.Add(PageManger2);

            if (FunctionGroups != null)
            {
                //MenuItem list0 = new MenuItem();
                foreach (var item in FunctionGroups)
                {
                    var menu = new MenuItem() { Text = item.Name,Icon=item.Icon };
                    if (item.FunctionPages != null)
                    {
                        List<MenuItem> list = new List<MenuItem>();
                        foreach (var item2 in item.FunctionPages)
                        {
                            var menu2 = new MenuItem() { Text = item2.Name, Url = "/runing/" + item2.Id.ToString(),Icon=item2.Icon };
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

        public void Dispose()
        {
           DispatchService?.UnSubscribe(Notify);
        }
    }
}
