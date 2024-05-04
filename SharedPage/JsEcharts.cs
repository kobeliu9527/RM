using Microsoft.JSInterop;

namespace SharedPage
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class JsEcharts : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsEcharts(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SharedPage/js/jsecharts.js").AsTask());
        }
        public async ValueTask Init(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.init", id);
        }
        public async ValueTask SetOption(string id,object? option)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.setOption", id,option);
        }
        public async ValueTask dispose(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.dispose", id);
        }
        
        public async ValueTask Resize(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.resize", id);
        }
        public async ValueTask addResizeListener(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.addResizeListener", id);
        }
        public async ValueTask removeResizeListener(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.removeResizeListener", id);
        }
        public async ValueTask addClassForSelect(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.addClassForSelect", id);
        }
        public async ValueTask removeClassForSelect()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.removeClassForSelect");
        }
        public async ValueTask<string> Prompt(string message)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }


        public async ValueTask<int[]> getWH(string id)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int[]>("echartsFunc.getWH", id);
        }
        public async ValueTask Prompt2(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync($"echarts.init(document.getElementById({id}))");
        }
        public async ValueTask Log(object id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("echartsFunc.Log",id);
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
