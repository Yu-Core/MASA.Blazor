﻿using Masa.Blazor.Mixins.ScrollStrategy;
using Element = BemIt.Element;

namespace Masa.Blazor;

public partial class MOverlay : IThemeable
{
    [Inject] private ScrollStrategyJSModule ScrollStrategyJSModule { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public bool Value
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Contained { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool Eager { get; set; }

    [Parameter] public StringNumber? Opacity { get; set; }

    [Parameter] public string? ScrimClass { get; set; }

    [Parameter] [MasaApiParameter("v1.8.0")] public string? ScrimStyle { get; set; }

    [Parameter] [MasaApiParameter(5)] public int ZIndex { get; set; } = 5;

    private static readonly Block _block = new("m-overlay");
    private readonly ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private readonly ModifierBuilder _scrimModifierBuilder = _block.Element("scrim").CreateModifierBuilder();
    private static readonly Element _content = _block.Element("content");

    private bool _booted;

    public ElementReference ContentRef { get; private set; }

    private string? ComputedColor => Color ?? MasaBlazor.Theme.CurrentTheme.OnSurface;

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Value), ValueChangeCallback);
    }

    private async Task ValueChangeCallback()
    {
        if (Value)
        {
            _ = NextTickIf(async () => { await HideScroll(); }, () => ContentRef.Context is null);
        }
        else
        {
            await ShowScroll();
        }
    }

    private bool _isActive;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!_booted)
        {
            _booted = true;
            _isActive = Value;
        }
        else
        {
            _isActive = Value;
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("active", Value)
            .Add("absolute", Absolute || Contained)
            .Add(Contained)
            .AddTheme(ComputedTheme)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return $"z-index: {ZIndex}";
        if (Opacity != null)
        {
            yield return $"--m-overlay-opacity: {Opacity.TryGetNumber().number}";
        }
    }

    private ScrollStrategyResult? _scrollStrategyResult;

    private async Task HideScroll()
    {
        if (_scrollStrategyResult is null)
        {
            _scrollStrategyResult =
                await ScrollStrategyJSModule.CreateScrollStrategy(Ref, ContentRef,
                    new(ScrollStrategy.Block, Contained));
        }
        else
        {
            _scrollStrategyResult.Bind?.Invoke();
        }
    }

    private async Task ShowScroll()
    {
        _scrollStrategyResult?.Unbind?.Invoke();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _scrollStrategyResult?.Unbind?.Invoke();
        _scrollStrategyResult?.Dispose?.Invoke();
    }
}