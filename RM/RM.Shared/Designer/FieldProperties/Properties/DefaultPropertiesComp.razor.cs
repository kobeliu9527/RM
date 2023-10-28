using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BootstrapBlazor.Components;
using RM.Shared.Data;
using RM.Shared.Shared;
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
using RM.Shared.Models;
using RM.Shared.Core;
using RM.Shared.Extensions;
using RM.Shared.Designer.FieldProperties;
using RM.Shared.Designer.Palette;
using RM.Shared.Designer.Whiteboard;
using MatBlazor;
using SqlSugar;

namespace RM.Shared.Designer.FieldProperties.Properties
{
    public partial class DefaultPropertiesComp
    {
        [Parameter]
        public ComponentDto? ComponentData { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnLabelChanged { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnWidthChanged { get; set; }
        [Parameter]
        public EventCallback<ChangeEventArgs> OnHeightChanged { get; set; }

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
        public void sa()
        {
            
           //Color.Dark
        }
    }
}
