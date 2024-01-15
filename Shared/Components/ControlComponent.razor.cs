using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models.Dto;
using Shared.Page;
using SqlSugar;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Components
{
    public partial class ControlComponent
    {
        public Random Random { get; set; } = new Random();
        [Inject]
        [NotNull]
        public ISqlSugarClient? Db { get; set; }
        [CascadingParameter(Name = "MainPage")]
        [NotNull]
        public MainPage? MainPage { get; set; }
        [Parameter]
        [NotNull]
        public Control? Data { get; set; }
        [Parameter]
        [NotNull]
        public Control? ParentData { get; set; }
        [NotNull]
        private DataTableDynamicContext? DataTablePageDynamicContext { get; set; }
        private DataTable UserData { get; set; } = new DataTable();
        private DataTable PageDataTable { get; set; } = new();
        [NotNull]
        private List<SelectedItem>? PageItemsSource { get; set; } = [
            new("2", "2��/ҳ"),
            new("4", "4��/ҳ"),
            new("10", "10��/ҳ"),
            new("20", "20��/ҳ")
        ];

        private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y) => x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();
        /// <summary>
        /// ���ñ����ΪMainPage�б�ѡ�еĿؼ�,����ˢ�½���
        /// </summary>
        /// <returns></returns>
        private async Task SelectComponentAsync(MouseEventArgs e)
        {
            await MainPage.SetSelectControlByDesignerAsync(Data);
        }
        /// <summary>
        /// ���ñ����ΪMainPage�б�ѡ�еĿؼ�,����ˢ�½���
        /// </summary>
        /// <returns></returns>
        private async Task SelectComponentAsync(Control e)
        {
            await MainPage.SetSelectControlByDesignerAsync(e);
        }
        private void SelectLeftComponentAsync(Control c)
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[0];
                MainPage.StateHasChangedInvoke();
            }
        }
        private void SelectRightComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[1];
                MainPage.StateHasChangedInvoke();
            }
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Data.CtrType == WidgetType.Table)
            {
                InitDataTable();
                ;
            }
        }
        private void InitDataTable()
        {
            UserData = Db.Ado.UseStoredProcedure().GetDataTable(Data.TableInfo!.DataSourse);
            InitPageDataTable();
        }
        private void InitPageDataTable()
        {
            Data.TableInfo!.TotalCount = UserData.Rows.Count;
            Data.TableInfo!.PageCount = (int)Math.Ceiling(Data.TableInfo!.TotalCount * 1.0 / Data.TableInfo!.PageItems);
            RebuildPageDataTable();
            RebuildPaginationDataTable();
        }
        private void RebuildPageDataTable()
        {
            PageDataTable.Rows.Clear();
            // �˴��������ͨ�����ݿ��÷�ҳ�������ת���� DataTable �ٸ� DynamicContext ����ʵ�����ݿ��ҳ
            //foreach (var f in PageFoos.Skip((PageIndex - 1) * PageItems).Take(PageItems).ToList())
            //{
            //    PageDataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
            //}
            PageDataTable = GetPagedData(UserData, Data.TableInfo!.PageIndex, Data.TableInfo!.PageItems);
            PageDataTable.AcceptChanges();
        }
        private void RebuildPaginationDataTable()
        {
            PageDataTable.Rows.Clear();
            // �˴��������ͨ�����ݿ��÷�ҳ�������ת���� DataTable �ٸ� DynamicContext ����ʵ�����ݿ��ҳ
            //foreach (var f in PageFoos.Skip((PageIndex - 1) * PageItems).Take(PageItems).ToList())
            //{
            //    PageDataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
            //}
            PageDataTable.AcceptChanges();
            PageDataTable = GetPagedData(UserData, Data.TableInfo!.PageIndex, Data.TableInfo!.PageItems);

            //�����ҳ
            // var page = db.Queryable<VendorDelivery>().ToPageList(pagenumber, pageSize, ref totalCount);
            DataTablePageDynamicContext = new DataTableDynamicContext(PageDataTable, (context, col) =>
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
        #region ��ť
        public async void ButtonClick(MouseEventArgs e)
        {
            try
            {
                List<SugarParameter> parameters = new List<SugarParameter>();
                var name = Data.DataSourse.StoreName;
                var p = Data.DataSourse.StoreName.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (!(string.IsNullOrEmpty(name)))
                {
                    if (p != null)
                    {
                        foreach (var item in p)
                        {
                            Control? ctr = MainPage.FindFirst(item);
                            if (ctr != null)
                            {
                                var sp = new SugarParameter(item, ctr.Values.Value) { DbType = ctr.Values.DbType };
                                parameters.Add(sp);
                            }
                        }
                    }
                    var tab = await Db.Ado.UseStoredProcedure().GetDataTableAsync(Data.DataSourse?.StoreName, parameters);
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Tab
        public async Task OnClickTabItemAsync(TabItem e)
        {
            Data.Controls.ForEach(x=>x.IsActive=false);
            var ss = Data.Controls.Find(x => x.Key == e.Text);
            if (ss != null)
            {
                ss.IsActive = true;
                await SelectComponentAsync(ss);
            }

        }
        #endregion
    }
}