﻿using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor.Components.DataTable;

public partial class MDataTableRow<TItem>
{
    [Parameter]
    public List<DataTableHeader<TItem>> Headers { get; set; } = null!;

    [Parameter]
    public TItem Item { get; set; } = default!;

    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public Func<ItemColProps<TItem>, bool> HasSlot { get; set; } = null!;

    [Parameter]
    public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; } = null!;

    [Inject]
    private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool IsSelected { get; set; }

    [Parameter]
    public bool IsExpanded { get; set; }

    [Parameter]
    public Func<TItem, string?>? ItemClass { get; set; }

    [Parameter]
    public bool Stripe { get; set; }

    public bool IsStripe => Stripe && Index % 2 == 1;

    protected override IEnumerable<string?> BuildComponentClass()
    {
        if (IsSelected)
        {
            yield return "m-data-table__selected";
        }
        
        if (IsExpanded)
        {
            yield return "m-data-table__expanded m-data-table__expanded__row";
        }
        
        if (IsStripe)
        {
            yield return "stripe";
        }

        if (Disabled)
        {
            yield return "m-data-table__disabled";
        }

        yield return ItemClass?.Invoke(Item);
    }

    private string GetCellClass(DataTableHeader header)
    {
        var stringBuilder = new CssBuilder();
        stringBuilder.Add($"text-{header.Align.ToString().ToLowerInvariant()}");

        if (header.Divider)
        {
            stringBuilder.Add("m-data-table__divider");
        }

        if (header.Fixed == DataTableFixed.Right)
        {
            stringBuilder.Add("m-data-table__column--fixed-right");
        }

        if (header.Fixed == DataTableFixed.Left)
        {
            stringBuilder.Add("m-data-table__column--fixed-left");
        }

        if (header.IsFixedShadowColumn)
        {
            stringBuilder.Add("first-fixed-column");
        }

        if (header.HasEllipsis)
        {
            stringBuilder.Add("m-data-table__column--ellipsis");
        }

        stringBuilder.Add(header.CellClass);

        return stringBuilder.ToString();
    }
    
    private string GetCellStyle(DataTableHeader<TItem> header)
    {
        var styleBuilder = StyleBuilder.Create();

        if (header.Fixed == DataTableFixed.Right)
        {
            var count = Headers.Count;
            var lastIndex = Headers.LastIndexOf(header);
            if (lastIndex > -1)
            {
                var widths = Headers.TakeLast(count - lastIndex - 1).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                styleBuilder.Add(MasaBlazor.RTL ? "left" : "right", $"{widths}px");
            }
        }
        else if (header.Fixed == DataTableFixed.Left)
        {
            var index = Headers.IndexOf(header);
            if (index > -1)
            {
                var widths = Headers.Take(index).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                styleBuilder.Add(MasaBlazor.RTL ? "right" : "left", $"{widths}px");
            }
        }

        return styleBuilder.Build();
    }
}
