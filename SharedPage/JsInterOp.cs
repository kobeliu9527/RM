using EntityStore;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedPage.Ext;
using SharedPage.JsonConvert;
using SharedPage.Model;
using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedPage
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class JsInterOp : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        private IJSRuntime jsRun;
        public JsInterOp(IJSRuntime jsRuntime)
        {
            jsRun = jsRuntime;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/SharedPage/js/jsfunc.js").AsTask());
        }
        public async ValueTask Init(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.init", id);
        }
        /// <summary>
        /// 直接传对象,方法内部复制序列化,默认会合并之前的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <param name="notMerge">不合并需要设置成true</param>
        /// <param name="lazyUpdate"></param>
        /// <returns></returns>
        public async ValueTask SetOption(string id, object? option, bool notMerge = false, bool lazyUpdate = false)
        {
            var module = await moduleTask.Value;
            var json = option.SerializeForJsFun();
            await module.InvokeVoidAsync("JsFunc.setOption", id, json, notMerge, lazyUpdate);
        }
        public async ValueTask dispose(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.dispose", id);
        }

        public async ValueTask Resize(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.resize", id);
        }
        public async ValueTask addResizeListener(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.addResizeListener", id);
        }
        public async ValueTask removeResizeListener(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.removeResizeListener", id);
        }
        /// <summary>
        /// 未选择的组件增加边框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCtrl"></param>
        /// <returns></returns>
        public async ValueTask addClassForSelect(string id, bool isCtrl = false)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.addClassForSelect", id, isCtrl);
        }
        /// <summary>
        /// 移除所有被选中添加边框的效果
        /// </summary>
        /// <returns></returns>
        public async ValueTask removeClassForSelect()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("JsFunc.removeClassForSelect");
        }
        /// <summary>
        /// 获取一个html元素的宽和高
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<int[]> getWH(string id)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int[]>("JsFunc.getWH", id);
        }

        public async ValueTask<Result> setItem(string key, object val)
        {
            Result result = new();
            try
            {
                var module = await moduleTask.Value;
                await jsRun.InvokeVoidAsync("localforage.setItem", key, val);
                return result.Ok();
            }
            catch (JSException ex)
            {
                await jsRun.InvokeVoidAsync("console.log", ex);
                return result.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async ValueTask<T?> getItem<T>(string key)
        {
            try
            {
                var module = await moduleTask.Value;
                var s= await jsRun.InvokeAsync<T>("localforage.getItem", key);
                return s;
            }
            catch (JSException ex)
            {
                await jsRun.InvokeVoidAsync("console.log", ex);
                return default;
            }
        }
        public async ValueTask<List<T>> getItems<T>()
        {
            try
            {
                var module = await moduleTask.Value;
                var s = await jsRun.InvokeAsync<List<string>>("localforage.keys");
                List<T> items = new List<T>();
                foreach (var item in s)
                {
                    try
                    {
                        var itt = await jsRun.InvokeAsync<T>("localforage.getItem", item);
                        if (itt != null)//只会获取到我们需要的类型
                        {
                            items.Add(itt);
                        }
                    }
                    catch (Exception e)
                    {
                        await jsRun.InvokeVoidAsync("console.log", e);
                    }
                  
                }
                return items;
            }
            catch (JSException ex)
            {
                await jsRun.InvokeVoidAsync("console.log", ex);
                return new List<T>();
            }
        }
        public async ValueTask<List<string>> getKeys()
        {
            try
            {
                var module = await moduleTask.Value;
                var s = await jsRun.InvokeAsync<List<string>>("localforage.keys");
                return s;
            }
            catch (Exception e)
            {
                await jsRun.InvokeVoidAsync("console.log", e);
                return new List<string>();
            }
        }
        public async ValueTask<Result> removeItem(string key)
        {
            Result result = new();
            try
            {
                await jsRun.InvokeVoidAsync("localforage.removeItem", key);
                return result.Ok();
            }
            catch (JSException ex)
            {
                await jsRun.InvokeVoidAsync("console.log", ex);
                return result.HasException(ex);
            }
        }
        public async ValueTask clear()
        {
            try
            {
                await jsRun.InvokeVoidAsync("localforage.clear");
            }
            catch (JSException ex)
            {
                await jsRun.InvokeVoidAsync("console.log", ex);
            }
        }
        public async ValueTask Log(object? id)
        {
            await jsRun.InvokeVoidAsync("console.log", id);
        }
        /// <summary>
        /// 复制文本到系统剪贴板
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async ValueTask Copy(string obj)
        {
            await jsRun.InvokeVoidAsync("navigator.clipboard.writeText", obj);
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
