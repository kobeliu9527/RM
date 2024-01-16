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
        public static void ToSelectedItemList(this Control ctr, List<SelectedItem> list)
        {
            foreach (var item in ctr.Controls)
            {
                if (item.CtrType == WidgetType.InputText)
                {
                    list.Add(new SelectedItem(item.Key, item.DisplayName));
                }
                if (item.Controls.Count > 0)
                {
                    ToSelectedItemList(item, list);
                }
            }
        }
    }
}
