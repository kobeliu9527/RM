using Microsoft.AspNetCore.Components;

namespace Ufo.Auto.Client
{
    public partial class Routes
    {
        public int app { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            app = 5;
        }
        protected override void OnParametersSet()
        {
            ;
        }
        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            Console.WriteLine(firstRender);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await Task.Delay(5000);
            app = 4;

        }
    }
}