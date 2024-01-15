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
            new("2", "2条/页"),
            new("4", "4条/页"),
            new("10", "10条/页"),
            new("20", "20条/页")
        ];

        private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y) => x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();
        /// <summary>
        /// 设置本组件为MainPage中被选中的控件,并且刷新界面
        /// </summary>
        /// <returns></returns>
        private async Task SelectComponentAsync(MouseEventArgs e)
        {
            await MainPage.SetSelectControlByDesignerAsync(Data);
        }
        /// <summary>
        /// 设置本组件为MainPage中被选中的控件,并且刷新界面
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
            // 此处代码可以通过数据库获得分页后的数据转化成 DataTable 再给 DynamicContext 即可实现数据库分页
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
            // 此处代码可以通过数据库获得分页后的数据转化成 DataTable 再给 DynamicContext 即可实现数据库分页
            //foreach (var f in PageFoos.Skip((PageIndex - 1) * PageItems).Take(PageItems).ToList())
            //{
            //    PageDataTable.Rows.Add(f.Id, f.DateTime, f.Name, f.Count);
            //}
            PageDataTable.AcceptChanges();
            PageDataTable = GetPagedData(UserData, Data.TableInfo!.PageIndex, Data.TableInfo!.PageItems);

            //单表分页
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
                //    // 使用 AutoGenerateColumnAttribute 设置显示名称示例
                //    context.AddAutoGenerateColumnAttribute(nameof(Foo.DateTime), new KeyValuePair<string, object?>[] { new(nameof(AutoGenerateColumnAttribute.Text),
                //        nameof(Foo.DateTime)) });
                //}
                //else if (propertyName == nameof(Foo.Name))
                //{
                //    context.AddRequiredAttribute(nameof(Foo.Name), "NameRequired");
                //    // 使用 Text 设置显示名称示例
                //    col.Text = nameof(Foo.Name);
                //}
                //else if (propertyName == nameof(Foo.Count))
                //{
                //    context.AddRequiredAttribute(nameof(Foo.Count));
                //    // 使用 DisplayNameAttribute 设置显示名称示例
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
        /// 点击页码处理函数
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
        #region 按钮
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