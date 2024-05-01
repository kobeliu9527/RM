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
            // await jss.SetOption("{   title: {     text: 'ECharts ����ʾ��'   },   tooltip: {},   xAxis: {     data: ['����', '��ë��', 'ѩ����', '����', '�߸�Ь', '����']   },   yAxis: {},   series: [     {       name: '����',       type: 'bar',       data: [5, 20, 36, 10, 10, 20]     }   ] }");
        }
        Random r = new();
        public async void init()
        {
            await jss.Init("asdfg");
            var option = new
            {
                title = new { text = "ECharts����ʾ��" },
                tooltip = new { },
                xAxis = new { data = new object[] { "adsf", "adsfasdf", "���ֶ���", "���ֶ���2", "���ֶ���3" } },
                yAxis = new { },
                series = new object[] { new { name = "xiaoliang", type = "bar", data = new object[] { 5, 20, 36, 10, 10, 20 } } }
            };
            await jss.SetOption("asdfg", option);
        }

        public async void setOption()
        {
           
            var option = new
            {
                //title = new { text = "ECharts����ʾ��" },
                //tooltip = new { },
                //xAxis = new { data = new object[] { "adsf", "adsfasdf", "���ֶ���", "���ֶ���2", "���ֶ���3" } },
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
            ////{title:{text:'ECharts����ʾ��'},tooltip:{},xAxis:{data:['����','��ë��','ѩ����','����','�߸�Ь','����']},yAxis:{},series:[{name:'����',type:'bar',data:[5,20,36,10,10,20]}]}
            //var option = new { 
            //    title = new { text = "ECharts����ʾ��" }, 
            //    tooltip = new { }, 
            //    xAxis = new { data = new object[] { "adsf", "adsfasdf", "���ֶ���" } } ,
            //    yAxis = new {  },
            //    series = new object[] { new { name="xiaoliang",type="bar",data=new object[] { 5, 20, 36, 10, 10, 20 } } }
            //};
            //await jss.SetOption("asdfg", option);
        }




    }
}