using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Models.Dto
{
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
        /// <summary>
        /// 递归找到第一个符合条件的
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Match"></param>
        /// <returns></returns>
        public Control? FindFirst(string name, Func<Control, bool>? Match= null)
        {
            if (Match==null)
            {
                return FindFirst(Controlmain.Controls, (s) => { return s.Key == name; });
            }
            return FindFirst(Controlmain.Controls, Match);
        }

        public static Control? FindFirst(IEnumerable<Control> collection, Func<Control, bool> math)
        {
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    return item;
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindFirst(item.Controls, math);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return default;
        }
        public static List<Control> FindAll(IEnumerable<Control> collection, Func<Control, bool> math)
        {
            List<Control> list = new List<Control>();
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    list.Add(item);
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindAll(item.Controls, math);
                    list.AddRange(control);
                }
            }
            return list;
        }

    }
}
