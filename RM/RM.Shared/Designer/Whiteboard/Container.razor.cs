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
    /// 行容器,参考bootstarp设计
    /// </summary>
    public partial class Container
    {
        /// <summary>
        /// 每一个组件都应该有这个属性
        /// </summary>
        [CascadingParameter(Name = "FormDesigner")]
        [NotNull]
        public FormDesigner? FormDesigner { get; set; }

        /// <summary>
        /// SelectContainerAsync();赋值当前选中的容器
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback { get; set; }

        /// <summary>
        /// 跟容器数据
        /// </summary>
        [Parameter]
        public ContainerDto? ContainerData { get; set; }

        private const string ComponentHighlighterCssClass = @"'component-element--hover'";
        private const string DropZoneCssClasses = @"'bo-dropzone-hover bo-drag-enter'";
        private const string DropZoneCssClassesWithoutSingleQuotes = @"bo-dropzone-hover bo-drag-enter";
#region Move Row Up-Down Methods
        private bool IsMoveRowUpVisible(List<ComponentDto> componentsInRow)
        {
            return ContainerData.Rows.IsMoveLeftPossible(componentsInRow);
        }

        private bool IsMoveRowDownVisible(List<ComponentDto> componentsInRow)
        {
            return ContainerData.Rows.IsMoveRightPossible(componentsInRow);
        }

        private async Task MoveRowUpAsync(List<ComponentDto> componentsInRow)
        {
            ContainerData.Rows.MoveLeft(componentsInRow);
            await Task.CompletedTask;
        }

        private async Task MoveRowDownAsync(List<ComponentDto> componentsInRow)
        {
            ContainerData.Rows.MoveRight(componentsInRow);
            await Task.CompletedTask;
        }

#endregion Move Row Up-Down Methods
#region Drag and Drop Methods
        private async Task DragComponentStartAsync(ComponentDto draggedItemData, List<ComponentDto> currentRow, ContainerDto currentContainer)
        {
            await FormDesigner.SetDraggedComponentAsync(draggedItemData, currentRow, currentContainer);
        }

        /// <summary>
        /// 在拖动一个控件,并且要放到一个新行的时候(前置伪元素)
        /// </summary>
        /// <param name = "rowIndex"></param>
        /// <param name = "currentContainer"></param>
        /// <returns></returns>
        private async Task DropComponentBeforeRowAsync(int rowIndex, ContainerDto currentContainer)
        {
            var newRow = new List<ComponentDto>();
            currentContainer.Rows.Insert(rowIndex, newRow);
            await DropComponentToEndOfRowAsync(currentContainer, newRow);
        }

        /// <summary>
        /// 在拖动一个控件,并且要放到一个新行的时候(后置伪元素)
        /// </summary>
        /// <param name = "rowIndex"></param>
        /// <param name = "currentContainer"></param>
        /// <returns></returns>
        private async Task DropComponentAfterRowAsync(int rowIndex, ContainerDto currentContainer)
        {
            var newRow = new List<ComponentDto>();
            var newRowIndex = rowIndex + 1;
            currentContainer.Rows.Insert(newRowIndex, newRow);
            await DropComponentToEndOfRowAsync(currentContainer, newRow);
        }

        /// <summary>
        /// 拖拽一个组件到一个已经存在的行的时候触发
        /// </summary>
        /// <param name = "containerData"></param>
        /// <param name = "destinationRow"></param>
        /// <returns></returns>
        /// <exception cref = "NotImplementedException"></exception>
        private async Task DropComponentToEndOfRowAsync(ContainerDto containerData, List<ComponentDto> destinationRow)
        {
            if (FormDesigner.IsDraggedItemPaletteWidget()) //从工具箱中拖动的
            {
                // you are trying to drag and drop a new widget from Palette
                var paletteWidgetData = await FormDesigner.GetDraggedPaletteWidgetAsync();
                var newComponentData = paletteWidgetData.CreateComponent();
                await AddComponentToRowAsync(newComponentData, destinationRow);
                await FormDesigner.SelectComponentAsync(newComponentData);
            }
            else if (FormDesigner.IsDraggedItemComponent())
            {
                // you are trying to drag and drop a widget already defined in whiteboard
                var componentData = await FormDesigner.GetDraggedComponentDataAsync();
                var originRow = await FormDesigner.GetDraggedComponentOriginRowAsync();
                // de-attach widget from its previous position in origin row
                await DetachComponentFromPreviousRowAsync(componentData, originRow);
                // TODO: Check is component movable
                // add component to destination row
                await AddComponentToRowAsync(componentData, destinationRow);
                if (originRow.Count == 0)
                {
                    var originContainer = await FormDesigner.GetDraggedComponentOriginContainerAsync();
                    originContainer.Rows.Remove(originRow);
                    originRow = null;
                }

                await FormDesigner.SelectComponentAsync(componentData);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 移除拖动添加控件的样式
        /// </summary>
        /// <param name = "elementId"></param>
        /// <returns></returns>
        private async Task OnDropFinishedAsync(Guid elementId)
        {
            await JSRuntime.InvokeVoidAsync("formDesigner.utils.removeClassById", elementId, DropZoneCssClassesWithoutSingleQuotes);
        }

#endregion Drag and Drop Methods
#region Helper Methods
        /// <summary>
        /// 将组件添加到行中,并重新计算空间
        /// </summary>
        /// <param name = "componentData"></param>
        /// <param name = "currentRow"></param>
        /// <returns></returns>
        private async Task AddComponentToRowAsync(ComponentDto componentData, List<ComponentDto> currentRow)
        {
            currentRow.Add(componentData);
            ComponentUtils.ComputeEachItemSizeInRow(currentRow);
            await Task.CompletedTask;
        }

        private async Task DetachComponentFromPreviousRowAsync(ComponentDto componentData, List<ComponentDto> destinationRow)
        {
            var originRow = await FormDesigner.GetDraggedComponentOriginRowAsync();
            // remove component from origin row
            originRow.Remove(componentData);
            var originContainer = await FormDesigner.GetDraggedComponentOriginContainerAsync();
            if (destinationRow?.Count == 0 && destinationRow != originRow)
            {
                await originContainer.RemoveRowAsync(originRow);
            }

            if (originRow?.Count == 0 && destinationRow != originRow)
            {
                await originContainer.RemoveRowAsync(originRow);
            }

            await FormDesigner.SelectComponentAsync(null);
        }

        /// <summary>
        /// Returns the CSS column width that must be used by a component in the editor
        /// to reflect its width and the currently selected resolution.
        /// </summary>
        /// <param name = "componentsInRow"></param>
        /// <returns></returns>
        private int CalculateRowSize(List<ComponentDto> componentsInRow)
        {
            int size = 0;
            foreach (var component in componentsInRow)
            {
                size += ComponentUtils.CalculateColumnWidth(component);
            }

            return size;
        }
#endregion Helper Methods
    }
}
