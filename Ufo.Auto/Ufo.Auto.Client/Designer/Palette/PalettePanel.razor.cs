using Microsoft.AspNetCore.Components;
using Models;
using Models.Core;

namespace Ufo.Auto.Client.Designer.Palette
{
    public partial class PalettePanel
    {
        /// <summary>
        /// ���еĹ��ߵ�����
        /// </summary>
        [Parameter]
        public List<PaletteWidgetDto>? Widgets { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Widgets == null)
            {
                Widgets = PaletteWidgetSeeder.GetPaletteWidgets();
            }
        }

    }
}