using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class Query<T> where T : class,new()
    {
        public T QueryDto { get; set; } = new();
        public List<Role>? Roles { get; set; }
    }
}
