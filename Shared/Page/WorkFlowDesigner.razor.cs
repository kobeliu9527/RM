using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models.Dto.SVG;
using Shared.Components.Svg;
using Shared.Extensions;
using SqlSugar;

namespace Shared.Page
{
    public partial class WorkFlowDesigner
    {
        /// <summary>
        /// 拖动选中的节点类型
        /// </summary>
        public NodeType? SelectedNodeType { get; set; }
        [Inject]
        [NotNull]
        public ISqlSugarClient? db { get; set; }
        private BlazorDiagram _blazorDiagram = new BlazorDiagram()
        {
        };
        private int? _draggedType;
        public WorkFlowTemplate WorkFlowTemplate { get; set; } = new();
        public CustomNode? SelectedNode { get; set; }
        public void Init()
        {
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
            _blazorDiagram.SelectionChanged += (m) =>
            {
                if (m is CustomNode cusNode)
                {
                    if (cusNode.Selected)
                    {
                        SelectedNode = cusNode;
                        StateHasChanged();
                    }
                }
            };
            _blazorDiagram.RegisterComponent<CustomNode, NodeFW>(true);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Init();
        }
        private void OnDrop(DragEventArgs e)
        {
            var position = _blazorDiagram.GetRelativeMousePoint(e.ClientX - 87, e.ClientY - 30);
            var node = new CustomNode(position);
            
            if (SelectedNodeType!=null)
            {
                node.AppearanceInfo.NodeType = SelectedNodeType;
                switch (SelectedNodeType)
                {
                    case NodeType.Square:
                        node.AddPort(PortAlignment.Top);
                        node.AddPort(PortAlignment.TopLeft);
                        node.AddPort(PortAlignment.TopRight);
                        node.AddPort(PortAlignment.Left);
                        node.AddPort(PortAlignment.Right);
                        node.AddPort(PortAlignment.Bottom);
                        node.AddPort(PortAlignment.BottomLeft);
                        node.AddPort(PortAlignment.BottomRight);
                        break;
                    case NodeType.Diamond:
                        node.AddPort(PortAlignment.Top);
                        node.AddPort(PortAlignment.Left);
                        node.AddPort(PortAlignment.Right);
                        node.AddPort(PortAlignment.Bottom);
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
            }
            _blazorDiagram.Nodes.Add(node);
        }
        public async Task OnChangeString(string a)
        {
            SelectedNode?.Refresh();
            await Task.CompletedTask;
        }
        public Task RefreshInvoke()
        {
            SelectedNode?.Refresh();
            return Task.CompletedTask;
        }
        public Task<bool> OnSaveAsync(WorkFlowTemplate obj, ItemChangedType type)
        {
            switch (type)
            {
                case ItemChangedType.Add:
                    db.Insertable(obj).ExecuteReturnSnowflakeId();
                    break;
                case ItemChangedType.Update:
                    if (WorkFlowTemplate != null)
                    {
                        var dto = _blazorDiagram.ToBlazorDiagramDto();
                        obj.Nodes = dto.Nodes;
                        obj.Links = dto.Links;
                        db.Updateable(obj).ExecuteCommand();
                    }

                    break;
            }
            return Task.FromResult(true);
            //db.Queryable<>(WorkFlowTemplate).ToList();
            //Func<QueryPageOptions, Task<QueryData<TItem>>>
        }
        public Task<QueryData<WorkFlowTemplate>> OnQueryAsync(QueryPageOptions options)
        {
            // IEnumerable<Product> items = Items;
            var items = db.Queryable<WorkFlowTemplate>().ToList();
            items = items ?? new List<WorkFlowTemplate>();
            // 过滤
            var isFiltered = false;
            if (options.Filters.Any())
            {
                // items = items.Where(options.Filters.GetFilterFunc<Product>());
                isFiltered = true;
            }

            // 排序
            var isSorted = false;
            if (!string.IsNullOrEmpty(options.SortName))
            {
                // var invoker = Foo.GetNameSortFunc();
                // items = invoker(items, options.SortName, options.SortOrder);
                isSorted = true;
            }

            // 设置记录总数
            var total = items.Count();
            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
            return Task.FromResult(new QueryData<WorkFlowTemplate>() { Items = items, TotalCount = total, IsSorted = isSorted, IsFiltered = isFiltered, IsSearch = true });
        }
        public Task<WorkFlowTemplate> OnAddAsync()
        {
            var obj = new WorkFlowTemplate() { Links = new(), Nodes = new() };
            return Task.FromResult(obj);
        }
        public async Task<bool> OnDeleteAsync(IEnumerable<WorkFlowTemplate> list)
        {
            await db.Deleteable<WorkFlowTemplate>(list).ExecuteCommandAsync();
            return true;
        }
        public async Task OnClickRowCallbackProduct(WorkFlowTemplate p)
        {
            if (WorkFlowTemplate.Id != p.Id)
            {
                WorkFlowTemplate = p;
                _blazorDiagram.Nodes.Clear();
                _blazorDiagram.Links.Clear();
                if (WorkFlowTemplate.Nodes != null)
                {
                    foreach (Models.Dto.SVG.AppearanceInfo item in WorkFlowTemplate.Nodes)
                    {   
                        switch (item.NodeType)
                        {
                            case NodeType.Square:
                                break;
                            case NodeType.Diamond:
                                break;
                            case NodeType.Ellipse:
                                break;
                            case NodeType.Appoint:
                                break;
                            case NodeType.Other:
                                break;
                            default:
                                break;
                        }
                        var Node = new CustomNode(position: new Point(item.Position!.X, item.Position.Y), id: item.Id!)
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
                if (WorkFlowTemplate.Links != null)
                {
                    foreach (var item in WorkFlowTemplate.Links)
                    {
                        var sourse = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.SourceId)?.GetPort(item.SourceAlignment);
                        var target = _blazorDiagram.Nodes.FirstOrDefault(x => x.Id == item.TargetId)?.GetPort(item.TargetAlignment);
                        if (sourse != null && target != null)
                        {
                            _blazorDiagram.Links.Add(new Blazor.Diagrams.Core.Models.LinkModel(sourse, target) { TargetMarker = LinkMarker.Arrow });
                        }
                    }
                }
                _blazorDiagram.Refresh();
            }
             StateHasChanged();
            await Task.Delay(50);
        }
        public void SaveSelect()
        {
            if (WorkFlowTemplate.Id != 0)
            {
                var dto = _blazorDiagram.ToBlazorDiagramDto();
                WorkFlowTemplate.Nodes = dto.Nodes;
                WorkFlowTemplate.Links = dto.Links;
                db.Updateable(WorkFlowTemplate).ExecuteCommand();
            }
        }

        #region MyRegion
        public void OnDragStartWithWidetPanel(NodeType type)
        {
            SelectedNodeType = type;
        }
        #endregion
    }
}