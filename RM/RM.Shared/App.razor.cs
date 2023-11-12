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
using System.Diagnostics.CodeAnalysis;
using RM.Shared.Models.System;

namespace RM.Shared
{
    public partial class App
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public LoginDto Login { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
