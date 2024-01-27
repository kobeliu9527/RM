using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
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
        /// <summary>
        /// 设计器中被选中的节点
        /// </summary>
        public CustomNode? SelectedNode { get; set; }
        /// <summary>
        /// 设计器中被选中的线条
        /// </summary>
        public LinkModel? SelectedLink { get; set; }
        /// <summary>
        /// 工具箱中节点默认样式
        /// </summary>
        public NodeDto DesginerNode { get; set; } = new() { Width=100};
        /// <summary>
        /// 工具箱中线条的默认样式
        /// </summary>
        public LinkDto DesginerLink { get; set; } = new() { Router = true, PathGenerator = true };
        public void Init()
        {
            //_blazorDiagram.Options.Links.DefaultPathGenerator= new StraightPathGenerator();//直线
            // _blazorDiagram.Options.Links.DefaultPathGenerator= new SmoothPathGenerator(); //曲线
            // _blazorDiagram.Options.Links.DefaultRouter  = new OrthogonalRouter();//正交的
            //_blazorDiagram.Options.Links.DefaultRouter  = new NormalRouter();//普通的
            _blazorDiagram.Options.Links.Factory = (d, s, ta) =>
            {
                var link = new LinkModel(
                    new SinglePortAnchor((s as PortModel)!) { MiddleIfNoMarker = true, UseShapeAndAlignment = false }, ta)
                {
                    Router = DesginerLink.Router ? new OrthogonalRouter() : new NormalRouter(),//正交的
                    PathGenerator = DesginerLink.PathGenerator ? new StraightPathGenerator() : new SmoothPathGenerator(),//直的
                    TargetMarker = LinkMarker.Arrow,
                    // SourceMarker = LinkMarker.NewCircle(10),
                    SourceMarker = LinkMarker.Circle,
                    // SourceMarker = LinkMarker.NewRectangle(10, 20),
                    // TargetMarker = LinkMarker.NewArrow(20, 10),
                    Segmentable = false,
                    Color = DesginerLink.Color,
                    SelectedColor = DesginerLink.SelectedColor,
                    Width = DesginerLink.Width,
                }
                ;
                return link;

            };
            _blazorDiagram.SelectionChanged -= SelectionChanged;
            _blazorDiagram.SelectionChanged += SelectionChanged;
            _blazorDiagram.RegisterComponent<CustomNode, NodeFW>();
        }
        public void SelectionChanged(SelectableModel m)
        {
            if (m is LinkVertexModel lvm)
            {
                if (lvm.Selected)
                {
                }
            }
            if (m is CustomNode cusNode)
            {
                if (cusNode.Selected)
                {
                    SelectedNode = cusNode;
                    foreach (var item in SelectedNode.PortLinks)
                    {
                        foreach (var lab in item.Labels)
                        {
                            var ss = lab;
                        }
                        //item.Labels.Add(new LinkLabelModel(item, SelectedNode.Id, r.Next(200)));
                    }
                    StateHasChanged();
                }
            }
            if (m is Blazor.Diagrams.Core.Models.LinkModel lm)
            {
                if (lm.Selected)
                {
                    SelectedNode = null;
                    SelectedLink = lm;
                    StateHasChanged();
                }
            }
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
            node.NodeData.BorderWidth = DesginerNode.BorderWidth;
            node.NodeData.Corner = DesginerNode.Corner;
            node.NodeData.FontSize = DesginerNode.FontSize;
            node.NodeData.Radius = DesginerNode.Radius;
            node.NodeData.Offset = DesginerNode.Offset;
            node.NodeData.BorderColor = DesginerNode.BorderColor;
            node.NodeData.TextColor = DesginerNode.TextColor;
            node.NodeData.FillColor = DesginerNode.FillColor;
            if (SelectedNodeType != null)
            {
                node.NodeData.NodeType = SelectedNodeType;
                switch (SelectedNodeType)
                {
                    case NodeType.Square:
                        node.NodeData.Width = 80;
                        node.NodeData.Height = 80;
                        break;
                    case NodeType.Diamond:
                        node.NodeData.Width = 100;
                        node.NodeData.Height = 60;
                        break;
                    case NodeType.Ellipse:
                        node.NodeData.Height = 50;
                        break;
                    case NodeType.Circle:

                        break;
                    case NodeType.Other:
                        break;
                    case null:
                        break;
                    case NodeType.Parallelogram:
                        node.NodeData.Width = 80;
                        node.NodeData.Height = 50;
                        break;
                    default:
                        break;
                }
            }

            node.NodeData.Width = node.NodeData.Width * DesginerNode.Width / 100;
            node.NodeData.Height = node.NodeData.Height * DesginerNode.Width / 100;
            node.AddPort(PortAlignment.Top);
            node.AddPort(PortAlignment.Left);
            node.AddPort(PortAlignment.Right);
            node.AddPort(PortAlignment.Bottom);


            _blazorDiagram.Nodes.Add(node);
        }


        public Task RefreshInvoke()
        {
            SelectedNode?.RefreshAll();
            SelectedLink?.RefreshLinks();
            return Task.CompletedTask;
        }
        public async Task PathGeneratorChange(LinkModel link, bool val)
        {
            //StraightPathGenerator  SmoothPathGenerator
            link.PathGenerator = val ? new StraightPathGenerator() : new SmoothPathGenerator();
            //link.RefreshLinks();
            link.Refresh();
            SelectedNode?.RefreshAll();
            SelectedLink?.RefreshLinks();
            await Task.Delay(100);
        }
        public async Task PathRouterChange(LinkModel link, bool val)
        {
            //StraightPathGenerator  SmoothPathGenerator
            link.Router = val ? new OrthogonalRouter() : new NormalRouter();
            //link.RefreshLinks();
            link.Refresh();
            SelectedNode?.RefreshAll();
            SelectedLink?.RefreshLinks();
            await Task.Delay(100);
        }
        public async Task LinkColorChange(LinkModel link, string str)
        {
            //StraightPathGenerator  SmoothPathGenerator
            link.Color = str;
            link.SelectedColor = str;

            //link.RefreshLinks();
            link.Refresh();
            SelectedNode?.RefreshAll();
            SelectedLink?.RefreshLinks();
            await Task.Delay(100);
        }
        public async Task LinkWidthChange(LinkModel link, double val)
        {
            //StraightPathGenerator  SmoothPathGenerator
            link.Width = val;

            //link.RefreshLinks();
            link.Refresh();
            SelectedNode?.RefreshAll();
            SelectedLink?.RefreshLinks();
            await Task.Delay(100);
        }
        public void AddLinkLabel(LinkModel SelectedLink)
        {
            var count = SelectedLink.Labels.Count + 1;
            SelectedLink.Labels.Add(new LinkLabelModel(SelectedLink, $"标签{count}", count * 50));
            StateHasChanged();
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
                var d = p.ToBlazorDiagram();
                _blazorDiagram.Nodes.Add(d.Nodes);
                _blazorDiagram.Links.Add(d.Links);
            }
            _blazorDiagram.Refresh();
            StateHasChanged();
            await Task.Delay(50);
        }

        /// <summary>
        /// 保存选中的工作流模板
        /// </summary>
        public void SaveSelect()
        {
            if (WorkFlowTemplate.Id != 0)
            {
                var dto = _blazorDiagram.ToBlazorDiagramDto();
                WorkFlowTemplate.Nodes = dto.Nodes;
                WorkFlowTemplate.Links = dto.Links;
                var res = db.Updateable(WorkFlowTemplate).ExecuteCommand();
            }
        }

        #region MyRegion
        /// <summary>
        /// 工具箱工具被拖动后
        /// </summary>
        /// <param name="type"></param>
        public void OnDragStartWithWidetPanel(NodeType type)
        {
            SelectedNodeType = type;
        }
        #endregion


    }
}