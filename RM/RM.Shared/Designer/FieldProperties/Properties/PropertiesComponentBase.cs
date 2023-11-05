using Microsoft.AspNetCore.Components;
using RM.Shared.Models;

namespace RM.Shared.Designer.FieldProperties.Properties
{
    /// <summary>
    /// 有级联参数FormDesigner,用于控制修改后立即触发界面更新
    /// </summary>
    public class PropertiesComponentBase : ComponentBase
    {
        public PropertiesComponentBase()
        {
        }

        [CascadingParameter(Name = "Root")]
        public FormDesigner FormDesigner { get; set; }

        [Parameter]
        public ComponentDto ComponentData { get; set; }
        
        public async Task OnPropChangedAsync(PInof e)
        {
            //ComponentData.Props.TryAdd
            // ComponentData.Label = e.Value.ToString();
            //ComponentData.Props[e.key].Value = e.Value.ToString();
             await FormDesigner.StateHasChangedAsync();
        }
        public async Task OnLabelChangedAsync(ChangeEventArgs e)
        {
            ComponentData.DisplayText = e.Value.ToString();

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
