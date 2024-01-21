using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models.Dto;
using Shared.Components.Sys;
using Shared.Extensions;
using SqlSugar;

namespace Shared.Components
{
    public partial class ControlProperties
    {
        [Inject]
        [NotNull]
        private MessageService? MessageService { get; set; }
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }
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
        /// <summary>
        /// 当前页面所有的表单元素
        /// </summary>
        List<SelectedItem>? BoxListData;
        List<SelectedItem>? BoxListData2;
        /// <summary>
        /// todo:后续应该优化为全局刷新的时候在更新这个值,应该这个值经常用,但是不会经常变
        /// </summary>
        private List<SelectedItem> StoreItems { get; set; } = new();
        /// <summary>
        /// 获取存储过程列表
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<QueryData<SelectedItem>> QueryForStoreName(VirtualizeQueryOption option)
        {
            await Task.Delay(200);
            var storeList = Db.DbMaintenance.GetProcList("OrBitMOM");
            var sourse = storeList.Select(x => new SelectedItem() { Text = x, Value = x }).ToList();
            return new QueryData<SelectedItem> { Items = sourse, TotalCount = sourse.Count };
        }
        /// <summary>
        /// 更新参数控件列表
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task CollapseChangedForInputText(CollapseItem e)
        {
            if (e.Text == "事件数据源" && e.IsCollapsed == false)
            {
                BoxListData = new();
                MainPage.Controlmain.ToSelectedItemList(BoxListData);
                BoxListData2 = new();
                MainPage.Controlmain.ToSelectedItemList(BoxListData2);
                var storeList= Db.DbMaintenance.GetProcList("OrBitMOM");
                StoreItems = storeList.Select(x=>new SelectedItem(x,x)).ToList();
                await MainPage.StateHasChangedInvoke();
            }
        }
        /// <summary>
        /// 删除一个控件
        /// </summary>
        /// <param name="e"></param>
        public async Task DeleteControl(MouseEventArgs e)
        {
            if (Data != null)
            {
                if (Data.CtrType == WidgetType.FirstPanel || Data.CtrType == WidgetType.SecondPanel || Data.CtrType == WidgetType.TabItem)
                {//
                    //await ToastService.Warning("失败", "不能单独删除多面板(分割面板/多页面组件)中的某一个子面板,请在控件树中找到,在执行删除");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "不能单独删除多面板(分割面板/多页面组件)中的某一个子面板,请在控件树中找到,在执行删除",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Warning
                    });
                    return;
                }
                var res = MainPage.Controlmain.Remove(Data.Key);
                if (res)
                {
                    //await ToastService.Success("成功", "成功删除");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "删除成功",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Success
                    });
                }
                else
                {
                    //await ToastService.Warning("失败", "删除失败");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "删除失败",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Warning
                    });
                }
                await MainPage.StateHasChangedInvoke();
            }
        }
        /// <summary>
        /// 增加一个Tabitem页面
        /// </summary>
        /// <returns></returns>
        public async Task AddTabItem()
        {
            if (Data != null && Data.CtrType == WidgetType.Tab)
            {
                Data.Controls.Add(new Control() { CtrType = WidgetType.TabItem, DisplayName = "新增的" });
                
                //await ToastService.Success("成功", "成功添加");
                await MessageService.Show(new MessageOption()
                {
                    Content = "添加成功",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Success
                });
            }
            else
            {
                //await ToastService.Warning("失败", "添加失败");
                await MessageService.Show(new MessageOption()
                {
                    Content = "添加失败",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Success
                });

            }
            await MainPage.StateHasChangedInvoke();
        }
        #region 表格
        
        /// <summary>
        /// 获取所有的表格控件作为数据源
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<QueryData<SelectedItem>> OnQueryAsync(VirtualizeQueryOption option)
        {
            await Task.Delay(200);
            var items = MainPage.FindAll(x=>x.CtrType==  WidgetType.Table);
            items.Add(new Control() {DisplayName="无" });

            //if (!string.IsNullOrEmpty(option.SearchText))
            //{
            //    items = items.Where(i => i.Name!.Contains(option.SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            //}
            return new QueryData<SelectedItem>
            {
                Items = items.Skip(option.StartIndex).Take(option.Count).Select(i => new SelectedItem(i.Key!, i.DisplayName!)),
                TotalCount = items.Count
            };
        }

        /// <summary>
        /// 设置表格的列信息
        /// </summary>
        public void AddColumnForTable()
        {
            if (Data!=null)
            {
                List<SelectedItem>? Items=new();
                MainPage.Controlmain.ToSelectedItemList(Items);
                DialogService.ShowModal<UpdateTableColumn>(new ResultDialogOption()
                {
                    ComponentParameters = new Dictionary<string, object>
                    {
                        [nameof(UpdateTableColumn.Control)] = Data,
                        [nameof(UpdateTableColumn.Items)] = Items,
                    }
                });
            }
           
        }
        #endregion
    }
}