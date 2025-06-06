﻿namespace Masa.Blazor.Components.DataTable.Header;

public partial class MDataTableHeaderBase: MasaComponentBase
{
    [Inject] protected I18n I18n { get; set; } = default!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter] private MDataTableHeader? DataTableHeader { get; set; }

    [Parameter] public bool IsMobile { get; set; }

    [Parameter] public List<DataTableHeader> Headers { get; set; } = new();

    [Parameter] public bool MultiSort { get; set; }

    [Parameter] public bool SingleSelect { get; set; }

    [Parameter] public bool DisableSort { get; set; }

    [Parameter] public RenderFragment<DataTableHeader>? HeaderColContent { get; set; }

    [Parameter] public RenderFragment<DataTableHeaderSelectContext>? DataTableSelectContent { get; set; }

    [Parameter] public string SortIcon { get; set; } = "$sort";

    [Parameter] public DataOptions Options { get; set; } = null!;

    [Parameter] public EventCallback<OneOf<string?, List<string>>> OnSort { get; set; }

    [Parameter] public bool EveryItem { get; set; }

    [Parameter] public bool SomeItems { get; set; }

    [Parameter] public EventCallback<bool> OnToggleSelectAll { get; set; }

    [Parameter] public string? CheckboxColor { get; set; }

    [Parameter] public string? GroupText { get; set; }

    [Parameter] public EventCallback<string> OnGroup { get; set; }

    [Parameter] public bool ShowGroupBy { get; set; }

    [Parameter] public bool Resizable { get; set; }
    
    private bool IsIndeterminate => !EveryItem && SomeItems;

     protected static Block Block = new("m-data-table-header");
    private ModifierBuilder _modifierBuilder = Block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("mobile", IsMobile).Build();
    }

    protected string GetHeaderClass(DataTableHeader header)
    {
        CssBuilder stringBuilder = new();

        if (!DisableSort && header.Sortable)
        {
            var sortIndex = Options.SortBy.IndexOf(header.Value);
            var beingSorted = sortIndex >= 0;
            var isDesc = beingSorted && Options.SortDesc.ElementAtOrDefault(sortIndex);

            stringBuilder.Add("sortable");
            if (header.Divider)
            {
                stringBuilder.Add(" m-data-table__divider");
            }

            if (beingSorted)
            {
                stringBuilder.Add("active");
            }

            if (beingSorted)
            {
                stringBuilder.Add(isDesc ? "desc" : "asc");
            }
        }

        stringBuilder.Add($"text-{header.Align.ToString().ToLower()}");

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

        stringBuilder.Add(header.Class);

        return stringBuilder.ToString();
    }

    protected string GetHeaderStyle(DataTableHeader header)
    {
        var styleBuilder = StyleBuilder.Create()
            .AddWidth(header.Width)
            .AddMinWidth(header.Width);

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

    protected string? GetSelectChipsClass(DataTableHeader header)
    {
        if (header.Text is null && header.Value is null) return null;

        var sortIndex = Options.SortBy.IndexOf(header.Value);
        var beingSorted = sortIndex >= 0;
        var isDesc = Options.SortDesc.ElementAtOrDefault(sortIndex);

        StringBuilder stringBuilder = new("m-chip__close sortable");
        if (beingSorted)
        {
            stringBuilder.Append(" active");
        }

        if (beingSorted && !isDesc)
        {
            stringBuilder.Append(" asc");
        }

        if (beingSorted && isDesc)
        {
            stringBuilder.Append(" desc");
        }

        return stringBuilder.ToString();
    }
}