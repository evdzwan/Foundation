using Microsoft.AspNetCore.Components;

namespace Foundation;

interface IColumn<TItem>
{
    RenderFragment RenderCell(TItem item);
    RenderFragment RenderTitle();
}
