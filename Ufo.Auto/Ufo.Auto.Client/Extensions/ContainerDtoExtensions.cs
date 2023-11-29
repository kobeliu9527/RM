using AntDesign;
using BootstrapBlazor.Components;
using Models;
using System.Collections.Generic;
using System.Xml.Linq;
using Ufo.Auto.Client.Designer.Palette;

namespace Ufo.Auto.Client.Extensions
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
            if (container != null && container.Rows.Any())
            {
                foreach (var row in container.Rows)
                {
                    if (row != null && row.ComponentList.Any())
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public static async Task RemoveRowAsync(this ContainerDto container,
            RowDto row)
        {
            if (container.Rows.Count > 1)
            {
                container.Rows.Remove(row);
            }

            await Task.CompletedTask;
        }
        /// <summary>
        /// 递归找到容器中符合条件的控件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match">Predicate{</param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static void FindAllComponent(ContainerDto container, Func<ComponentDto, bool>? match, List<ComponentDto> result)
        {
            foreach (var row in container.Rows)
            {
                foreach (var component in row.ComponentList)
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
                            FindAllComponent(c, match, result);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 找到符合条件的组件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <param name="component"></param>
        public static void FindComponent(this ContainerDto container, Func<ComponentDto, bool> match, ref ComponentDto? component)
        {
            foreach (var row in container.Rows)
            {
                foreach (var com in row.ComponentList)
                {
                    if (match(com))
                    {
                        component = com;
                        return;
                    }
                    if (com.ChildContainers?.Count > 0)
                    {
                        foreach (var c in com.ChildContainers)
                        {
                            FindComponent(c, match, ref component);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 找到符合条件的行
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <param name="component"></param>
        public static void FindRow(this ContainerDto container, Func<RowDto, bool> match, ref RowDto? res)
        {
            foreach (var row in container.Rows)
            {
                if (match(row))
                {
                    res = row;
                    return;
                }
                foreach (var com in row.ComponentList)
                {
                    if (com.ChildContainers?.Count > 0)
                    {
                        foreach (var c in com.ChildContainers)
                        {
                            FindRow(c, match, ref res);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 找到符合条件的容器
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <param name="component"></param>
        public static void FindContainer(this ContainerDto container, Func<ContainerDto, bool> match, ref ContainerDto? res)
        {
            if (match(container))
            {
                res = container;
                return;
            }
            foreach (var row in container.Rows)
            {
                foreach (var com in row.ComponentList)
                {
                    if (com.ChildContainers?.Count > 0)
                    {
                        foreach (var c in com.ChildContainers)
                        {
                            FindContainer(c, match, ref res);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 生成控件树结构 IEnumerable<SelectedItem>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<TreeViewItem<TreeComponentData>> ToTree(this ContainerDto container, IEnumerable<TreeViewItem<TreeComponentData>> list)
        {
            var root = new TreeViewItem<TreeComponentData>(new TreeComponentData() { Key = container.Id, Title = container.Name, Type = ControlType.Comtainer, Children = new List<TreeComponentData>() }) {  Text= container.Name
            };
            ((List<TreeViewItem<TreeComponentData>>)list).Add(root);
            foreach (var row in container.Rows)
            {
                var node = new TreeViewItem<TreeComponentData>(new TreeComponentData() { Key = row.Id, Title = row.Name, Type = ControlType.Row, Children = new List<TreeComponentData>() })
                {
                    Text = row.Name
                };
                //((List<TreeComponentData>)root.Children).Add(node);
                root.Items.Add(node);
                foreach (var com in row.ComponentList)
                {
                    var node2 = new TreeViewItem<TreeComponentData>(new TreeComponentData() { Key = com.Id, Title = com.Name, Type = ControlType.Component, Children = new List<TreeComponentData>() })
                    {
                        Text = com.Name
                    };
                    //((List<TreeComponentData>)node.Children).Add(node2);
                    node.Items.Add(node2);
                    if (com.ChildContainers.Count > 0)
                    {
                        foreach (var c in com.ChildContainers)
                        {
                            //var node3 = new TreeComponentData() { Id = com.Id, Name = "rongqi" };
                            //node2.Items.Add(node3);
                            ToTree(c, node2.Items);
                        }
                    }
                }
            }
            return list;
        }
        public static List<TreeViewItem<TreeComponentData>> ToSelectedItemList(this ContainerDto container, List<SelectedItem> list)
        {
            foreach (var row in container.Rows)
            {
                foreach (var item in row.ComponentList)
                {
                    list.Add(new SelectedItem() { Text=item.Name,Value=item.Id });
                    if (item.ChildContainers.Count>0)
                    {
                        foreach (var c in item.ChildContainers)
                        {
                            ToSelectedItemList(c,list);
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 递归找到容器中符合条件的所有子容器,包括本身 List<TreeComponentData>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match">Predicate{</param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static void FindAllContainer(ContainerDto container, Func<ContainerDto, bool>? match, List<ContainerDto> result)
        {
            if (match != null)
            {
                if (match(container))
                {
                    result.Add(container);
                }
            }
            else
            {
                result.Add(container);
            }
            foreach (var row in container.Rows)
            {
                foreach (var component in row.ComponentList)
                {
                    if (component.ChildContainers?.Count > 0)
                    {
                        foreach (var c in component.ChildContainers)
                        {
                            FindAllContainer(c, match, result);
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
        public static List<ComponentDto> FindAllComponent(this ContainerDto container, Func<ComponentDto, bool>? match = null)
        {
            List<ComponentDto> res = new List<ComponentDto>();
            FindAllComponent(container, match, res);
            return res;
        }
        /// <summary>
        /// 递归找到所有符合条件的ContainerDto
        /// </summary>
        /// <param name="container"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static List<ContainerDto> FindAllContainer(this ContainerDto container, Func<ContainerDto, bool>? match = null)
        {
            List<ContainerDto> res = new List<ContainerDto>();
            FindAllContainer(container, match, res);
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
            FindAllComponent(container, match, res);
            return res.FirstOrDefault();
        }
    }
}
