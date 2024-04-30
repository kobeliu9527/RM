using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedPage.Page
{
    public partial class Designer
    {
        [Inject]
        [NotNull]
        public JsEcharts? jss { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            //JsEcharts jss = new(js);
            //await jss.Init("asdfg");
            // return;
            // await jss.SetOption("{   title: {     text: 'ECharts 入门示例'   },   tooltip: {},   xAxis: {     data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']   },   yAxis: {},   series: [     {       name: '销量',       type: 'bar',       data: [5, 20, 36, 10, 10, 20]     }   ] }");
        }
        Random r = new();
        public async void init()
        {
            await jss.Init("asdfg");
            var option = new
            {
                title = new { text = "ECharts入门示例" },
                tooltip = new { },
                xAxis = new { data = new object[] { "adsf", "adsfasdf", "啊手动阀", "啊手动阀2", "啊手动阀3" } },
                yAxis = new { },
                series = new object[] { new { name = "xiaoliang", type = "bar", data = new object[] { 5, 20, 36, 10, 10, 20 } } }
            };
            await jss.SetOption("asdfg", option);
        }

        public async void setOption()
        {
           
            var option = new
            {
                //title = new { text = "ECharts入门示例" },
                //tooltip = new { },
                //xAxis = new { data = new object[] { "adsf", "adsfasdf", "啊手动阀", "啊手动阀2", "啊手动阀3" } },
                //yAxis = new { },
                series = new[]{ new { name = "xiaoliang", type = "bar", data = new[] { r.Next(100), 20, 36, 10, 10, 20 } } }
            };
            await jss.SetOption("asdfg", option);
            return;
            ////await js.InvokeVoidAsync("window.EchartsExt.api.init");
            //await jss.Init("asdfg");
            ////await jss.Prompt("asdf");
            ////
            ////JSON.parse(jsonString)
            ////{title:{text:'ECharts入门示例'},tooltip:{},xAxis:{data:['衬衫','羊毛衫','雪纺衫','裤子','高跟鞋','袜子']},yAxis:{},series:[{name:'销量',type:'bar',data:[5,20,36,10,10,20]}]}
            //var option = new { 
            //    title = new { text = "ECharts入门示例" }, 
            //    tooltip = new { }, 
            //    xAxis = new { data = new object[] { "adsf", "adsfasdf", "啊手动阀" } } ,
            //    yAxis = new {  },
            //    series = new object[] { new { name="xiaoliang",type="bar",data=new object[] { 5, 20, 36, 10, 10, 20 } } }
            //};
            //await jss.SetOption("asdfg", option);
        }




    }
}