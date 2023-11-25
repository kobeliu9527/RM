using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Models;
using System.Diagnostics.CodeAnalysis;

namespace Ufo.Auto.Client.Designer.FieldProperties
{
    public partial class FieldPropertiesPanel
    {

        [CascadingParameter(Name = "Root")]
        [NotNull]
        public FormDesigner? FormDesigner { get; set; }
        public TreeViewItem<ComponentDto> MyProperty { get; set; }
        private ComponentDto? ComponentData = null;
        private ContainerDto? ContainerData = null;
        private List<TreeViewItem<ComponentDto>> BindingItems;
        public List<TreeViewItem<ComponentDto>> Get()
        {
            var res = new List<TreeViewItem<ComponentDto>>();
            ToTree(FormDesigner.ContainerData, res);
            return res;
        }
        private void ToTree(ContainerDto container, List<TreeViewItem<ComponentDto>> list)
        {
            foreach (var item2 in container.Rows)
            {
                foreach (var item3 in item2.Row)
                {
                    var itemtree = new TreeViewItem<ComponentDto>(item3) { Text = item3.Id };
                    list.Add(itemtree);
                    if (item3?.ChildContainers?.Count > 0)
                    {
                        itemtree.Items = new List<TreeViewItem<ComponentDto>>();
                        foreach (var item4 in item3.ChildContainers)
                        {
                            ToTree(item4, itemtree.Items);
                        }
                    }
                }
            }
        }
    }
}