using Microsoft.AspNetCore.Components;
using RM.Shared.Models;

namespace RM.Shared.Designer.FieldProperties.Properties
{
    public class PropertiesComponentBase : ComponentBase
    {
        public PropertiesComponentBase()
        {
        }

        [CascadingParameter(Name = "FormDesigner")]
        public FormDesigner FormDesigner { get; set; }

        [Parameter]
        public ComponentDto ComponentData { get; set; }

        public async Task OnLabelChangedAsync(ChangeEventArgs e)
        {
            ComponentData.Label = e.Value.ToString();

            await FormDesigner.StateHasChangedAsync();
        }

        public async Task OnWidthChangedAsync(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out var w))
            {
                ComponentData.Width = w;
            }
            await FormDesigner.StateHasChangedAsync();
        }
        public async Task OnHeightChangedAsync(ChangeEventArgs e)
        {
            //if (int.TryParse(e.Value.ToString(), out var w))
            //{
            //    ComponentData.Width = w;
            //}
            var ss = ComponentData.Height;
            await FormDesigner.StateHasChangedAsync();
        }
    }
}
