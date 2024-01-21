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
        /// ��ǰҳ�����еı�Ԫ��
        /// </summary>
        List<SelectedItem>? BoxListData;
        List<SelectedItem>? BoxListData2;
        /// <summary>
        /// todo:����Ӧ���Ż�Ϊȫ��ˢ�µ�ʱ���ڸ������ֵ,Ӧ�����ֵ������,���ǲ��ᾭ����
        /// </summary>
        private List<SelectedItem> StoreItems { get; set; } = new();
        /// <summary>
        /// ��ȡ�洢�����б�
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
        /// ���²����ؼ��б�
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task CollapseChangedForInputText(CollapseItem e)
        {
            if (e.Text == "�¼�����Դ" && e.IsCollapsed == false)
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
        /// ɾ��һ���ؼ�
        /// </summary>
        /// <param name="e"></param>
        public async Task DeleteControl(MouseEventArgs e)
        {
            if (Data != null)
            {
                if (Data.CtrType == WidgetType.FirstPanel || Data.CtrType == WidgetType.SecondPanel || Data.CtrType == WidgetType.TabItem)
                {//
                    //await ToastService.Warning("ʧ��", "���ܵ���ɾ�������(�ָ����/��ҳ�����)�е�ĳһ�������,���ڿؼ������ҵ�,��ִ��ɾ��");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "���ܵ���ɾ�������(�ָ����/��ҳ�����)�е�ĳһ�������,���ڿؼ������ҵ�,��ִ��ɾ��",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Warning
                    });
                    return;
                }
                var res = MainPage.Controlmain.Remove(Data.Key);
                if (res)
                {
                    //await ToastService.Success("�ɹ�", "�ɹ�ɾ��");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "ɾ���ɹ�",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Success
                    });
                }
                else
                {
                    //await ToastService.Warning("ʧ��", "ɾ��ʧ��");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "ɾ��ʧ��",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Warning
                    });
                }
                await MainPage.StateHasChangedInvoke();
            }
        }
        /// <summary>
        /// ����һ��Tabitemҳ��
        /// </summary>
        /// <returns></returns>
        public async Task AddTabItem()
        {
            if (Data != null && Data.CtrType == WidgetType.Tab)
            {
                Data.Controls.Add(new Control() { CtrType = WidgetType.TabItem, DisplayName = "������" });
                
                //await ToastService.Success("�ɹ�", "�ɹ����");
                await MessageService.Show(new MessageOption()
                {
                    Content = "��ӳɹ�",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Success
                });
            }
            else
            {
                //await ToastService.Warning("ʧ��", "���ʧ��");
                await MessageService.Show(new MessageOption()
                {
                    Content = "���ʧ��",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Success
                });

            }
            await MainPage.StateHasChangedInvoke();
        }
        #region ���
        
        /// <summary>
        /// ��ȡ���еı��ؼ���Ϊ����Դ
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private async Task<QueryData<SelectedItem>> OnQueryAsync(VirtualizeQueryOption option)
        {
            await Task.Delay(200);
            var items = MainPage.FindAll(x=>x.CtrType==  WidgetType.Table);
            items.Add(new Control() {DisplayName="��" });

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
        /// ���ñ�������Ϣ
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