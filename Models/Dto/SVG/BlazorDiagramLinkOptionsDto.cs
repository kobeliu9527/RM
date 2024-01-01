using Microsoft.AspNetCore.Components.Routing;

namespace Models.Dto.SVG
{
    public class BlazorDiagramLinkOptionsDto
    {
        public DefaultRouter DefaultRouter { get; set; }
    }
    public enum DefaultRouter
    {
        NormalRouter, OrthogonalRouter
    }
}