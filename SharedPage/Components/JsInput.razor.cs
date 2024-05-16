using Microsoft.AspNetCore.Components;
using SharedPage.Model;

namespace SharedPage.Components
{
    public partial class JsInput
    {
        [Parameter]
        public string DisplayText { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool IsShow { get; set; }
        [Parameter]
        public JsFuncNumString? JsFuncNumString { get; set; }
        [Parameter]
        public EventCallback<JsFuncNumString?> JsFuncNumStringChanged { get; set; }

        public JsFuncNumString? ValueJsFuncNumString
        {
            get { return JsFuncNumString; }
            set
            {
                JsFuncNumString = value;
                if (JsFuncNumStringChanged.HasDelegate)
                {
                    JsFuncNumStringChanged.InvokeAsync(JsFuncNumString);
                }
            }
        }
        async Task OnValueChanged(bool res)
        {
            if (res)
            {
                ValueJsFuncNumString = new JsFuncNumString("0") { JsType = JsType.Function };
            }
            else
            {
                ValueJsFuncNumString = null;
            }
            await Task.CompletedTask;
        }
        async Task OnValueChangedForRaw(string res)
        {
            if (JsFuncNumString != null)
            {
                switch (JsFuncNumString.JsType)
                {
                    case JsType.Num:
                        if (!decimal.TryParse(res, out decimal num))
                        {
                            JsFuncNumString.RAW = "0";
                        }
                        break;
                    case JsType.Object:
                        if (res.StartsWith("{") && res.EndsWith("}"))
                        {

                        }
                        else
                        {
                            JsFuncNumString.RAW = "{}";
                        }
                        break;
                    case JsType.String:
                        break;
                    case JsType.Function:
                        break;
                    case JsType.Array:
                        if (res.StartsWith("[") && res.EndsWith("]"))
                        {

                        }
                        else
                        {
                            JsFuncNumString.RAW = "[]";
                        }
                        break;
                    case JsType.StringArray:
                        break;
                    default:
                        break;
                }
            }
            await Task.CompletedTask;
        }
    }
}