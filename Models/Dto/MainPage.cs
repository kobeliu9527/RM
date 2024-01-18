using Blazor.Diagrams.Core.Geometry;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Models.Dto
{
    /// <summary>
    /// 表示一个页面:有委托,有具体布局,有选中的control..
    /// </summary>
    public class MainPage
    {
        /// <summary>
        /// 调用StateHasChangedInvoke方法会调用这个委托
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Action? StateHasChanged { get; set; }
        /// <summary>
        /// 根控件
        /// </summary>
        public Control Controlmain { get; set; } = new() {  CtrType = WidgetType.None, Zindex = 5 };
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Control? SelectControl { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Control? SelectControlParent { get; set; }
        /// <summary>
        /// Used to save the Id of the currently selected row for each table
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore] 
        public Dictionary<string,object?> IdList { get; set; }=new();

        public WidgetType SelectedWidgetType { get; set; }
        /// <summary>
        /// 立即刷新主页面
        /// </summary>
        public async Task StateHasChangedInvoke()
        {
            //var json = System.Text.Json.JsonSerializer.Serialize(Controlmain);
            if (StateHasChanged != null)
            {
                StateHasChanged.Invoke();
                await Task.Delay(100);
            }
        }
        /// <summary>
        /// 设置工具箱中被选中的组件,拖动会触发!后续优化:增加一个字段,表示控件箱中被选中的控件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SetSelectedWidget(WidgetType data)
        {
            SelectedWidgetType = data;
            return Task.CompletedTask;
        }
        /// <summary>
        /// 设置设计器中被选中的控件:点击设计器中的控件等都会触发这个
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SetSelectedControl(Control data)
        {
            // SelectControlByBox = data.Adapt<Control>();
            if (SelectControl!=null)
            {
                SelectControl.IsSelect = false;
            }
            SelectControl = data;
            SelectControl.IsSelect=true;
            if (StateHasChanged!=null)
            {
                StateHasChanged.Invoke();
            }
            return Task.CompletedTask;
        }
       

        
        

    }
}
