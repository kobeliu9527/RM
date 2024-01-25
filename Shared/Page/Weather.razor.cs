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
            var options1 = new BlazorDiagramOptions
            {
                AllowMultiSelection = true,

                Zoom =
            {
                Enabled = false,
            },
                Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator(),
                Factory= (d, s, ta) =>
        {
                    var link = new Blazor.Diagrams.Core.Models.LinkModel(new SinglePortAnchor(s as PortModel)
                    {
                        UseShapeAndAlignment = false
                    }, ta)
                    {
                        TargetMarker = LinkMarker.Arrow
                    };
            return link;
        }
        },
            };
            var d=System.Text.Json.JsonSerializer.Deserialize<WorkFlow>(jsonstring);
            var dd=d.ToBlazorDiagram();
            Diagram = dd;
            return;
            Diagram = new BlazorDiagram();

            var firstNode = Diagram.Nodes.Add(new Blazor.Diagrams.Core.Models.NodeModel(position: new Point(50, 50))
            {
                Title = "Node 1"
            });
            var secondNode = Diagram.Nodes.Add(new Blazor.Diagrams.Core.Models.NodeModel(position: new Point(200, 100))
            {
                Title = "Node 2"
            });
            var leftPort = secondNode.AddPort(PortAlignment.Left);
            var rightPort = secondNode.AddPort(PortAlignment.Right);
            var leftPort2 = firstNode.AddPort(PortAlignment.Left);
            var rightPort2 = firstNode.AddPort(PortAlignment.Right);

            // The connection point will be the intersection of
            // a line going from the target to the center of the source
            var sourceAnchor = new ShapeIntersectionAnchor(firstNode);
            // The connection point will be the port's position
            var targetAnchor = new SinglePortAnchor(leftPort);
            var link = Diagram.Links.Add(new Blazor.Diagrams.Core.Models.LinkModel(sourceAnchor, targetAnchor));
            foreach (var item in Diagram.Links)
            {
                var ss = item;
                var sss = item.Links;
                var s2 = item.Source;
            }
            Diagram.Controls.AddFor(firstNode)
           .Add(new RemoveControl(1, 0))
           .Add(new DragNewLinkControl(1, 0.5, 20))
           .Add(new BoundaryControl());

            Diagram.Controls.AddFor(secondNode)
                .Add(new RemoveControl(1, 0))
                .Add(new DragNewLinkControl(1, 0.5, 20))
                .Add(new BoundaryControl());
            var dtoobj = Diagram.ToBlazorDiagramDto();
            var json=System.Text.Json.JsonSerializer.Serialize(dtoobj);
            return;
            WorkFlow dto = new WorkFlow()
            {
                Options = new BlazorDiagramOptionsDto()
                {
                    AllowMultiSelection = true,
                    Zoom = { Enabled = true, },
                    Links = { DefaultRouter = DefaultRouter.NormalRouter },
                },
                DefaultPathGenerator = Models.Dto.SVG.PathGenerator.SmoothPathGenerator

            };
            Diagram = dto.ToBlazorDiagram();

            var options = new BlazorDiagramOptions
            {
                AllowMultiSelection = dto.Options.AllowMultiSelection,
                Zoom = { Enabled = dto.Options.Zoom.Enabled, },
                Links = { DefaultRouter = dto.Options.Links.DefaultRouter==  DefaultRouter.NormalRouter?new NormalRouter():new OrthogonalRouter() ,//OrthogonalRouter
                DefaultPathGenerator = dto.DefaultPathGenerator switch
            {
                Models.Dto.SVG.PathGenerator.SmoothPathGenerator=>
                     new SmoothPathGenerator(),
                Models.Dto.SVG.PathGenerator.StraightPathGenerator=>
                   new StraightPathGenerator(),
                   _ => new SmoothPathGenerator(),
            }
        },
            };

            // Diagram = new BlazorDiagram(options);

            var firstNode1 = new Blazor.Diagrams.Core.Models.NodeModel(position: new Point(50, 50))
            {
                Title = "Node 1\r\n(³¢ÊÔÍÏ×§ÎÒ)"
            };

            var secondNode1 = new Blazor.Diagrams.Core.Models.NodeModel(position: new Point(200, 100))
            {
                Title = "Node 2(³¢ÊÔÍÏ×§ÎÒ)"
            };
            foreach (PortAlignment item in Enum.GetValues(typeof(PortAlignment)))
            {
                firstNode.AddPort(item);
                secondNode.AddPort(item);
            }
            var link2 = new Blazor.Diagrams.Core.Models.LinkModel(firstNode, secondNode);
            Diagram.Nodes.Add(firstNode);
            Diagram.Nodes.Add(secondNode);

            var sourceAnchor1 = new ShapeIntersectionAnchor(firstNode);
            //var sourceAnchor = new SinglePortAnchor();

            // The connection point will be the port's position
            // var targetAnchor = new SinglePortAnchor(rightPort);
            var link1 = Diagram.Links.Add(new Blazor.Diagrams.Core.Models.LinkModel(firstNode, secondNode));
            // link.SourceMarker = LinkMarker.Circle;
            link1.TargetMarker = LinkMarker.Arrow;
            // link.Router = new OrthogonalRouter();
        }
    }
}