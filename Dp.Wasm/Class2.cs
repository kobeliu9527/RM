using Microsoft.AspNetCore.Components;
using SharedPage.Model;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Dp.Wasm
{
    public class Class2: IOrderedEnumerable<IGrouping<object?, BigScreen>>
    {
      
        public IOrderedEnumerable<IGrouping<object?, BigScreen>> CreateOrderedEnumerable<TKey>(Func<IGrouping<object?, BigScreen>, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IGrouping<object?, BigScreen>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
