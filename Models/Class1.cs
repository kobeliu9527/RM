namespace Models
{
    public class Class1
    {
        public Class1()
        {
            ComponentDto componentDto = new ComponentDto();
            switch (componentDto.ComponentType)
            {
                case ComponentType.Container:
                    break;
                case ComponentType.Row:
                    break;
                case ComponentType.TabControl:
                    break;
                case ComponentType.Button:
                    break;
                case ComponentType.TextBox:
                    break;
                case ComponentType.DataGridView:
                    break;
                default:
                    break;
            }
            switch (componentDto.Position)
            {
                case PositionType.Static:
                    break;
                case PositionType.Relative:
                    break;
                case PositionType.Absolute:
                    break;
                case PositionType.Fixex:
                    break;
                case PositionType.Sticky:
                    break;
                default:
                    break;
            }
        }
    }
}
