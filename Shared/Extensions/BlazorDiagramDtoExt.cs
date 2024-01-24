using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Models.Dto.SVG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Page.SVG;
using System.Net.Http.Headers;
using Shared.Components.Svg;
using SqlSugar.Extensions;
using NetTaste;

namespace Shared.Extensions
{
    public static class BlazorDiagramDtoExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static BlazorDiagram ToBlazorDiagram(this WorkFlow dto)
        {
            var obj = new BlazorDiagram() { };
            
            obj.Options.Zoom.Enabled = true;
            obj.Options.Links.Factory = (d, s, ta) =>
            {
                var link = new Blazor.Diagrams.Core.Models.LinkModel(new SinglePortAnchor((s as PortModel)!)
                {
                    UseShapeAndAlignment = false
                }, ta)
                {
                    TargetMarker = LinkMarker.Arrow
                };
                return link;
            };
            foreach (Models.Dto.SVG.NodeModel item in dto.Nodes)
            {
                
                var Node = new NodeModelFW(position: new Point(item.Position.X, item.Position.Y), id: item.Id)
                {
                    Title = item.Title,
                   
                };
                
                foreach 
                    (PortAlignment item2 in Enum.GetValues(typeof(PortAlignment)))
                {
                    Node.AddPort(item2);
                }
                obj.Nodes.Add(Node);
            }
            foreach (var item in dto.Links)
            {
                var sourse = obj.Nodes.FirstOrDefault(x => x.Id == item.SourceId)?.GetPort(item.SourceAlignment);
                var target = obj.Nodes.FirstOrDefault(x => x.Id == item.TargetId)?.GetPort(item.TargetAlignment);
                if (sourse != null && target != null)
                {
                    obj.Links.Add(new Blazor.Diagrams.Core.Models.LinkModel(sourse, target) { TargetMarker = LinkMarker.Arrow });
                }
            }
            return obj;
        }
        /// <summary>
        /// 转化成数据库对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WorkFlowTemplate ToBlazorDiagramDto(this BlazorDiagram obj,long id=0)
        {
            var dto = new WorkFlowTemplate() { Id=id};
            dto.Links=new ();
            dto.Nodes = new ();
            foreach (var node in obj.Nodes)
            {
                if (node is NodeModelFW nodefw)
                {
                    var n = new Models.Dto.SVG.NodeModel() { 
                        Id = node.Id, 
                        Title = node.Title,
                        Position = node.Position };
                    n.SoursesIdList = node.PortLinks.Select(x => (x.Source.Model as PortModel)!.Id).ToList();
                    n.TargetsIdList = node.PortLinks.Select(x => (x.Target.Model as PortModel)!.Id).ToList();
                    dto.Nodes.Add(n);
                }

            }
            foreach (var item in obj.Links)
            {
                Models.Dto.SVG.LinkModel lDto = new Models.Dto.SVG.LinkModel();
                lDto.Id = item.Id;
                if (item.Source.Model is PortModel portModels)
                {
                    lDto.SourceId = portModels.Parent.Id;
                    lDto.SourceAlignment = portModels.Alignment;
                }
                if (item.Target.Model is PortModel portModelt)
                {
                    lDto.TargetId = portModelt.Parent.Id;
                    lDto.TargetAlignment = portModelt.Alignment;
                }
                dto.Links.Add(lDto);
            }
            return dto;
        }
        public static BlazorDiagram ToBlazorDiagram(this WorkFlowTemplate wft)
        {
            BlazorDiagram _blazorDiagram = new BlazorDiagram();
            _blazorDiagram.Options.Links.Factory = (d, s, ta) =>
            {
                var link = new Blazor.Diagrams.Core.Models.LinkModel(new SinglePortAnchor((s as PortModel)!)
                {
                    MiddleIfNoMarker = true,
                    UseShapeAndAlignment = false

                }, ta)
                {
                    // Router = new OrthogonalRouter(),//正交的
                    // PathGenerator = new StraightPathGenerator(),//直的
                    TargetMarker = LinkMarker.Arrow,
                    // SourceMarker = LinkMarker.NewCircle(10),
                    SourceMarker = LinkMarker.Circle,
                    // SourceMarker = LinkMarker.NewRectangle(10, 20),
                    // TargetMarker = LinkMarker.NewArrow(20, 10),
                    Segmentable = true
                }
                ;
                return link;

            };
            //_blazorDiagram.SelectionChanged += (m) =>
            //{
            //    if (m is NodeModelFW nm)
            //    {
            //        if (nm.Selected)
            //        {
            //            SelectedNode = nm;
            //            StateHasChanged();
            //        }
            //    }

            //};
            _blazorDiagram.RegisterComponent<NodeModelFW, NodeFW>();
            if (wft.Nodes!=null)
            {
                foreach (Models.Dto.SVG.NodeModel item in wft.Nodes)
                {
                    var Node = new NodeModelFW(position: new Point(item.Position!.X, item.Position.Y), id: item.Id!)
                    {
                        Title = item.Title,
                    };

                    foreach
                        (PortAlignment item2 in Enum.GetValues(typeof(PortAlignment)))
                    {
                        Node.AddPort(item2);
                    }
                    _blazorDiagram.Nodes.Add(Node);
                }
            }

            if (wft.Links!=null)
            {
                foreach (var item in wft.Links)
                {
                    var sourse = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.SourceId)?.GetPort(item.SourceAlignment);
                    var target = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.TargetId)?.GetPort(item.TargetAlignment);
                    if (sourse != null && target != null)
                    {
                        _blazorDiagram.Links.Add(new Blazor.Diagrams.Core.Models.LinkModel(sourse, target) { TargetMarker = LinkMarker.Arrow });
                    }
                }
            }
            return _blazorDiagram;
        }
        public static PortAlignment ToPortAlignment(this PortPosition p)
        {
            switch (p)
            {
                case PortPosition.Top:
                    return PortAlignment.Top;
                case PortPosition.TopRight:
                    return PortAlignment.TopRight;
                case PortPosition.Right:
                    return PortAlignment.Right; ;
                case PortPosition.BottomRight:
                    return PortAlignment.BottomRight; ;
                case PortPosition.Bottom:
                    return PortAlignment.Bottom; ;
                case PortPosition.BottomLeft:
                    return PortAlignment.BottomLeft; ;
                case PortPosition.Left:
                    return PortAlignment.Left; ;
                case PortPosition.TopLeft:
                    return PortAlignment.TopLeft; ;
                default:
                    return PortAlignment.TopRight; ;
            }

        }



    }
}
