using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Base
{
    public  class EchartsBase : ComponentBase
    {
        [Inject]
        [NotNull]
        public JsEcharts? JsEcharts { get; set; }
    }
}
