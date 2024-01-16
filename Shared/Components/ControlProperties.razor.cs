using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Models.Dto;
using Shared.Extensions;
using SqlSugar;

namespace Shared.Components
{
    public partial class ControlProperties
    {
        [Inject]
        [NotNull]
        public ISqlSugarClient? Db { get; set; }
        [Parameter]
        public Control? Data { get; set; }
        [Parameter]
        public Control? ParentData { get; set; }
        [CascadingParameter(Name = "MainPage")]
        [NotNull]
        public MainPage? MainPage { get; set; }
        public Size Size { get; set; }
        List<SelectedItem>? BoxListData;
        /// <summary>
        /// 获取存储过程列表
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<QueryData<SelectedItem>> QueryForStoreName(VirtualizeQueryOption option)
        {
            await Task.Delay(200);
            var storeList = Db.DbMaintenance.GetProcList("ManagementServer3");
            var sourse = storeList.Select(x => new SelectedItem() { Text = x, Value = x }).ToList();
            return new QueryData<SelectedItem> { Items = sourse, TotalCount = sourse.Count };
            // return new QueryData<SelectedItem>
            //     {
            //         Items = new List<SelectedItem>()
            //                 {
            //             new SelectedItem("北京", "北京"),
            //             new SelectedItem("上海", "上海") { Active = true },
            //         },
            //         TotalCount = 2
            //     };
        }
        public async Task CollapseChangedForInputText(CollapseItem e)
        {
            if (e.Text == "事件" && e.IsCollapsed == false)
            {
                BoxListData = new();
                MainPage.Controlmain.ToSelectedItemList(BoxListData);
                var ss = Data.InputText.EnterStoreParmeter;
                await MainPage.StateHasChangedInvoke();
            }
        }
    }
}