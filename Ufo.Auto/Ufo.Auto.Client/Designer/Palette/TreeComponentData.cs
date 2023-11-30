﻿using AntDesign;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ufo.Auto.Client.Designer.Palette
{
    public class TreeComponentData : ITreeData<TreeComponentData>
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public ControlType Type { get; set; }
        public IEnumerable<TreeComponentData> Children { get; set; }=new List<TreeComponentData>();
        public TreeComponentData Value => this;
        
    }
}