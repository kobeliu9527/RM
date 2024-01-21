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
    public class ControlComponentBase : ComponentBase
    {
        [Inject]
        [NotNull]
        public MessageService? MessageService { get; set; }
        [Inject]
        [NotNull]
        public SwalService? SwalService { get; set; }
        [Inject]
        [NotNull]
        public ToastService? ToastService { get; set; }

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
        public DataTableDynamicContext? DataTablePageDynamicContext { get; set; }

        public string SearchText { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        // public DataTable UserData { get; set; } = new DataTable();
        // public DataTable PageDataTable { get; set; } = new();
        // [NotNull]
        // public List<SelectedItem>? PageItemsSource { get; set; } = [
        //    new("10", "10条/页"),
        //    new("20", "20条/页")
        //];


        /// <summary>
        /// 设置本组件为MainPage中被选中的控件,并且刷新界面
        /// </summary>
        /// <returns></returns>
        public async Task SelectComponentAsync(MouseEventArgs e)
        {
            await MainPage.SetSelectedControl(Data);
        }
        /// <summary>
        /// 传入一个Control,将此Control设置为页面中的SelectControl
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task SetSelectedAsync(Control e)
        {
            await MainPage.SetSelectedControl(e);
        }
        /// <summary>
        /// 传入一个Control,将此Control设置为页面中的SelectControl
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task SelectLeftComponentAsync(Control c)
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[0];
                await MainPage.StateHasChangedInvoke();
            }
        }
        public async Task SelectRightComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (MainPage != null)
            {
                MainPage.SelectControl = Data.Controls[1];
                await MainPage.StateHasChangedInvoke();
            }
        }
        #region 生命周期函数
        protected override void OnInitialized()
        {
            //Console.WriteLine("组件开始:" + DateTime.Now);
            if (Data.CtrType == WidgetType.Table)
            {
                InitDataTable();

                StateHasChanged();
                Data.UpdateSelf = InitDataTable;
            }
            base.OnInitialized();

        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            //Console.WriteLine("组件结束:" + DateTime.Now);

        }
        #endregion

        #region 表格
        /// <summary>
        /// 判断两行是否相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool ModelEqualityComparer(IDynamicObject x, IDynamicObject y)
        {
            return x.GetValue("Id")?.ToString() == y.GetValue("Id")?.ToString();
        }
        /// <summary>
        /// 点击一行后执行的方法
        /// <para>1.记录当前行的值 </para>
        /// <para>2.找到依赖于本Table的其他Table,调用委托,刷新他们</para>
        /// <para>3.找到本Table绑定的需要更新的表单元素,为其赋值</para>
        /// <para>4.刷新主界面</para>
        /// </summary>
        /// <param name="row">行信息</param>
        /// <returns></returns>
        public async Task OnClickedRow(DynamicObject row)
        {
            //1.以这张表的名字/Key为Key,这一行中的Id字段的值为Value,保存在字典中
            var tabKey = Data.Key;
            foreach (var col in Data.TableInfo.TableFields)
            {
                var val = row.GetValue(col.FieldName);
                col.val = val;
                foreach (var ctrkey in col.FieldNameList)
                {
                    var ctr = MainPage.FindFirst(ctrkey);
                    if (ctr != null)
                    {
                        ctr.Values.Value = val;
                    }
                }
            }
            //foreach (var item in Data.TableInfo.TableFieldNames.Split(",", StringSplitOptions.RemoveEmptyEntries))
            //{
            //    var v = row.GetValue(item);
            //    if (MainPage.TableValues.ContainsKey(tabKey))
            //    {
            //        MainPage.TableValues[tabKey].KeyValues[item] = v;
            //    }
            //    else
            //    {
            //        CurrentRowValue crv = new CurrentRowValue();
            //        crv.KeyValues.Add(item, v);
            //        MainPage.TableValues.Add(tabKey, crv);
            //    }
            //    MainPage.TableValues[tabKey].KeyValues[item] = v;
            //}
            //2.找到这个页面中所有的Table控件并且ta的父级表名称是这个Row所在表的表名
            //List<Control> list = new List<Control>();
            var controllist = MainPage.FindAll(x => x.CtrType == WidgetType.Table && x.TableInfo.RequestParentTable == tabKey);
            //3.更新找到的控件
            foreach (var control in controllist)
            {
                control.UpdateSelf?.Invoke();
            }
            //foreach (var item in Data.TableInfo.FieldNameList)//找到跟这个表格绑定了的所有控件
            //{
            //    var c = MainPage.FindFirst(item);
            //    if (c != null)
            //    {
            //        var n = c.FieldName;
            //        var val = row.GetValue(n);
            //        c.Values.Value = val;
            //    }
            //}
            await MainPage.StateHasChangedInvoke();
        }
        /// <summary>
        /// 初始化数据源
        /// <para>1.找到本Table绑定的父级Table</para>
        /// <para>2.找到本Table绑定的需要更新的表单元素</para>
        /// </summary>
        /// <returns></returns>
        public async void  InitDataTable()
        {
            try
            {
                //逻辑:一张表,可能对应一张父级表,所以在获取数据的时候要先判断是否有父级表以及父级表Id是否存在
                var parentName = Data.TableInfo.RequestParentTable;
                List<SugarParameter> sps = new List<SugarParameter>();
                DataTable? res = null;
                if (!string.IsNullOrEmpty(parentName) && MainPage.TableValues.TryGetValue(parentName, out CurrentRowValue? crv))
                {
                    if (crv != null)
                    {
                        var na = Data.TableInfo.RequestParentTableIdName;
                        if (crv.KeyValues.TryGetValue(na, out object? vl))
                        {
                            SugarParameter sp = new SugarParameter("PId", vl);
                            sps.Add(sp);
                        }
                    }
                }
                if (Data.TableInfo.RequestParmeter.Count > 0)
                {
                    foreach (var item in Data.TableInfo.RequestParmeter)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var con = MainPage.FindFirst(item);
                            if (con != null)
                            {
                                var val = con.Values.Value;
                                var pname = con.FieldName;
                                if (val != null && !string.IsNullOrEmpty(pname))
                                {
                                    SugarParameter sp1 = new SugarParameter(pname, val);
                                    sps.Add(sp1);
                                }
                            }
                        }
                    }
                }
                res =  Db.Ado.UseStoredProcedure().GetDataTable(Data.TableInfo!.RequestAddress, sps);

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
            //StateHasChanged();
        }

        public void Search(string val)
        { 

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
                var res = Data.TableInfo.TableFields.FirstOrDefault(x => x.FieldName.ToLower() == propertyName.ToLower());
                if (res != null)
                {
                    col.Text = res.DisplayName;
                    col.Editable = res.Editable;
                    col.Visible = res.Visible;
                }
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
                OnDeleteAsync = async (e) =>
                {
                    
                    await MessageService.Show(new MessageOption()
                    {
                        Content = $"获取到返回值{Data.Id}",
                        Icon = "fa-solid fa-circle-info",
                        Color = Color.Danger
                    });
                    return (false);
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
        public Task OnPageLinkClick(int pageIndex)
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
                    var StoreName = Data.Button.EnterStoreName;
                    var StoreParmeter = Data.Button.EnterStoreParmeter;
                    var ParentTable = Data.Button.RequestParentTable;
                    var ParentTableFieldId = Data.Button.RequestParentTableIdName;
                    var pTable = MainPage.FindFirst(ParentTable);
                    if (pTable != null)
                    {
                        var Colnum = pTable.TableInfo.TableFields.FirstOrDefault(x=>x.FieldName.ToLower()==ParentTableFieldId.ToLower());
                        if (Colnum != null)
                        {
                            parameters.Add(new SugarParameter(ParentTableFieldId, Colnum.val));
                        }
                    }
                    foreach (var item in Data.Button.EnterStoreParmeter)
                    {
                        var ctr = MainPage.FindFirst(item);
                        if (ctr != null)
                        {
                            var pname = ctr.FieldName;
                            var pval = ctr.Values.Value;
                            parameters.Add(new SugarParameter(pname, pval, ctr.ParameterType == ParameterDirection.Output) { DbType=ctr.Values.DbType});
                        }
                    }
                    if (!(string.IsNullOrEmpty(StoreName)))
                    {
                        var tab =  Db.Ado.UseStoredProcedure().GetDataTable(StoreName, parameters);
                        foreach (var item in parameters)
                        {
                            if (item.Direction== ParameterDirection.Output)
                            {
                                await MessageService.Show(new MessageOption()
                                {
                                    Content = $"获取到返回值{item.Value}",
                                    Icon = "fa-solid fa-circle-info",
                                    Color = Color.Danger
                                });
                                return;
                            }
                        }
                        
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
                        Content = $"我成功执行了存储过程:{StoreName}",
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
        }

        #endregion
        #region CheckBox
        public async Task ValueChangedCheckBox(bool val)
        {
            if (Data.CheckBox != null && Data.CheckBox.ChangeEnAble)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = $"我成功执行了存储过程:{Data.CheckBox.ChangeStoreName}",
                    Icon = "fa-solid fa-circle-info",
                    Color = Color.Success
                });
            }
        }
        #endregion
    }
}
