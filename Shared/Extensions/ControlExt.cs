using BootstrapBlazor.Components;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class ControlExt
    {
        /// <summary>
        /// 生成控件树
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="list"></param>
        public static void ToTree(this Control ctr, List<TreeViewItem<Control>> list)
        {
            var root = new TreeViewItem<Control>(ctr) { Text = ctr.DisplayName };
            list.Add(root);
            if (ctr.Controls.Count > 0)
            {
                foreach (Control control in ctr.Controls)
                {
                    ToTree(control, root.Items);
                }
            }

        }
        /// <summary>
        /// 找到所有的参数控件(表单元素)
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="list"></param>
        public static void ToSelectedItemList(this Control ctr, List<SelectedItem> list)
        {
            foreach (var item in ctr.Controls)
            {
                if (item.CtrType == WidgetType.InputText|| item.CtrType == WidgetType.CheckBox)
                {
                    list.Add(new SelectedItem(item.Key, item.FieldName+":"+item.DisplayName));
                }
                if (item.Controls.Count > 0)
                {
                    ToSelectedItemList(item, list);
                }
            }
        }
        public static bool Remove(this Control ctr,string key)
        {
            if (ctr.Controls.Count>0)
            {
                if (ctr.Controls.RemoveAll(x => x.Key == key)>0)
                {
                    return true;
                }
            }
            foreach (var item in ctr.Controls)
            {
                if (item.Controls.Count>0)
                {
                    if (Remove(item, key))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 递归找到这个页面中所有符合条件的Control
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="math"></param>
        /// <returns></returns>
        public static List<Control> FindAll(this MainPage mp, Func<Control, bool> math)
        { 
            return mp.Controlmain.Controls.FindAll(math);
        }
        /// <summary>
        /// 递归找到一个集合中所有符合条件的Control
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="math"></param>
        /// <returns></returns>
        public static List<Control> FindAll(this List<Control> collection, Func<Control, bool> math)
        {
            List<Control> list = new List<Control>();
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    list.Add(item);
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindAll(item.Controls, math);
                    list.AddRange(control);
                }
            }
            return list;
        }
       /// <summary>
       /// 找到一个集合中,第一个符合条件的控件
       /// </summary>
       /// <param name="collection"></param>
       /// <param name="math"></param>
       /// <returns></returns>
        public static Control? FindFirst(this List<Control> collection, Func<Control, bool> math)
        {
            foreach (Control item in collection)
            {
                if (math(item))
                {
                    return item;
                }
                if (item.Controls.Count > 0)
                {
                    var control = FindFirst(item.Controls, math);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return default;
        }
        /// <summary>
        /// 递归找到第一个符合条件的(默认找key)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Match"></param>
        /// <returns></returns>
        public static Control? FindFirst(this MainPage ctr, string name, Func<Control, bool>? Match = null)
        {
            if (Match == null)
            {
                return FindFirst(ctr.Controlmain.Controls, (s) => { return s.Key == name; });
            }
            return FindFirst(ctr.Controlmain.Controls, Match);
        }
    }
}
