using AntDesign;
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
        List<TreeComponentData> _datas;
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
            _datas = new List<TreeComponentData>() { new TreeComponentData() { Id = ContainerData.Id, Type = ControlType.Comtainer, Name = "跟容器" } };
        }
        //OnClick
        public async Task OnClick(TreeEventArgs<TreeComponentData> args) 
        {
            var dataItem = ((TreeComponentData)args.Node.DataItem);
            switch (dataItem.Type)
            {
                case ControlType.Comtainer:
                    ContainerDto? res = null;
                    ContainerData.FindContainer(x => x.Id == dataItem.Id, ref res);
                    if (res != null)
                    {
                        await Root.SelectContainerAsync(res);
                    }

                    break;
                case ControlType.Row:
                    RowDto? resrow = null;
                    ContainerData.FindRow(x => x.Id == dataItem.Id, ref resrow);
                    if (resrow != null)
                    {
                        await Root.SelectRowAsync(resrow);
                    }
                    break;
                case ControlType.Component:
                    ComponentDto? resrom = null;
                    ContainerData.FindComponent(x => x.Id == dataItem.Id, ref resrom);
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
            var dataItem = ((TreeComponentData)args.Node.DataItem);
            dataItem.Items.Clear();
            switch (dataItem.Type)
            {
                case ControlType.Comtainer:
                    ContainerDto? res = null;
                    ContainerData.FindContainer(x => x.Id == dataItem.Id, ref res);
                    if (res != null)
                    {
                        await Root.SelectContainerAsync(res);
                        foreach (var item in res.Rows)
                        {
                            dataItem.Items.Add(new TreeComponentData() { Id = item.Id, Type = ControlType.Row, Name = "行" });
                        }
                    }

                    break;
                case ControlType.Row:
                    RowDto? resrow = null;
                    ContainerData.FindRow(x => x.Id == dataItem.Id, ref resrow);
                    if (resrow != null)
                    {
                        foreach (var item in resrow.ComponentList)
                        {
                            dataItem.Items.Add(new TreeComponentData() { Id = item.Id, Type = ControlType.Component, Name = "组件" });
                        }
                    }
                    break;
                case ControlType.Component:
                    ComponentDto? resrom = null;
                    ContainerData.FindComponent(x => x.Id == dataItem.Id, ref resrom);
                    if (resrom != null)
                    {
                        foreach (var item in resrom.ChildContainers)
                        {
                            dataItem.Items.Add(new TreeComponentData() { Id = item.Id, Type = ControlType.Comtainer, Name = "容器" });
                        }
                    }
                    break;
                default:
                    break;
            }

        }
    }
    public class TreeComponentData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ControlType Type { get; set; }
        public List<TreeComponentData> Items { get; set; } = new();
    }
    public enum ControlType
    {
        Comtainer,
        Row,
        Component
    }
}