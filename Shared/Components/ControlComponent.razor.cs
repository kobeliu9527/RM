using BootstrapBlazor.Components;
using Dm.parser;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.Dto;
using Shared.Extensions;
using Shared.Page;
using SQLitePCL;
using SqlSugar;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Console = System.Console;

namespace Shared.Components
{
    public partial class ControlComponent
    {
        [Inject]
        [NotNull]
        private MessageService? MessageService { get; set; }
        [Inject]
        [NotNull]
        private SwalService? SwalService { get; set; }
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }
        public Random Random { get; set; } = new Random();
        [Inject]
        [NotNull]
        public ISqlSugarClient? Db { get; set; }
        /// <summary>
        /// ������ҳ��
        /// </summary>
        [CascadingParameter(Name = "MainPage")]
        [NotNull]
        public MainPage? MainPage { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Parameter]
        [NotNull]
        public Control? Data { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        [Parameter]
        [NotNull]
        public Control? ParentData { get; set; }
        [NotNull]
        private DataTableDynamicContext? DataTablePageDynamicContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        // private DataTable UserData { get; set; } = new DataTable();
        // private DataTable PageDataTable { get; set; } = new();
        // [NotNull]
        // private List<SelectedItem>? PageItemsSource { get; set; } = [
        //    new("10", "10��/ҳ"),
        //    new("20", "20��/ҳ")
        //];


        /// <summary>
        /// ���ñ����ΪMainPage�б�ѡ�еĿؼ�,����ˢ�½���
        /// </summary>
        /// <returns></returns>
        private async Task SelectComponentAsync(MouseEventArgs e)
        {
            await MainPage.SetSelectedControl(Data);
        }
        /// <summary>
        /// ����һ��Control,����Control����Ϊҳ���е�SelectControl
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task SetSelectedAsync(Control e)
        {
            await MainPage.SetSelectedControl(e);
        }
        /// <summary>
        /// ����һ��Control,����Control����Ϊҳ���е�SelectControl
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private async Task SelectLeftComponentAsync(Control c)
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[0];
                await MainPage.StateHasChangedInvoke();
            }
        }
        private async Task SelectRightComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[1];
                await MainPage.StateHasChangedInvoke();
            }
        }
      
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Data.CtrType == WidgetType.Table)
                {
                    await InitDataTable();
                    StateHasChanged();
                    Data.UpdateSelf = InitDataTable;
                }
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        #region ���
        /// <summary>
        /// �ж������Ƿ����
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y)
        {
            return x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();
        }
        /// <summary>
        /// ���һ�к�ִ�еķ���
        /// <para>1.��¼��ǰ�е�ֵ </para>
        /// <para>2.�ҵ������ڱ�Table������Table,����ί��,ˢ������</para>
        /// <para>3.�ҵ���Table�󶨵���Ҫ���µı�Ԫ��,Ϊ�丳ֵ</para>
        /// <para>4.ˢ��������</para>
        /// </summary>
        /// <param name="row">����Ϣ</param>
        /// <returns></returns>
        public async Task OnClickedRow(DynamicObject row)
        {
            //1.�����ű������/KeyΪKey,��һ���е�Id�ֶε�ֵΪValue,�������ֵ���
            var tabName = Data.Key;
            foreach (var item in Data.TableInfo.TableFieldNames.Split(",",StringSplitOptions.RemoveEmptyEntries))
            {
                var v= row.GetValue(item);
                MainPage.TableValues[tabName].KeyValues[item] = v;
            }
            //2.�ҵ����ҳ�������е�Table�ؼ�����ta�ĸ��������������Row���ڱ�ı���
            List<Control> list = new List<Control>();
            var controllist = MainPage.FindAll(x => x.CtrType == WidgetType.Table && x.TableInfo.RequestParentTable == tabName);
            //3.�����ҵ��Ŀؼ�
            foreach (var control in controllist)
            {
                control.UpdateSelf?.Invoke();
            }
            foreach (var item in Data.TableInfo.FieldNameList)
            {
                var c = MainPage.FindFirst(item);
                if (c != null)
                {
                    var n = c.FieldName;
                    var val = row.GetValue(n);
                    c.Values.Value = val;
                }
            }
            await MainPage.StateHasChangedInvoke();
        }
        /// <summary>
        /// ��ʼ������Դ
        /// <para>1.�ҵ���Table�󶨵ĸ���Table</para>
        /// <para>2.�ҵ���Table�󶨵���Ҫ���µı�Ԫ��</para>
        /// </summary>
        /// <returns></returns>
        private async Task InitDataTable()
        {
            try
            {
                //�߼�:һ�ű�,���ܶ�Ӧһ�Ÿ�����,�����ڻ�ȡ���ݵ�ʱ��Ҫ���ж��Ƿ��и������Լ�������Id�Ƿ����
                var parentName = Data.TableInfo.RequestParentTable;
                DataTable? res = null;
                if (!string.IsNullOrEmpty(parentName) &&MainPage.TableValues.TryGetValue(parentName, out CurrentRowValue? crv))
                {
                    if (crv!=null)
                    {
                        var na = Data.TableInfo.RequestParentTableIdName;
                        if (crv.KeyValues.TryGetValue(na, out object? vl))
                        {
                            SugarParameter sp = new SugarParameter("PId", vl);
                            res = await Db.Ado.UseStoredProcedure().GetDataTableAsync(Data.TableInfo!.RequestAddress, sp);
                        }
                    }
                }
                else
                {
                    res = await Db.Ado.UseStoredProcedure().GetDataTableAsync(Data.TableInfo.RequestAddress);
                }
                if (res != null)
                {
                    Data.TableInfo.UserData = res;
                }
                else
                {
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "��ȡ����Ϊ��:",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Danger
                    });
                }
            }
            catch (Exception ex)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = ex.Message,
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Danger
                });
            }
            InitPageDataTable();
            //StateHasChanged();
        }
        /// <summary>
        /// ��ȡ��ҳ����,Ȼ�����RebuildPaginationDataTable���ɷ�ҳ������
        /// </summary>
        public void InitPageDataTable()
        {
            Data.TableInfo!.TotalCount = Data.TableInfo.UserData.Rows.Count;
            Data.TableInfo!.PageCount = (int)Math.Ceiling(Data.TableInfo!.TotalCount * 1.0 / Data.TableInfo!.PageItems);
            RebuildPaginationDataTable();
        }
        /// <summary>
        /// 1.���ݷ�ҳ������ȡ��ҳ���Table:PageDataTable 
        /// 2.��������DataTableDynamicContext
        /// </summary>
        public void RebuildPaginationDataTable()
        {
            Data.TableInfo.PageDataTable.Rows.Clear();
            Data.TableInfo.PageDataTable.AcceptChanges();
            Data.TableInfo.PageDataTable = GetPagedData(Data.TableInfo.UserData, Data.TableInfo!.PageIndex, Data.TableInfo!.PageItems);
            //�����ҳ
            DataTablePageDynamicContext = new DataTableDynamicContext(Data.TableInfo.PageDataTable, (context, col) =>
            {
                var propertyName = col.GetFieldName();
                var sss = col.GetDisplayName();
                var ss = context;
                var ccc = col;
                col.Width = 120;
                //if (propertyName == nameof(Foo.DateTime))
                //{
                //    context.AddRequiredAttribute(nameof(Foo.DateTime));
                //    // ʹ�� AutoGenerateColumnAttribute ������ʾ����ʾ��
                //    context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[] { new(nameof(AutoGenerateColumnAttribute.Text),
                //        nameof(Foo.DateTime)) });
                //}
                //else if (propertyName == nameof(Foo.Name))
                //{
                //    context.AddRequiredAttribute(nameof(Foo.Name), "NameRequired");
                //    // ʹ�� Text ������ʾ����ʾ��
                //    col.Text = nameof(Foo.Name);
                //}
                //else if (propertyName == nameof(Foo.Count))
                //{
                //    context.AddRequiredAttribute(nameof(Foo.Count));
                //    // ʹ�� DisplayNameAttribute ������ʾ����ʾ��
                //    context.AddDisplayNameAttribute(nameof(Foo.Count), nameof(Foo.Count));
                //}
                //else if (propertyName == nameof(Foo.Id))
                //{
                //    col.Editable = false;
                //    col.Visible = false;
                //}
            })
            {
                OnDeleteAsync = (e) =>
                {
                    return Task.FromResult(false);
                },
                OnAddAsync = (e) =>
                {
                    return Task.FromResult(true);
                },
                OnChanged = (e) =>
                {
                    return Task.FromResult(true);
                },
                OnValueChanged = (a, b, c) =>
                {
                    return Task.FromResult(true);
                }


            };
        }
        /// <summary>
        /// ���ҳ�봦����
        /// </summary>
        /// <param name = "pageIndex"></param>
        /// <returns></returns>
        private Task OnPageLinkClick(int pageIndex)
        {
            Data.TableInfo!.PageIndex = pageIndex;
            RebuildPaginationDataTable();
            StateHasChanged();
            return Task.CompletedTask;
        }
        public static DataTable GetPagedData(DataTable dataTable, int pageNumber, int pageSize)
        {
            DataTable pagedTable = dataTable.Clone();
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize;

            if (startIndex >= dataTable.Rows.Count)
            {
                return pagedTable;
            }

            if (endIndex > dataTable.Rows.Count)
            {
                endIndex = dataTable.Rows.Count;
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow row = dataTable.Rows[i];
                pagedTable.ImportRow(row);
            }

            return pagedTable;
        }
        #endregion
        #region ��ť
        public async void ButtonClick(MouseEventArgs e)
        {
            //todo:��ťӦ���и�����,���������ťӦ��ִ�е��������� ��������?��ӡ?...
            try
            {
                if (Data.Button != null && Data.Button.EnterEnAble)
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    var name = Data.Button.EnterStoreName;
                    var spp = Data.Button.EnterStoreParmeter;
                    var tableName = Data.Button.RequestParentTable;
                    var id = Data.Button.RequestParentTableIdName;
                    if (MainPage.IdList.TryGetValue("tableName", out object? valu))
                    {
                        parameters.Add(new SugarParameter(id, valu));
                    }

                    foreach (var item in Data.Button.EnterStoreParmeter)
                    {
                        var ctr = MainPage.FindFirst(item);
                        if (ctr!=null)
                        {
                            var pname = ctr.FieldName;
                            var pval = ctr.Values.Value;
                            parameters.Add(new SugarParameter(pname, pval));
                        }
                    }
                    if (!(string.IsNullOrEmpty(name)))
                    {
                        var tab = await Db.Ado.UseStoredProcedure().GetDataTableAsync(name, parameters);
                    }
                    else
                    {
                        await MessageService.Show(new MessageOption()
                        {
                            Content = $"δָ��Ҫִ�е�����",
                            Icon = "fa-solid fa-circle-info",
                            Color = Color.Danger
                        });
                        return;
                    }
                    //await ToastService.Success("ִ�гɹ�", $"�ҳɹ�ִ���˴洢����:{name}");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = $"�ҳɹ�ִ���˴洢����:{name}",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Success
                    });
                }
            }
            catch (Exception ex)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = $"�����쳣:{ex.Message}",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Danger
                });
            }
        }
        #endregion
        #region Tab
        public async Task OnClickTabItemAsync(TabItem e)
        {
            await SelectComponentAsync(new MouseEventArgs());
            //Data.Controls.ForEach(x=>x.IsActive=false);
            //var ss = Data.Controls.Find(x => x.Key == e.Text);
            //if (ss != null)
            //{
            //    ss.IsActive = true;
            //    await SelectComponentAsync(ss);
            //}
            await Task.Delay(100);
        }
        #endregion
        #region �ı���
        /// <summary>
        /// Enter��ִ�еĴ洢����
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task OnEnterAsyncInputText(string val)
        {
            if (Data.InputText != null && Data.InputText.EnterEnAble)
            {
                await ToastService.Success("ִ�гɹ�", $"�ҳɹ�ִ���˴洢����:{Data.InputText.EnterStoreName}");
            }
            await Task.Delay(100);
        }
        /// <summary>
        /// �˳���Ҫִ�е�
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task OnEscAsyncInputText(string val)
        {
            if (Data.InputText != null && Data.InputText.EscEnAble)
            {
                await ToastService.Success("ִ�гɹ�", $"�ҳɹ�ִ���˴洢����:{Data.InputText.EscStoreName}");
            }
            await Task.Delay(100);
        }

        #endregion
    }
}