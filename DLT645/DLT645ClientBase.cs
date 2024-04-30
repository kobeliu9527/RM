using System.Collections.Concurrent;
using UfoBase;

namespace DLT645
{
    public class DLT645ClientBase
    {
        /// <summary>
        /// 设备名字+变量名为key  变量为value
        /// </summary>
        public ConcurrentDictionary<string, Variable> Variables { get; set; }
    }
}