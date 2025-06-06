﻿namespace Masa.Blazor;

public partial class MSimpleCheckbox
{
    [Parameter] public string? Color { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Value { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public bool Indeterminate { get; set; }

    [Parameter]
    [MasaApiParameter("$checkboxIndeterminate")]
    public string IndeterminateIcon { get; set; } = "$checkboxIndeterminate";

    [Parameter]
    [MasaApiParameter("$checkboxOn")]
    public string OnIcon { get; set; } = "$checkboxOn";

    [Parameter]
    [MasaApiParameter("$checkboxOff")]
    public string OffIcon { get; set; } = "$checkboxOff";

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Ripple { get; set; } = true;

    private static Block _block = new("m-simple-checkbox");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private static Block _selectionBlock = new("m-input--selection-controls");

    private string? ComputedColor => Disabled || !Value ? null : Color;

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Disabled).AddTextColor(ComputedColor).Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        yield return new StyleBuilder().AddTextColor(ComputedColor).ToString();
    }

    public string ComputedIcon
    {
        get
        {
            if (Indeterminate)
            {
                return IndeterminateIcon;
            }

            if (Value)
            {
                return OnIcon;
            }

            return OffIcon;
        }
    }

    public virtual async Task HandleOnClickAsync(MouseEventArgs args)
    {
        if (Disabled || Readonly)
        {
            return;
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(!Value);
        }
        else
        {
            Value = !Value;
        }
    }
}