using AntDesign;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Models;
using System.Diagnostics.CodeAnalysis;
using Ufo.Auto.Client.Core;
using Ufo.Auto.Client.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ufo.Auto.Client.Designer.Palette
{
    public partial class PalettePanel
    {
        /// <summary>
        /// 每一个组件都应该有这个属性
        /// </summary>
        [CascadingParameter(Name = "Root")]
        [NotNull]
        public FormDesigner? Root { get; set; }
        /// <summary>
        /// 所有的工具的数据
        /// </summary>
        [Parameter]
        public List<PaletteWidgetDto>? Widgets { get; set; }
        List<TreeViewItem<TreeComponentData>> _datas = new ();
        /// <summary>
        /// 容器本身的数据数据
        /// </summary>
        [Parameter]
        [NotNull]
        public ContainerDto? ContainerData { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Widgets == null)
            {
                Widgets = PaletteWidgetSeeder.GetPaletteWidgets();
            }
            ContainerData.ToTree(_datas);
        }

        //OnClick
        public void Query()
        {
            _datas = new() {  }; 
            ContainerData.ToTree(_datas);
        }
        public async Task OnClick(TreeEventArgs<TreeComponentData> args)
    {
        var dataItem = ((TreeComponentData)args.Node.DataItem);
        switch (dataItem.Type)
        {
            case ControlType.Comtainer:
                ContainerDto? res = null;
                ContainerData.FindContainer(x => x.Id == dataItem.Key, ref res);
                if (res != null)
                {
                    await Root.SelectContainerAsync(res);
                }

                break;
            case ControlType.Row:
                RowDto? resrow = null;
                ContainerData.FindRow(x => x.Id == dataItem.Key, ref resrow);
                if (resrow != null)
                {
                    await Root.SelectRowAsync(resrow);
                }
                break;
            case ControlType.Component:
                ComponentDto? resrom = null;
                ContainerData.FindComponent(x => x.Id == dataItem.Key, ref resrom);
                if (resrom != null)
                {
                    await Root.SelectComponentAsync(resrom);
                }
                break;
            default:
                break;
        }
    }
    public async Task OnNodeLoadDelayAsync(TreeEventArgs<TreeComponentData> args)
    {
        _datas = new List<TreeViewItem<TreeComponentData>>();
        ContainerData.ToTree(_datas);
        return;
    }
}
public enum ControlType
{
    Comtainer,
    Row,
    Component
}
}