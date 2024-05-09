using System.ComponentModel;

namespace SharedPage.Model
{
    public class Legend 
    {
        [DisplayName("图例类型"),Description("图例的类型。可选值:'plain'：普通图例。缺省就是普通图例。'scroll'：可滚动翻页的图例。当图例数量较多时可以使用。 https://echarts.apache.org/zh/option.html#legend ")]
        ///
        public string? type { get; set; } //= "plain";
        ///
        public bool? show { get; set; }//=true;
    }
}
