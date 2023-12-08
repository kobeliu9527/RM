using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Designer.Palette
{
    public class TreeComponentData
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public ControlType Type { get; set; }
        public IEnumerable<TreeComponentData> Children { get; set; } = new List<TreeComponentData>();
        public TreeComponentData Value => this;

    }
}