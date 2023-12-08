using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Designer
{
    public partial class FormDesigner
    {
        [Parameter]
        public Action? StateHasChangedOnContainer { get; set; }
        [Inject]
        [NotNull]
        public IJSRuntime? JSRuntime { get; set; }
        /// <summary>
        /// 左侧面板
        /// </summary>
        [Parameter]
        public string? LeftName { get; set; }
        /// <summary>
        /// 是否处于设计设计模式
        /// </summary>
        [Parameter]
        public bool IsDesigner { get; set; }
        private ConsoleLogger? NormalLogger { get; set; }
        private ConcurrentQueue<ConsoleMessageItem> ColorMessages { get; set; } = new();
        ///// <summary>
        ///// 跟容器数据
        ///// </summary>
        //[Parameter]
        //[NotNull]
        //public ContainerDto? ContainerData { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        [Parameter]
        [NotNull]
        public FunctionPage? FunctionPage { get; set; }
        /// <summary>
        /// 选中的容器
        /// </summary>
        public ContainerDto? SelectedContainer = null;
        /// <summary>
        /// 选中的控件
        /// </summary>
        public ComponentDto? SelectedComponent = null;
        /// <summary>
        /// 选中的行
        /// </summary>
        public RowDto? SelectedRowDto = null;
        /// <summary>
        /// 工具箱中正在被拖动的项的数据,ondragstart会触发
        /// </summary>
        public PaletteWidgetDto? DraggedPaletteWidget = null;
        /// <summary>
        /// 设计器中被拖动的项目的Dto数据
        /// </summary>
        public ComponentDto? DraggedComponentData = null;
        /// <summary>
        /// 设计器中被拖动的组件所在的行
        /// </summary>
        public RowDto? DraggedComponentOriginRow = null;
        /// <summary>
        /// 设计器中被拖动的组件所在的容器
        /// </summary>
        public ContainerDto? DraggedComponentOriginContainer = null;
        public void Test()
        {

            if (ColorMessages.Count > 2)
            {
                ColorMessages.TryDequeue(out _);
            }
            ColorMessages.Enqueue(new ConsoleMessageItem() { Message = "啊手动阀" });
        }
        protected override void OnInitialized()
        {
            StateHasChangedOnContainer = () => { StateHasChanged(); };
            ColorMessages.Enqueue(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message" });
            base.OnInitialized();

        }
        /// <summary>
        /// 点击容器后会触发,实际就是给SelectedContainer赋值!(实际点击行也会触发)
        /// </summary>
        /// <param name="containerData"></param>
        /// <returns></returns>
        public async Task SelectContainerAsync(ContainerDto? containerData)
        {
            SelectedContainer = containerData;
            SelectedComponent = null;
            SelectedRowDto = null;
            await StateHasChangedAsync();
        }
        public async Task<ContainerDto?> GetSelectedContainerAsync()
        {
            return await Task.FromResult(SelectedContainer);
        }
        public bool IsSelectedContainer(ContainerDto containerData)
        {
            return SelectedContainer != null && SelectedContainer == containerData;
        }
        /// <summary>
        /// 选中设计器中的组件的时候会触发
        /// </summary>
        /// <param name="componentData"></param>
        /// <returns></returns>
        public async Task SelectComponentAsync(ComponentDto? componentData)
        {
            SelectedComponent = componentData;
            SelectedContainer = null;
            SelectedRowDto = null;
            await StateHasChangedAsync();
        }
        /// <summary>
        /// 选中设计器中的行的时候触发,触发StateHasChangedAsync
        /// </summary>
        /// <param name="componentData"></param>
        /// <returns></returns>
        public async Task SelectRowAsync(RowDto? componentData)
        {
            SelectedComponent = null;
            SelectedContainer = null;
            SelectedRowDto = componentData;
            await StateHasChangedAsync();
        }
        /// <summary>
        /// 获取设计器中被选中的控件
        /// </summary>
        /// <returns></returns>
        public async Task<ComponentDto?> GetSelectedComponentAsync()
        {
            return await Task.FromResult(SelectedComponent);
        }
        /// <summary>
        /// 获取设计器中被选中的行
        /// </summary>
        /// <returns></returns>
        public async Task<RowDto?> GetSelectedRowAsync()
        {
            return await Task.FromResult(SelectedRowDto);
        }
        /// <summary>
        /// 判断一个组件是否处于被选中的状态
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool IsSelectedComponent(ComponentDto? component)
        {
            return SelectedComponent != null && SelectedComponent == component;
        }
        /// <summary>
        /// 工具箱中的工具被开始拖动的时候会触发,然后赋值给 <see cref="DraggedPaletteWidget"/>
        /// </summary>
        /// <param name="draggedPaletteWidget"></param>
        /// <returns></returns>
        public async Task SetDraggedPaletteWidgetAsync(PaletteWidgetDto draggedPaletteWidget)
        {
            DraggedPaletteWidget = draggedPaletteWidget;
            DraggedComponentData = null;
            DraggedComponentOriginRow = null;
            DraggedComponentOriginContainer = null;
            await Task.CompletedTask;
        }
        /// <summary>
        /// 返回工具箱中正在被拖动的项
        /// </summary>
        /// <returns></returns>
        public async Task<PaletteWidgetDto?> GetDraggedPaletteWidgetAsync()
        {
            return await Task.FromResult(DraggedPaletteWidget);
        }
        /// <summary>
        /// 拖动的是设计器中的组件实例:所有设计器中被拖动的Dto不为null,为工具箱中的Dto必须为null
        /// </summary>
        /// <returns></returns>
        public bool IsDraggedItemComponent()
        {
            return DraggedComponentData != null && DraggedPaletteWidget == null;
        }
        /// <summary>
        /// 拖动的是工具箱中的组件
        /// </summary>
        /// <returns></returns>
        public bool IsDraggedItemPaletteWidget()
        {
            return DraggedPaletteWidget != null && DraggedComponentData == null;
        }
        /// <summary>
        /// 设计器中的组件被拖动的时候触发
        /// </summary>
        /// <param name="draggedComponentData">被拖动的组件</param>
        /// <param name="draggedComponentOriginRow">被拖动的组件所在的行</param>
        /// <param name="draggedComponentOriginContainer">被拖动的组件所在的容器</param>
        /// <returns></returns>
        public async Task SetDraggedComponentAsync(ComponentDto draggedComponentData,
            RowDto draggedComponentOriginRow, ContainerDto draggedComponentOriginContainer)
        {
            DraggedComponentData = draggedComponentData;
            DraggedComponentOriginRow = draggedComponentOriginRow;
            DraggedComponentOriginContainer = draggedComponentOriginContainer;
            DraggedPaletteWidget = null;
            await Task.CompletedTask;
        }
        public async Task<ComponentDto?> GetDraggedComponentDataAsync()
        {
            return await Task.FromResult(DraggedComponentData);
        }
        public async Task<RowDto?> GetDraggedComponentOriginRowAsync()
        {
            return await Task.FromResult(DraggedComponentOriginRow);
        }
        /// <summary>
        /// 找到容器
        /// </summary>
        /// <returns></returns>
        public async Task<ContainerDto?> GetDraggedComponentOriginContainerAsync()
        {
            return await Task.FromResult(DraggedComponentOriginContainer);
        }

        /// <summary>
        /// 刷新整个页面
        /// </summary>
        /// <returns></returns>
        public async Task StateHasChangedAsync()
        {
            StateHasChanged();
            await Task.CompletedTask;
        }

    }
}