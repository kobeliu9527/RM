using SharedPage.Base;
using SharedPage.JsonConvert;
using System.Text.Json.Serialization;

namespace SharedPage.Model
{
    public class SerieLine : ESerieBase
    {
        [JsonConverter(typeof(EnumConvert<ESeriesType>))]
        public override ESeriesType type { get  ; set ; }= ESeriesType.line;
    }
}
