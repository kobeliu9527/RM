using Blazor.Diagrams;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Controls.Default;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Mapster;
using Models.Dto.SVG;
using Shared.Components.Svg;
using Shared.Extensions;

namespace Shared.Page
{
    public partial class Weather
    {
        public BlazorDiagram Diagram { get; set; } = null!;
        public void OnClick()
        {
           var dto=Diagram.ToBlazorDiagramDto();
            var json=System.Text.Json.JsonSerializer.Serialize(dto);
          

        }
        private string jsonstring = "{\"Options\":null,\"Zoom\":0,\"DefaultPathGenerator\":0,\"Nodes\":[{\"Id\":\"0067a8d2-f7c0-4160-ba2d-a45d9a17a2d4\",\"Position\":{\"X\":63.199981689453125,\"Y\":91.20002746582031,\"Length\":110.9580222215302},\"Title\":null,\"Ports\":[]},{\"Id\":\"53b24069-de7e-45f9-9fe4-fa17adc13c42\",\"Position\":{\"X\":276.0000305175781,\"Y\":260.80003356933594,\"Length\":379.7271051103289},\"Title\":null,\"Ports\":[]}],\"Links\":[{\"Id\":\"2c05971c-1e04-4237-9b94-333480dd8250\",\"SourceId\":\"53b24069-de7e-45f9-9fe4-fa17adc13c42\",\"SourceAlignment\":6,\"TargetAlignment\":3,\"Source\":null,\"TargetId\":\"0067a8d2-f7c0-4160-ba2d-a45d9a17a2d4\",\"Target\":null},{\"Id\":\"3ab522cd-ec8b-4800-83f3-188e1fccfa7f\",\"SourceId\":\"0067a8d2-f7c0-4160-ba2d-a45d9a17a2d4\",\"SourceAlignment\":0,\"TargetAlignment\":1,\"Source\":null,\"TargetId\":\"53b24069-de7e-45f9-9fe4-fa17adc13c42\",\"Target\":null}]}";
        protected override void OnInitialized()
        {
            Diagram = new BlazorDiagram();
            Diagram.RegisterComponent<CustomNode, NodeFW>();
            var node = new CustomNode(new Point(50, 50));
            var node2 = new CustomNode(new Point(10, 10));
            node.AddPort(PortAlignment.Left);
            node2.AddPort(PortAlignment.Left);
            node2.AddPort(PortAlignment.Top);
            //node.AddPort(PortAlignment.Right);
            Diagram.Nodes.Add(node);
            Diagram.Nodes.Add(node2);

            //node.AddPort(PortAlignment.BottomLeft);
            return;
        }
    }
}