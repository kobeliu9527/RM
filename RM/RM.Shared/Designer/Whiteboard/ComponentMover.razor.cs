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

namespace RM.Shared.Designer.Whiteboard
{
    /// <summary>
    /// 功能组件:左右移动组件
    /// </summary>
    public partial class ComponentMover
    {
        /// <summary>
        /// 
        /// </summary>
        [CascadingParameter(Name = "Root")]
        [NotNull]
        public FormDesigner? Root { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CascadingParameter(Name = "Container")]
        public Container? Container { get; set; }

        /// <summary>
        /// 要控制的这个组件所在的行
        /// </summary>
        [Parameter]
        public List<ComponentDto> ComponentsInParentRow { get; set; }
        /// <summary>
        /// 要控制的组件的数据
        /// </summary>
        [Parameter]
        public ComponentDto? Component { get; set; }

#region Move Left Button
        private bool IsMoveLeftVisible()
        {
            return ComponentsInParentRow.IsMoveLeftPossible(Component);
        }

        private async Task MoveLeftAsync()
        {
            ComponentsInParentRow?.MoveLeft(Component);
            await Root.StateHasChangedAsync();
        }

#endregion Move Left Button
#region Move Right Button
        private bool IsMoveRightVisible()
        {
            return ComponentsInParentRow.IsMoveRightPossible(Component);
        }

        private async Task MoveRightAsync()
        {
            ComponentsInParentRow?.MoveRight(Component);
            await Root.StateHasChangedAsync();
        }

#endregion Move Right Button
#region Delete Button
        private async Task OnDeleteAsync()
        {
            //将这个组件从这个行集合中移除 
            ComponentsInParentRow.Remove(Component);
            
            // remove row from parent container 
            // if the row is empty and not last row in container
            // otherwise there will be no drop zone to drag and drop new components
            if (ComponentsInParentRow.Count == 0 && Container.ContainerData.Rows.Count != 1)
            {
                Container.ContainerData.Rows.Remove(ComponentsInParentRow);
            }

            await Root.SelectComponentAsync(null);
            await Root.StateHasChangedAsync();
        }
#endregion Delete Button
    }
}
