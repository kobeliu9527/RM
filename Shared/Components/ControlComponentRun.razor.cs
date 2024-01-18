using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.Dto;
using Shared.Extensions;
using Shared.Page;
using SQLitePCL;
using SqlSugar;
using System.Data;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Console = System.Console;

namespace Shared.Components
{
    public partial class ControlComponentRun
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
        /// 所属根页面
        /// </summary>
        [CascadingParameter(Name = "MainPage")]
        [NotNull]
        public MainPage? MainPage { get; set; }
        /// <summary>
        /// 本身数据
        /// </summary>
        [Parameter]
        [NotNull]
        public Control? Data { get; set; }
        /// <summary>
        /// 父级容器数据
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
        //    new("10", "10条/页"),
        //    new("20", "20条/页")
        //];


        /// <summary>
        /// 设置本组件为MainPage中被选中的控件,并且刷新界面
        /// </summary>
        /// <returns></returns>
        private async Task SelectComponentAsync(MouseEventArgs e)
        {
            await MainPage.SetSelectedControl(Data);
        }
        /// <summary>
        /// 传入一个Control,将此Control设置为页面中的SelectControl
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task SetSelectedAsync(Control e)
        {
            await MainPage.SetSelectedControl(e);
        }
        /// <summary>
        /// 传入一个Control,将此Control设置为页面中的SelectControl
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
        protected async  override Task OnInitializedAsync()
        {
            
            await  base.OnInitializedAsync();
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
        #region 表格
        /// <summary>
        /// 判断两行是否相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y)
        {
            return x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();
        }
        /// <summary>
        /// 点击一行后执行的方法
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public async Task OnClickedRow(DynamicObject row)
        {
            //1.以这张表的名字/Key为Key,这一行中的Id字段的值为Value,保存在字典中
            var id = row.GetValue("Id");
            var tabName = Data.Key;
            //var tabName = Data.DisplayName;
            MainPage.IdList[tabName] = id;
            //2.找到这个页面中所有的Table控件并且ta的父级表名称是这个Row所在表的表名
            List<Control> list = new List<Control>();
            var controllist = MainPage.FindAll(x => x.CtrType == WidgetType.Table && x.TableInfo.RequestParentTable == tabName);
            //3.更新找到的表格控件
            foreach (var control in controllist)
            {
                control.UpdateSelf?.Invoke();
            }
            //一个表格,有一个对应字段表 ,表示这个表格绑定了页面中那些控件(key)
            //找到这些控件,就能知道这个控件的数据类型,也就知道了他的字段名,根据这个字段名,
            //调用row.GetValue(name)获取值object类型;
            //调用Data.Values.Value = id;设置值
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
            //var ss =Data.TableInfo

            await MainPage.StateHasChangedInvoke();
        }
        /// <summary>
        /// 初始化数据源
        /// </summary>
        /// <returns></returns>
        private async Task InitDataTable()
        {
            try
            {
                //逻辑:一张表,可能对应一张父级表,所以在获取数据的时候要先判断是否有父级表以及父级表Id是否存在
                var parentName = Data.TableInfo.RequestParentTable;
                DataTable? res = null;
                if (!string.IsNullOrEmpty(parentName) && MainPage.IdList.TryGetValue(parentName, out object? val))
                {
                    SugarParameter sp = new SugarParameter("PId", val);
                    res = await Db.Ado.UseStoredProcedure().GetDataTableAsync(Data.TableInfo!.RequestAddress, sp);
                }
                else
                {
                    res = await Db.Ado.UseStoredProcedure().GetDataTableAsync(Data.TableInfo!.RequestAddress);
                }
                if (res != null)
                {
                    Data.TableInfo.UserData = res;
                }
                else
                {
                    await MessageService.Show(new MessageOption()
                    {
                        Content = "获取数据为空:",
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
           
        }
        /// <summary>
        /// 获取分页参数,然后调用RebuildPaginationDataTable生成分页上下文
        /// </summary>
        public void InitPageDataTable()
        {
            Data.TableInfo!.TotalCount = Data.TableInfo.UserData.Rows.Count;
            Data.TableInfo!.PageCount = (int)Math.Ceiling(Data.TableInfo!.TotalCount * 1.0 / Data.TableInfo!.PageItems);
            RebuildPaginationDataTable();
        }
        /// <summary>
        /// 1.根据分页参数获取分页后的Table:PageDataTable 
        /// 2.重新生成DataTableDynamicContext
        /// </summary>
        public void RebuildPaginationDataTable()
        {
            Data.TableInfo.PageDataTable.Rows.Clear();
            Data.TableInfo.PageDataTable.AcceptChanges();
            Data.TableInfo.PageDataTable = GetPagedData(Data.TableInfo.UserData, Data.TableInfo!.PageIndex, Data.TableInfo!.PageItems);
            //单表分页
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
        #endregion
        #region 按钮
        public async void ButtonClick(MouseEventArgs e)
        {
            //todo:按钮应该有个属性,控制这个按钮应该执行的任务类型 请求数据?打印?...
            try
            {
                if (Data.Button != null && Data.Button.EnterEnAble)
                {
                    List<SugarParameter> parameters = new List<SugarParameter>();
                    var name = Data.Button.EnterStoreName;
                    var spp = Data.Button.EnterStoreParmeter;
                    foreach (var item in Data.Button.EnterStoreParmeter)
                    {
                        var ctr = MainPage.FindFirst(item);
                        if (ctr != null)
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
                            Content = $"未指定要执行的任务",
                            Icon = "fa-solid fa-circle-info",
                            Color = Color.Danger
                        });
                        return;
                    }
                    //await ToastService.Success("执行成功", $"我成功执行了存储过程:{name}");
                    await MessageService.Show(new MessageOption()
                    {
                        Content = $"我成功执行了存储过程:{name}",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Success
                    });
                }
            }
            catch (Exception ex)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = $"出现异常:{ex.Message}",
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
        #region 文本框
        /// <summary>
        /// Enter键执行的存储过程
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task OnEnterAsyncInputText(string val)
        {
            if (Data.InputText != null && Data.InputText.EnterEnAble)
            {
                await ToastService.Success("执行成功", $"我成功执行了存储过程:{Data.InputText.EnterStoreName}");
            }
            await Task.Delay(100);
        }
        /// <summary>
        /// 退出键要执行的
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task OnEscAsyncInputText(string val)
        {
            if (Data.InputText != null && Data.InputText.EscEnAble)
            {
                await ToastService.Success("执行成功", $"我成功执行了存储过程:{Data.InputText.EscStoreName}");
            }
            await Task.Delay(100);
        }

        #endregion
    }
}