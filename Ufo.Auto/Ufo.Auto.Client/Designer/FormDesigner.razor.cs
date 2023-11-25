using Microsoft.AspNetCore.Components;
using Models;

namespace Ufo.Auto.Client.Designer
{
    public partial class FormDesigner
    {
        /// <summary>
        /// ������
        /// </summary>
        [Parameter]
        public string? LeftName { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        [Parameter]
        public ContainerDto? ContainerData { get; set; }
        /// <summary>
        /// ѡ�е�����
        /// </summary>
        private ContainerDto? SelectedContainer = null;
        /// <summary>
        /// ѡ�еĿؼ�
        /// </summary>
        private ComponentDto? SelectedComponent = null;
        /// <summary>
        /// �����������ڱ��϶����������,ondragstart�ᴥ��
        /// </summary>
        private PaletteWidgetDto? DraggedPaletteWidget = null;
        /// <summary>
        /// ������б��϶�����Ŀ��Dto����
        /// </summary>
        private ComponentDto? DraggedComponentData = null;
        /// <summary>
        ///
        /// </summary>
        private RowDto DraggedComponentOriginRow = null;
        /// <summary>
        ///
        /// </summary>
        private ContainerDto DraggedComponentOriginContainer = null;
        /// <summary>
        /// ���������ᴥ��,ʵ�ʾ��Ǹ�SelectedContainer��ֵ!(ʵ�ʵ����Ҳ�ᴥ��)
        /// </summary>
        /// <param name="containerData"></param>
        /// <returns></returns>
        public async Task SelectContainerAsync(ContainerDto? containerData)
        {
            SelectedContainer = containerData;
            SelectedComponent = null;
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
            await StateHasChangedAsync();
        }
        public async Task<ComponentDto> GetSelectedComponentAsync()
        {
            return await Task.FromResult(SelectedComponent);
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
        public async Task<RowDto> GetDraggedComponentOriginRowAsync()
        {
            return await Task.FromResult(DraggedComponentOriginRow);
        }
        public async Task<ContainerDto> GetDraggedComponentOriginContainerAsync()
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