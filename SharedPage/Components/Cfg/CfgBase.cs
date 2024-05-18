using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using SharedPage.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Components.Cfg
{
    /// <summary>
    /// 
    /// </summary>
    public class CfgBase<T> : ComponentBase
    {
        /// <summary>
        /// 级联大屏
        /// </summary>
        [CascadingParameter]
        [NotNull]
        public BigScreen? BigScreen { get; set; }
        /// <summary>
        /// 控制是否配置这个选项
        /// </summary>
        [Parameter]
        public bool IsShow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        public JsInterOp? JsOp { get; set; }

        [Inject]
        [NotNull]
        public ToastService? Msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Parameter, EditorRequired]
        public T? Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<T?> ValueChanged { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T? ValueHander
        {
            get { return Value; }
            set
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
        /// <summary>
        /// 显示要配置的项目的名称
        /// </summary>
        [Parameter, EditorRequired]
        public virtual string DisplayText { get; set; } = "";
        /// <summary>
        /// 显示这个项目的帮组信息
        /// </summary>
        [Parameter]
        public string HelpInfo { get; set; } = "";
        /// <summary>
        /// 委托更新主页面
        /// </summary>
        public void Update()
        {
            BigScreen?.RefreshHandel?.Invoke(true);
        }
    }
}
