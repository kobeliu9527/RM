using Microsoft.AspNetCore.Components;
using Shared.Page;

namespace Shared.Components
{
    public partial class ControlComponent
    {
        [CascadingParameter(Name = "DesiginerPro")]
        public DesiginerPro? DesiginerPro { get; set; }
        [Parameter]
        [NotNull]
        public Control? Data { get; set; }
        [Parameter]
        [NotNull]
        public Control? ParentData { get; set; }
        /// <summary>
        /// ���ñ����ΪFormDesigner�б�ѡ�еĿؼ�,����ˢ�½���
        /// </summary>
        /// <returns></returns>
        private  void SelectComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (DesiginerPro != null)
            {
                DesiginerPro.SelectControl = Data;
                DesiginerPro.StateHasChangedInvoke();
            }
        }
        private void SelectLeftComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (DesiginerPro != null)
            {
                DesiginerPro.SelectControl = Data.Controls[0];
                DesiginerPro.StateHasChangedInvoke();
            }
        }
        private void SelectRightComponentAsync()
        {
            // await FormDesigner.SelectComponentAsync(ComponentData);
            if (DesiginerPro != null)
            {
                DesiginerPro.SelectControl = Data.Controls[1];
                DesiginerPro.StateHasChangedInvoke();
            }
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Console.WriteLine(Data.Name);
        }
    }
}