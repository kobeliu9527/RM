using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Models.Dto
{
    public class MainPage
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Action? StateHasChanged { get; set; }
        /// <summary>
        /// 根控件
        /// </summary>
        public Control Controlmain { get; set; } = new() { Key = "主页面", CtrType = WidgetType.None, Zindex = 5 };
        public Control? SelectControl { get; set; }
        public WidgetType SelectedWidgetType { get; set; }
        public void StateHasChangedInvoke()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(Controlmain);
           
            StateHasChanged?.Invoke();
        }
        public Task SetSelectedControlByBoxAsync(WidgetType data)
        {
            SelectControl = null;//这里考虑不用置空
            SelectedWidgetType = data;
            return Task.CompletedTask;
        }
        public Task SetSelectControlByDesignerAsync(Control data)
        {
            // SelectControlByBox = data.Adapt<Control>();
            SelectControl = data;
            // SelectedWidgetType = null;
            StateHasChanged?.Invoke();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 递归找到第一个符合条件的
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control? FindFirst(string name)
        {
            return FindFirst(Controlmain.Controls, (s) => { return s.Key == name; });
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
