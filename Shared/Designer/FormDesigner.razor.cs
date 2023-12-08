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
        /// ������
        /// </summary>
        [Parameter]
        public string? LeftName { get; set; }
        /// <summary>
        /// �Ƿ���������ģʽ
        /// </summary>
        [Parameter]
        public bool IsDesigner { get; set; }
        private ConsoleLogger? NormalLogger { get; set; }
        private ConcurrentQueue<ConsoleMessageItem> ColorMessages { get; set; } = new();
        ///// <summary>
        ///// ����������
        ///// </summary>
        //[Parameter]
        //[NotNull]
        //public ContainerDto? ContainerData { get; set; }
        /// <summary>
        /// ҳ������
        /// </summary>
        [Parameter]
        [NotNull]
        public FunctionPage? FunctionPage { get; set; }
        /// <summary>
        /// ѡ�е�����
        /// </summary>
        public ContainerDto? SelectedContainer = null;
        /// <summary>
        /// ѡ�еĿؼ�
        /// </summary>
        public ComponentDto? SelectedComponent = null;
        /// <summary>
        /// ѡ�е���
        /// </summary>
        public RowDto? SelectedRowDto = null;
        /// <summary>
        /// �����������ڱ��϶����������,ondragstart�ᴥ��
        /// </summary>
        public PaletteWidgetDto? DraggedPaletteWidget = null;
        /// <summary>
        /// ������б��϶�����Ŀ��Dto����
        /// </summary>
        public ComponentDto? DraggedComponentData = null;
        /// <summary>
        /// ������б��϶���������ڵ���
        /// </summary>
        public RowDto? DraggedComponentOriginRow = null;
        /// <summary>
        /// ������б��϶���������ڵ�����
        /// </summary>
        public ContainerDto? DraggedComponentOriginContainer = null;
        public void Test()
        {

            if (ColorMessages.Count > 2)
            {
                ColorMessages.TryDequeue(out _);
            }
            ColorMessages.Enqueue(new ConsoleMessageItem() { Message = "���ֶ���" });
        }
        protected override void OnInitialized()
        {
            StateHasChangedOnContainer = () => { StateHasChanged(); };
            ColorMessages.Enqueue(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message" });
            base.OnInitialized();

        }
        /// <summary>
        /// ���������ᴥ��,ʵ�ʾ��Ǹ�SelectedContainer��ֵ!(ʵ�ʵ����Ҳ�ᴥ��)
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
        /// ѡ��������е������ʱ��ᴥ��
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
        /// ѡ��������е��е�ʱ�򴥷�,����StateHasChangedAsync
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
        /// ��ȡ������б�ѡ�еĿؼ�
        /// </summary>
        /// <returns></returns>
        public async Task<ComponentDto?> GetSelectedComponentAsync()
        {
            return await Task.FromResult(SelectedComponent);
        }
        /// <summary>
        /// ��ȡ������б�ѡ�е���
        /// </summary>
        /// <returns></returns>
        public async Task<RowDto?> GetSelectedRowAsync()
        {
            return await Task.FromResult(SelectedRowDto);
        }
        /// <summary>
        /// �ж�һ������Ƿ��ڱ�ѡ�е�״̬
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool IsSelectedComponent(ComponentDto? component)
        {
            return SelectedComponent != null && SelectedComponent == component;
        }
        /// <summary>
        /// �������еĹ��߱���ʼ�϶���ʱ��ᴥ��,Ȼ��ֵ�� <see cref="DraggedPaletteWidget"/>
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
        /// ���ع����������ڱ��϶�����
        /// </summary>
        /// <returns></returns>
        public async Task<PaletteWidgetDto?> GetDraggedPaletteWidgetAsync()
        {
            return await Task.FromResult(DraggedPaletteWidget);
        }
        /// <summary>
        /// �϶�����������е����ʵ��:����������б��϶���Dto��Ϊnull,Ϊ�������е�Dto����Ϊnull
        /// </summary>
        /// <returns></returns>
        public bool IsDraggedItemComponent()
        {
            return DraggedComponentData != null && DraggedPaletteWidget == null;
        }
        /// <summary>
        /// �϶����ǹ������е����
        /// </summary>
        /// <returns></returns>
        public bool IsDraggedItemPaletteWidget()
        {
            return DraggedPaletteWidget != null && DraggedComponentData == null;
        }
        /// <summary>
        /// ������е�������϶���ʱ�򴥷�
        /// </summary>
        /// <param name="draggedComponentData">���϶������</param>
        /// <param name="draggedComponentOriginRow">���϶���������ڵ���</param>
        /// <param name="draggedComponentOriginContainer">���϶���������ڵ�����</param>
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
        /// �ҵ�����
        /// </summary>
        /// <returns></returns>
        public async Task<ContainerDto?> GetDraggedComponentOriginContainerAsync()
        {
            return await Task.FromResult(DraggedComponentOriginContainer);
        }

        /// <summary>
        /// ˢ������ҳ��
        /// </summary>
        /// <returns></returns>
        public async Task StateHasChangedAsync()
        {
            StateHasChanged();
            await Task.CompletedTask;
        }

    }
}