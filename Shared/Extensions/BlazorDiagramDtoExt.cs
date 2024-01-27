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
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class BlazorDiagramDtoExt
    {
        /// <summary>
        /// 序列化BlazorDiagram为Dto对象
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WorkFlowTemplate ToBlazorDiagramDto(this BlazorDiagram diagram, long id = 0)
        {
            var dto = new WorkFlowTemplate() { Id = id };
            dto.Links = new();
            dto.Nodes = new();
            foreach (var node in diagram.Nodes)
            {
                if (node is CustomNode cusNode)
                {
                    var n = new NodeDto()
                    {
                        Id = node.Id,
                        Order = node.Order,
                        Position = node.Position,
                        Width = cusNode.NodeData.Width,
                        Height = cusNode.NodeData.Height,
                        Radius = cusNode.NodeData.Radius,
                        Offset = cusNode.NodeData.Offset,
                        Corner = cusNode.NodeData.Corner,
                        BorderWidth = cusNode.NodeData.BorderWidth,
                        BorderColor = cusNode.NodeData.BorderColor,
                        FillColor = cusNode.NodeData.FillColor,
                        TextColor = cusNode.NodeData.TextColor,
                        FontSize = cusNode.NodeData.FontSize,
                        NodeType = cusNode.NodeData.NodeType,
                        NodeModelName = cusNode.NodeData.NodeModelName
                    };
                    n.SoursesIdList = node.PortLinks.Select(x => (x.Source.Model as PortModel)!.Id).ToList();
                    n.TargetsIdList = node.PortLinks.Select(x => (x.Target.Model as PortModel)!.Id).ToList();
                    dto.Nodes.Add(n);
                }
            }
            foreach (var link in diagram.Links)
            {
                LinkDto linkmodel = new LinkDto();
                linkmodel.Id = link.Id;
                linkmodel.PathGenerator = link.PathGenerator is StraightPathGenerator ? true : false;
                linkmodel.Router = link.Router is OrthogonalRouter ? true : false;
                linkmodel.Segmentable = link.Segmentable;
                if (link is LinkModel lm)
                {
                    linkmodel.Width = lm.Width;
                    linkmodel.Color = lm.Color;
                    linkmodel.SelectedColor = lm.SelectedColor;
                }
                linkmodel.Labels = link.Labels.Select(x => new LinkLabel() { Content = x.Content, Distance = x.Distance, Offset = x.Offset }).ToList();
                if (link.Source.Model is PortModel portModels)
                {
                    linkmodel.SourceId = portModels.Parent.Id;
                    linkmodel.SourceAlignment = portModels.Alignment;
                }
                if (link.Target.Model is PortModel portModelt)
                {
                    linkmodel.TargetId = portModelt.Parent.Id;
                    linkmodel.TargetAlignment = portModelt.Alignment;
                }
                dto.Links.Add(linkmodel);
            }
            return dto;
        }
        /// <summary>
        /// 从Dto反序列化为BlazorDiagram
        /// </summary>
        /// <param name="diagramDto"></param>
        /// <returns></returns>
        public static BlazorDiagram ToBlazorDiagram(this WorkFlowTemplate diagramDto, bool IsLocked = false)
        {
            BlazorDiagram Diagram = new BlazorDiagram();
            Diagram.Options.Links.Factory = (d, s, ta) =>
            {
                var link = new LinkModel(new SinglePortAnchor((s as PortModel)!)
                {
                    MiddleIfNoMarker = true,
                    UseShapeAndAlignment = false

                }, ta)
                {
                    TargetMarker = LinkMarker.Arrow,
                    SourceMarker = LinkMarker.Circle,
                    Segmentable = true
                }
                ;
                return link;

            };
            Diagram.RegisterComponent<CustomNode, NodeFW>(true);
            if (diagramDto.Nodes != null)
            {
                foreach (var nodeDto in diagramDto.Nodes)
                {
                    var Node = new CustomNode(position: nodeDto.Position, id: nodeDto.Id!)
                    {
                        Title = nodeDto.NodeModelName,
                        Locked = IsLocked
                    };
                    Node.NodeData.Width = nodeDto.Width;
                    Node.NodeData.Height = nodeDto.Height;
                    Node.NodeData.Radius = nodeDto.Radius;
                    Node.NodeData.Offset = nodeDto.Offset;
                    Node.NodeData.Corner = nodeDto.Corner;
                    Node.NodeData.BorderColor = nodeDto.BorderColor;
                    Node.NodeData.BorderWidth = nodeDto.BorderWidth;
                    Node.NodeData.TextColor = nodeDto.TextColor;
                    Node.NodeData.FillColor = nodeDto.FillColor;
                    Node.NodeData.FontSize = nodeDto.FontSize;
                    Node.NodeData.NodeType = nodeDto.NodeType;
                    Node.NodeData.NodeModelName = nodeDto.NodeModelName;
                    Node.AddPort(PortAlignment.Top);
                    Node.AddPort(PortAlignment.Bottom);
                    Node.AddPort(PortAlignment.Right);
                    Node.AddPort(PortAlignment.Left);
                    switch (nodeDto.NodeType)
                    {
                        case NodeType.Square:
                            break;
                        case NodeType.Diamond:
                            break;
                        case NodeType.Ellipse:
                            break;
                        case NodeType.Circle:
                            break;
                        case NodeType.Other:
                            break;
                        case null:
                            break;
                        default:
                            break;
                    }
                    Diagram.Nodes.Add(Node);
                }
            }
            if (diagramDto.Links != null)
            {
                foreach (var linkDto in diagramDto.Links)
                {
                    var sourse = Diagram.Nodes.FirstOrDefault(x => x.Id == linkDto.SourceId)?.GetPort(linkDto.SourceAlignment);
                    var target = Diagram.Nodes.FirstOrDefault(x => x.Id == linkDto.TargetId)?.GetPort(linkDto.TargetAlignment);
                    if (sourse != null && target != null)
                    {
                        var link = new LinkModel(sourse, target)
                        {
                            TargetMarker = LinkMarker.Arrow,
                            Segmentable = linkDto.Segmentable,
                            SourceMarker = LinkMarker.Circle,
                            Color = linkDto.Color,
                            SelectedColor = linkDto.SelectedColor,
                            Width = linkDto.Width,
                            //Locked = IsLocked
                        };
                        link.Router=linkDto.Router?new OrthogonalRouter():new NormalRouter();
                        link.PathGenerator=linkDto.PathGenerator?new StraightPathGenerator():new SmoothPathGenerator();
                        foreach (var lab in linkDto.Labels)
                        {
                            link.Labels.Add(new LinkLabelModel(link, $"{lab.Content}", lab.Distance));
                        }
                        Diagram.Links.Add(link);
                    }
                }
            }
            return Diagram;
        }
        public static string GetIcon(this NodeType type)
        {
            switch (type)
            {
                case NodeType.Square:
                    return "M960.031235 159.921913v703.656418c0 52.974134-42.979014 95.953148-95.953148 95.953148h-703.656418c-52.974134 0-95.953148-42.979014-95.953148-95.953148v-703.656418c0-52.974134 42.979014-95.953148 95.953148-95.953148h703.656418c52.974134 0 95.953148 42.979014 95.953148 95.953148z m-831.593949-159.921913C57.771791 0 0.499756 57.272035 0.499756 127.937531v767.625183c0 70.665495 57.272035 127.937531 127.93753 127.93753h767.625183c70.665495 0 127.937531-57.272035 127.937531-127.93753v-767.625183c0-70.665495-57.272035-127.937531-127.937531-127.937531h-767.625183z";
                case NodeType.Diamond:
                    return "M512 0l512 512-512 512-512-512 512-512z m0 80.497778L80.497778 512 512 943.502222 943.502222 512 512 80.497778z";
                case NodeType.Ellipse:
                    return "M501.5 136c-254.6 0-461 161.2-461 360s206.4 360 461 360 461-161.2 461-360-206.4-360-461-360z m0 659c-220.9 0-400-133.9-400-299s179.1-299 400-299 400 133.9 400 299-179.1 299-400 299z";
                case NodeType.Circle:
                    return "M512 1024c-69.1 0-136.2-13.5-199.3-40.3C251.7 958 197 921 150 874c-47-47-84-101.7-109.7-162.7C13.5 648.2 0 581.1 0 512s13.5-136.2 40.3-199.3C66 251.7 103 197 150 150c47-47 101.7-84 162.7-109.7C375.8 13.5 442.9 0 512 0s136.2 13.5 199.3 40.3C772.3 66 827 103 874 150c47 47 83.9 101.8 109.7 162.7 26.7 63.2 40.3 130.2 40.3 199.3s-13.5 136.2-40.3 199.3C958 772.3 921 827 874 874c-47 47-101.8 83.9-162.7 109.7-63.1 26.8-130.2 40.3-199.3 40.3z m0-934c-57 0-112.3 11.2-164.2 33.1-50.2 21.3-95.4 51.7-134.2 90.5-38.8 38.8-69.2 83.9-90.5 134.2C101.2 399.7 90 455 90 512c0 57 11.2 112.2 33.1 164.2 21.3 50.2 51.7 95.4 90.5 134.2 38.8 38.8 83.9 69.2 134.2 90.5 52 22 107.3 33.1 164.2 33.1 57 0 112.2-11.2 164.2-33.1 50.2-21.3 95.4-51.7 134.2-90.5 38.8-38.8 69.2-83.9 90.5-134.2 22-52 33.1-107.3 33.1-164.2 0-57-11.2-112.3-33.1-164.2-21.3-50.2-51.7-95.4-90.5-134.2-38.8-38.8-83.9-69.2-134.2-90.5C624.2 101.2 569 90 512 90z";
                case NodeType.Parallelogram:
                    return "M814.131 903.857H16.579l211.299-788.563h797.539L814.131 903.857z m-758.455-30h735.436l195.21-728.563H250.897L55.676 873.857z";
                case NodeType.Other:
                    break;
            }
            return "M1001.505 291.007 780.695 511.895l224.155 224.232c24.761 24.77 24.761 64.927 0 89.694l-179.323 179.391c-24.76 24.767-64.904 24.767-89.662 0L511.71 780.975 377.217 915.517c-119.18 119.22-342.489 72.284-342.489 72.284S-11.32 766.026 108.23 646.435l134.495-134.54L18.569 287.66c-24.758-24.77-24.758-64.927 0-89.694L197.894 18.576c24.758-24.767 64.902-24.767 89.66 0L511.71 242.812l220.863-220.94c6.399-8.619 17.173-15.609 34.256-15.609 29.66-0.627 69.125-1.464 96.478-2.046 19.284 0 42.092 9.214 51.349 18.47 22.575 22.498 59.933 59.729 83.516 83.234 10.412 10.412 20.864 25.399 20.864 55.701C1019.036 233.745 1025.04 275.533 1001.505 291.007zM586.428 766.026l67.246-67.269c4.128-4.128 10.817-4.128 14.944 0l14.944 14.949c4.125 4.128 4.125 10.821 0 14.949l-67.246 67.269 59.774 59.799 67.246-67.271c4.128-4.13 10.817-4.13 14.944 0l14.942 14.946c4.128 4.13 4.128 10.823 0 14.951l-67.246 67.271 44.832 44.846c16.508 16.512 43.269 16.512 59.774 0l119.548-119.591c16.508-16.512 16.508-43.285 0-59.797L735.865 556.741 556.54 736.127 586.428 766.026zM85.223 852.91c45.59 42.741 64.857 59.403 94.772 86.915 37.116-1.19 98.624-15.37 122.504-39.257L123.174 721.18C98.08 746.283 84.779 813.977 85.223 852.91zM272.61 93.321c-16.506-16.51-43.269-16.51-59.774 0L93.285 212.913c-16.504 16.512-16.504 43.285 0 59.797l44.832 44.848 67.246-67.271c4.128-4.128 10.817-4.128 14.944 0l14.944 14.946c4.125 4.13 4.125 10.823 0 14.951l-67.246 67.271 59.774 59.797 67.246-67.271c4.128-4.13 10.817-4.13 14.944 0l14.942 14.949c4.13 4.128 4.13 10.821 0 14.949l-67.244 67.271 29.886 29.897L466.88 287.66 272.61 93.321zM744.208 199.091c-14.607-14.605-21.943-26.582-21.943-50.8 0.208-7.995 0.453-17.316 0.708-27.125L168.006 676.332l179.323 179.391 555.825-556.024c-4.734 0.102-9.547 0.204-13.946 0.299-37.412 0-54.373-10.647-67.934-24.206C798.714 253.339 765.954 220.733 744.208 199.091zM898.202 123.958c-24.265-24.265-53.768-34.099-65.902-21.968-12.134 12.134-2.297 41.639 21.968 65.904 24.263 24.265 53.771 34.099 65.902 21.966C932.304 177.731 922.467 148.223 898.202 123.958z";
        }
        public static string GetText(this NodeType type)
        {
            switch (type)
            {
                case NodeType.Square:
                    return "正方形";
                case NodeType.Diamond:
                    return "菱形";
                case NodeType.Ellipse:
                    return "椭圆";
                case NodeType.Circle:
                    return "圆形";
                case NodeType.Parallelogram:
                    return "平行四边形";
                case NodeType.Other:
                    break;
            }
            return "自定义";
        }
        public static string GetColor(this NodeType type)
        {
            switch (type)
            {
                case NodeType.Square:
                    return "#1296db";
                case NodeType.Diamond:
                    return "#17B804";
                case NodeType.Ellipse:
                    return "#13227a";
                case NodeType.Circle:
                    return "#23fa05";
                case NodeType.Parallelogram:
                    return "#f4ea2a";
                case NodeType.Other:
                    break;
            }
            return "#1296db";
        }
    }
}
