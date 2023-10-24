using RM.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RM.Shared.Extensions
{
    public static class ContainerDtoExtensions
    {
        /// <summary>
        /// 如果为空容器,则返回true
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static bool IsEmpty(this ContainerDto container)
        {
            var result = true;

            if (container != null &&
                container.Rows.Any())
            {
                foreach (var row in container.Rows)
                {
                    if (row != null && row.Any())
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static async Task RemoveRowAsync(this ContainerDto container,
            List<ComponentDto> row)
        {
            if (container.Rows.Count > 1)
            {
                container.Rows.Remove(row);
            }

            await Task.CompletedTask;
        }
    }
}
