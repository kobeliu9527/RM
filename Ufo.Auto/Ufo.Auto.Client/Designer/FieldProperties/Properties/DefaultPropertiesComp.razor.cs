using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BootstrapBlazor.Components;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Globalization;

using MatBlazor;
using SqlSugar;
using System.Diagnostics.CodeAnalysis;
using Models;
using Ufo.Auto.Client.Extensions;

namespace Ufo.Auto.Client.Designer.FieldProperties.Properties
{
    public partial class DefaultPropertiesComp
    {
        [CascadingParameter(Name = "Root")]
        public FormDesigner FormDesigner { get; set; }
        [Parameter]
        public ComponentDto? ComponentData { get; set; }
        [NotNull]
        private ValidateForm? ValidateForm { get; set; }
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
        public void OnValueChanged(MouseEventArgs e)
        {

            //Color.Dark
        }
        public void OnFieldValueChanged(string key, object value)
        {//lstNames.GroupBy(n => n).Any(c => c.Count() > 1);
            if (key == "Id")
            {

                if (FormDesigner.ContainerData.FindAllComponent(x => true).GroupBy(x => x.Id).Any(x => x.Count() > 0))
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

            //if (FormDesigner.ContainerData.FindAllComponent(x => true).GroupBy(x => x.Id).Any(x => x.Count() > 1))
            //{
            //    ComponentData.Id = "";
            //}
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
    }
}
