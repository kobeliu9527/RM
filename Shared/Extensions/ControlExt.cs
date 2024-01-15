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
        public static void ToTree(this Control ctr, List<TreeViewItem<Control>> list)
        {
            var root = new TreeViewItem<Control>(ctr) { Text=ctr.DisplayName};
            list.Add(root);
            if (ctr.Controls.Count > 0)
            {
                foreach (Control control in ctr.Controls)
                {
                    ToTree(control, root.Items);
                }
            }

        }
    }
}
