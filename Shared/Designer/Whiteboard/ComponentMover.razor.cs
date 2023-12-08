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
using Shared.Designer;
using Shared.Extensions;

namespace Shared.Designer.Whiteboard
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
        public RowDto ComponentsInParentRow { get; set; }
        /// <summary>
        /// 要控制的组件的数据
        /// </summary>
        [Parameter]
        public ComponentDto? Component { get; set; }

        #region Move Left Button
        private bool IsMoveLeftVisible()
        {
            return ComponentsInParentRow.ComponentList.IsMoveLeftPossible(Component);
        }

        private async Task MoveLeftAsync()
        {
            ComponentsInParentRow?.ComponentList?.MoveLeft(Component);
            await Root.StateHasChangedAsync();
        }

        #endregion Move Left Button
        #region Move Right Button
        private bool IsMoveRightVisible()
        {
            return ComponentsInParentRow.ComponentList.IsMoveRightPossible(Component);
        }

        private async Task MoveRightAsync()
        {
            ComponentsInParentRow.ComponentList.MoveRight(Component);
            await Root.StateHasChangedAsync();
        }

        #endregion Move Right Button
        #region Delete Button
        private async Task OnDeleteAsync()
        {
            //将这个组件从这个行集合中移除 
            ComponentsInParentRow.ComponentList.Remove(Component);

            // remove row from parent container 
            // if the row is empty and not last row in container
            // otherwise there will be no drop zone to drag and drop new components
            if (ComponentsInParentRow.ComponentList.Count == 0 && Container.ContainerData.Rows.Count != 1)
            {
                Container.ContainerData.Rows.Remove(ComponentsInParentRow);
            }

            await Root.SelectComponentAsync(null);
            await Root.StateHasChangedAsync();
        }
        #endregion Delete Button
    }
}
