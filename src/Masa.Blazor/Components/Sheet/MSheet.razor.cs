﻿using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MSheet
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] [MasaApiParameter("div")] public virtual string Tag { get; set; } = "div";

        [Parameter] public bool? Show { get; set; }

        [Parameter] public bool Outlined { get; set; }

        [Parameter] public bool Shaped { get; set; }

        [Parameter] public StringNumber? Elevation { get; set; }

        [Parameter] public StringBoolean? Rounded { get; set; }

        [Parameter] public bool Tile { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public StringNumber? Width { get; set; }

        [Parameter] public StringNumber? MaxWidth { get; set; }

        [Parameter] public StringNumber? MinWidth { get; set; }

        [Parameter] public StringNumber? Height { get; set; }

        [Parameter] public StringNumber? MaxHeight { get; set; }

        [Parameter] public StringNumber? MinHeight { get; set; }

        private static Block _block = new("m-sheet");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add(Outlined, Shaped)
                .AddTheme(ComputedTheme)
                .AddElevation(Elevation)
                .AddRounded(Rounded, Tile)
                .AddBackgroundColor(Color)
                .Build();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            return new StyleBuilder()
                .AddHeight(Height)
                .AddWidth(Width)
                .AddMinWidth(MinWidth)
                .AddMaxWidth(MaxWidth)
                .AddMinHeight(MinHeight)
                .AddMaxHeight(MaxHeight)
                .AddBackgroundColor(Color)
                .GenerateCssStyles();
        }
    }
}