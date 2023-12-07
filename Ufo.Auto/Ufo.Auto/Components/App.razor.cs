using Microsoft.AspNetCore.Components;

namespace Ufo.Auto.Components
{
    public partial class App
    {
        [Parameter]
        public string app { get; set; }
        protected override void OnInitialized()
        {
            app="appsasdfasdfsafasfasf";
            base.OnInitialized();
        }
    }
}