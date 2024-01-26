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
            foreach (NodeDto item in dto.Nodes)
            {

                var Node = new CustomNode(position: new Point(item.Position.X, item.Position.Y), id: item.Id)
                {
                    Title = item.NodeModelName,

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
        public static WorkFlowTemplate ToBlazorDiagramDto(this BlazorDiagram obj, long id = 0)
        {
            var dto = new WorkFlowTemplate() { Id = id };
            dto.Links = new();
            dto.Nodes = new();
            foreach (var node in obj.Nodes)
            {
                if (node is CustomNode nodefw)
                {
                    var n = new NodeDto()
                    {

                        Id = node.Id,
                        Width = nodefw.AppearanceInfo.Width,
                        Height = nodefw.AppearanceInfo!.Height,
                        Corner = nodefw.AppearanceInfo.Corner,
                        BorderColor = nodefw.AppearanceInfo.BorderColor,
                        BorderWidth = nodefw.AppearanceInfo.BorderWidth,
                        TextColor = nodefw.AppearanceInfo.TextColor,
                        NodeType = nodefw.AppearanceInfo.NodeType,
                        FillColor = nodefw.AppearanceInfo.FillColor,
                        NodeModelName = nodefw.AppearanceInfo.NodeModelName,
                        Position = node.Position
                    };
                    n.SoursesIdList = node.PortLinks.Select(x => (x.Source.Model as PortModel)!.Id).ToList();
                    n.TargetsIdList = node.PortLinks.Select(x => (x.Target.Model as PortModel)!.Id).ToList();
                    dto.Nodes.Add(n);
                }

            }
            foreach (var item in obj.Links)
            {
                Models.Dto.SVG.LinkModel lDto = new Models.Dto.SVG.LinkModel();
                lDto.Id = item.Id;
                lDto.Labels = item.Labels.Select(x => new LinkLabel() { Content = x.Content, Distance = x.Distance, Offset = x.Offset }).ToList();
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
        /// <summary>
        /// 从Dto反序列化为实体
        /// </summary>
        /// <param name="wft"></param>
        /// <returns></returns>
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
            _blazorDiagram.RegisterComponent<CustomNode, NodeFW>(true);
            if (wft.Nodes != null)
            {
                foreach (var node in wft.Nodes)
                {
                    var Node = new CustomNode(position: node.Position, id: node.Id!)
                    {
                        Title = node.NodeModelName,
                    };
                    Node.AppearanceInfo.NodeModelName = node.NodeModelName;
                    Node.AppearanceInfo.NodeType = node.NodeType;
                    Node.AppearanceInfo.Width = node.Width;
                    Node.AppearanceInfo.Height = node.Height;
                    Node.AppearanceInfo.Corner = node.Corner;
                    Node.AppearanceInfo.BorderColor = node.BorderColor;
                    Node.AppearanceInfo.BorderWidth = node.BorderWidth;
                    Node.AppearanceInfo.TextColor = node.TextColor;
                    Node.AppearanceInfo.FillColor = node.FillColor;
                    switch (node.NodeType)
                    {
                        case NodeType.Square:
                            Node.AddPort(PortAlignment.Top);
                            Node.AddPort(PortAlignment.TopLeft);
                            Node.AddPort(PortAlignment.TopRight);
                            Node.AddPort(PortAlignment.Bottom);
                            Node.AddPort(PortAlignment.BottomLeft);
                            Node.AddPort(PortAlignment.BottomRight);
                            Node.AddPort(PortAlignment.Right);
                            Node.AddPort(PortAlignment.Left);
                            break;
                        case NodeType.Diamond:
                            Node.AddPort(PortAlignment.Top);
                            Node.AddPort(PortAlignment.Bottom);
                            Node.AddPort(PortAlignment.Right);
                            Node.AddPort(PortAlignment.Left);
                            break;
                        case NodeType.Ellipse:
                            break;
                        case NodeType.Appoint:
                            break;
                        case NodeType.Other:
                            break;
                        case null:
                            break;
                        default:
                            break;
                    }
                    _blazorDiagram.Nodes.Add(Node);
                }
            }

            if (wft.Links != null)
            {
                foreach (var item in wft.Links)
                {
                    var sourse = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.SourceId)?.GetPort(item.SourceAlignment);
                    var target = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.TargetId)?.GetPort(item.TargetAlignment);
                    if (sourse != null && target != null)
                    {
                        var link = new Blazor.Diagrams.Core.Models.LinkModel(sourse, target)
                        {
                            TargetMarker = LinkMarker.Arrow,
                            Segmentable = true,
                            SourceMarker = LinkMarker.Circle
                        };
                        foreach (var lab in item.Labels)
                        {
                            link.Labels.Add(new LinkLabelModel(link, $"{lab.Content}", lab.Distance));
                        }
                        _blazorDiagram.Links.Add(link);
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
