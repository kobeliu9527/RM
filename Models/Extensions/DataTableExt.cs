using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Extensions
{
    public static class DataTableExt
    {
        public static DataTable Queryable(this DataTable dt, string QueryString)
        {
            var dic = dt.Clone();
            var ind = Convert.ToInt32(QueryString);
            var res = dt.AsEnumerable().Take(ind).CopyToDataTable();
            return res;
        }
    }
}
