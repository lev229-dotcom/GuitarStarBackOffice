using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace GuitarStarBackOffice.ServerSide.Shared;

/// <summary>
/// Отображает title страницы
/// </summary>
public class Title : ComponentBase
{
    /// <summary>
    /// Название системы
    /// </summary>
    private const string SystemName = "Tegmento";

    /// <summary>
    /// Gets or sets the content to be rendered as the document title.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent(0, typeof(PageTitle));
        builder.AddAttribute(1, "ChildContent", (RenderFragment)BuildTitle);
        builder.CloseComponent();
    }

    private void BuildTitle(RenderTreeBuilder builder)
    {
        builder.AddContent(0, $"{SystemName} | ");
        builder.AddContent(1, ChildContent);
    }
}