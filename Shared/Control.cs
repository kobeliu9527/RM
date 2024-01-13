using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    
    public class Control
    {

        public string Name { get; set; } = "NA";
        public WidgetType CtrType { get; set; } = WidgetType.Bottom;
        public bool IsContainer { get; set; }
        public bool IsVertical { get; set; }
        public int Zindex { get; set; }
        public int Height { get; set; } = 100;
        public int Width { get; set; }
        /// <summary>
        /// 当为Split的时候,表示第一个panel占比
        /// </summary>
        public string Basis { get; set; } = "30%";
        public List<Control> Controls { get; set; }=new List<Control>();
        public Control()
        {
            this.Name = CtrType.ToString();
        }
        public Control(WidgetType type,int zindex=0)
        {
            CtrType=type;
            Zindex=zindex;
            switch (type)
            {
                case WidgetType.Split:
                    IsContainer = true;
                    var first = new Control(WidgetType.FirstPanel) { Name = "FirstPanel"};
                    first.Zindex = Zindex;
                    var second = new Control(WidgetType.SecondPanel) { Name = "SecondPanel" };
                    second.Zindex = Zindex;
                    Controls.Add(first);
                    Controls.Add(second);
                    Name = "Split";
                    break;
                case WidgetType.Row:
                    IsContainer = true;
                    break;
                case WidgetType.Bottom:
                    break;
                case WidgetType.InputText:
                    break;
                case WidgetType.None:
                    break;
                case WidgetType.FirstPanel:
                    IsContainer = true;
                    break;
                case WidgetType.SecondPanel:
                    IsContainer = true;
                    break;
                default:
                    break;
            }
        }
    }
    public enum WidgetType 
    {
        None,
        Split,
        FirstPanel,
        SecondPanel,
        Row,
        Bottom,
        InputText
    }
}
