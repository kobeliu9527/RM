using Microsoft.AspNetCore.Components;

namespace Ufo.Auto.Client
{
    public partial class Routes
    {
        public int app { get; set; }
        protected override void OnInitialized()
        {
            Console.WriteLine("OnInitializedִ����,��ִ��render");
            base.OnInitialized();
        }
        protected override async Task OnInitializedAsync()
        {
            await Task.Run(async () =>
               {
                   for (int i = 0; i < 5; i++) 
                   {
                       await Task.Delay(1000);
                       Console.WriteLine("��̨������.....");
                   }
               });
            Console.WriteLine("OnInitializedAsyncִ����,��ִ��render");

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
    }
}