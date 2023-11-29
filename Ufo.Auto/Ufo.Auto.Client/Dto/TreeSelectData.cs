using AntDesign;

namespace Ufo.Auto.Client.Dto
{

    public class TreeSelectData : ITreeData<string>
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Value { get; }
        public IEnumerable<string> Children { get; set; }
        //public string Key { get; set; }
        //public Data Value => this;
        //public string Title { get; set; }
        //public IEnumerable<string> Children { get; set; }
    }

}
