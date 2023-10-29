using RM.Shared.Models;
using System.Collections.Generic;

namespace RM.Shared.Extensions
{
    /// <summary>
    /// 
    /// </summary>
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
        /// <summary>
        /// 找到容器中符合条件的控件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match">Predicate<T> </param>
        /// <returns></returns>
        private static void FindAll(ContainerDto container, Func<ComponentDto, bool>? match, List<ComponentDto> result)
        {
            foreach (var row in container.Rows)
            {
                foreach (var component in row)
                {
                    if (match != null)
                    {
                        if (match(component))
                        {
                            result.Add(component);
                        }
                    }
                    else
                    {
                        result.Add(component);
                    }
                    if (component.ChildContainers?.Count > 0)
                    {
                        foreach (var c in component.ChildContainers)
                        {
                            FindAll(c, match, result);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 递归找到所有符合条件的ComponentDto
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static List<ComponentDto> FindAll(this ContainerDto container, Func<ComponentDto, bool>? match=null)
        {
            List<ComponentDto> res = new List<ComponentDto>();
            FindAll(container, match, res);
            return res;
        }
        /// <summary>
        /// 递归找到所有符合条件的ComponentDto,然后在用FirstOrDefault()函数
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static ComponentDto? FindFirst(this ContainerDto container, Func<ComponentDto, bool> match)
        {
            List<ComponentDto> res = new List<ComponentDto>();
            FindAll(container, match, res);
            return res.FirstOrDefault();
        }
    }
}
