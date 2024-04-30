using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Base
{
    public class EchartsBase : ComponentBase, IAsyncDisposable
    {
        [Inject]
        [NotNull]
        public JsEcharts? JsEcharts { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsEcharts.Init(Id);
                await JsEcharts.addResizeListener(Id);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        public async ValueTask DisposeAsync()
        {
            await JsEcharts.removeResizeListener(Id);
            GC.SuppressFinalize(this);
        }
    }
}
