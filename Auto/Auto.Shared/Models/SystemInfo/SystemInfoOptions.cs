using Furion.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Shared.Models.SystemInfo
{
    public class SystemInfoOptions : IConfigurableOptions
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Company { get; set; }
    }
}
