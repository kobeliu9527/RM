using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Shared.Designer;
using Shared.Extensions;
using SqlSugar;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Designer.FieldProperties.Properties
{
    public partial class TabsProperties
    {
        [NotNull]
        private Modal? Modal { get; set; }

        [CascadingParameter(Name = "Root")]
        public FormDesigner? FormDesigner { get; set; }

        [Parameter]
        public ComponentDto? ComponentData { get; set; }

        [NotNull]
        private ValidateForm? ValidateForm { get; set; }
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }

        //[Inject]
        //[NotNull]
        //private ISqlSugarClient? db { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnLabelChanged { get; set; }
        [Parameter]
        public EventCallback<ChangeEventArgs> OnWidthChanged { get; set; }
        [Parameter]
        public EventCallback<ChangeEventArgs> OnHeightChanged { get; set; }
        [Parameter]
        public EventCallback<PInof> OnPropChanged { get; set; }
        /// <summary>
        /// Primary / Secondary / Success / Danger / Warning / Info / Dark
        /// </summary>
        List<SelectedItem> ColorItems = new List<SelectedItem>() {
                new SelectedItem("None", "None"),
                new SelectedItem("Active", "Active"),
                new SelectedItem("Primary", "Primary"),
                new SelectedItem("Warning", "Warning"),
                new SelectedItem("Info", "Info"),
                new SelectedItem("Danger", "Danger"),
                new SelectedItem("Success", "Success"),
                new SelectedItem("Dark", "Dark")
                };
        List<SelectedItem> BoolItems = new List<SelectedItem>() {
                new SelectedItem("True", "True"),
                new SelectedItem("False", "False"),

                };
        private SelectedItem? VirtualItem1 { get; set; }
        private async Task<QueryData<SelectedItem>> OnQueryAsync(VirtualizeQueryOption option)
        {
            await Task.Delay(200);
            return new QueryData<SelectedItem>
            {
                Items = new List<SelectedItem>()
                        {
                    new SelectedItem("北京", "北京"),
                    new SelectedItem("上海", "上海") { Active = true },
                },
                TotalCount = 2
            };
        }
        public List<SelectedItem> Parameters { get; set; } = new List<SelectedItem>() { new SelectedItem() { Value = "1", Text = "22" }, new SelectedItem() { Value = "2", Text = "22" } };
        protected override Task OnInitializedAsync()
        {
            Parameters = FormDesigner.FunctionPage.ContainerData.FindAllComponent().Select(x => new SelectedItem() { Text = "   " + x.Id, Value = x.Id }).ToList();
            return base.OnInitializedAsync();
        }
        private IEnumerable<SelectedItem> GetParameters()
        {
            return FormDesigner.FunctionPage.ContainerData.FindAllComponent().Select(x => new SelectedItem() { Text = "   " + x.Id, Value = x.Id });

        }
        private IEnumerable<SelectedItem> GetProList()
        {
            return new List<SelectedItem> { };
            // return db.DbMaintenance.GetProcList("LCM").Select(x => new SelectedItem() { Text = x, Value = x });
        }
        public void OnValueChanged(MouseEventArgs e)
        {

            //Color.Dark
        }
        public void OnFieldValueChanged(string key, object value)
        {//lstNames.GroupBy(n => n).Any(c => c.Count() > 1);
            if (key == "Id")
            {

                if (FormDesigner.FunctionPage.ContainerData.FindAllComponent(x => true).GroupBy(x => x.Id).Any(x => x.Count() > 0))
                {
                    ValidateForm.SetError("Id", "数据库中已存在");
                }

            }
            //return Task.CompletedTask;
        }
        private Task OnValidComplexModel(EditContext context)
        {
            ValidateForm.SetError("Id", "数据库中已存在");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 当设置控件的Id属性的时候
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Task IdOnValueChanged(string v)
        {

            if (FormDesigner.FunctionPage.ContainerData.FindAllComponent(x => true).GroupBy(x => x.Id).Any(x => x.Count() > 1))
            {
                ComponentData.Id = "";
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 当设置控件的Id属性的时候
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task OnValueChangedForBool(bool v)
        {
            await FormDesigner.StateHasChangedAsync();
        }
        /// <summary>
        /// 当设置控件外观颜色的时候
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task OnValueChangedForColor(Color v)
        {
            await FormDesigner.StateHasChangedAsync();
        }
        /// <summary>
        /// 当设置控件外观颜色的时候
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task OnValueChangedForInt(int v)
        {
            await FormDesigner.StateHasChangedAsync();
        }
        /// <summary>
        /// 当设置控件外观颜色的时候
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task OnValueChangedForString(string v)
        {
            await FormDesigner.StateHasChangedAsync();
        }
    }
}