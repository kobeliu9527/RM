using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    public class ApiData
    {
        public Dictionary<string, List<object[]>> Data { get; set; } = new();
        //new List<object[]>() { new object[]{ "1","2"}, new object[] { "3", "4" } }
    }
}
