﻿using Masa.Blazor.Components.Tabs;
using Masa.Blazor.JSModules;

namespace Masa.Blazor
{
    public partial class MTabs
    {
        [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

        [Inject] protected IResizeJSModule ResizeJSModule { get; set; } = null!;

        [Parameter] public string? ActiveClass { get; set; }

        [Parameter] public bool AlignWithTitle { get; set; }

        [Parameter] public string? BackgroundColor { get; set; }

        [Parameter] public bool CenterActive { get; set; }

        [Parameter] public bool Centered { get; set; }

        [Parameter] public bool FixedTabs { get; set; }

        [Parameter] public bool Grow { get; set; }

        [Parameter] public StringNumber? Height { get; set; }

        [Parameter] public bool IconsAndText { get; set; }

        // [Parameter]
        // public StringNumber? MobileBreakpoint { get; set; }

        [Parameter] public bool Right { get; set; }

        [Parameter] public virtual bool HideSlider { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public bool Optional { get; set; }

        [Parameter] public string? SliderColor { get; set; }

        [Parameter] [MasaApiParameter(2)] public StringNumber SliderSize { get; set; } = 2;
        [Parameter] public StringNumber? Value { get; set; }

        [Parameter] public EventCallback<StringNumber?> ValueChanged { get; set; }

        [Parameter] public bool Vertical { get; set; }

        [Parameter] public string? NextIcon { get; set; }

        [Parameter] public string? PrevIcon { get; set; }

        [Parameter] public StringBoolean? ShowArrows { get; set; }

        [Parameter] public bool Routable { get; set; }

        private int _registeredTabItemsIndex;
        private CancellationTokenSource? _callSliderCts;
        private ElementReference _sliderWrapperRef;
        private bool _isFirstRender = true;

        private List<ITabItem> TabItems { get; set; } = new();

        private object? TabsBarRef { get; set; }

        protected (StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width)
            Slider { get; set; }

        protected bool RTL => MasaBlazor.RTL;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.RTLChanged += MasaBlazorOnRTLChanged;
        }

        private void MasaBlazorOnRTLChanged(object? sender, EventArgs e)
        {
            InvokeAsync(CallSlider);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await ResizeJSModule.ObserverAsync(Ref, OnResize);
                await IntersectJSModule.ObserverAsync(Ref, OnIntersectAsync);
                _isFirstRender = false;
                await CallSlider();
            }
        }

        private static Block _block = new("m-tabs");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add(AlignWithTitle)
                .Add(Centered)
                .Add(FixedTabs)
                .Add(Grow)
                .Add(IconsAndText)
                .Add(Right)
                .Add(Vertical)
                .AddTheme(ComputedTheme)
                .Build();
        }

        private async Task OnIntersectAsync(IntersectEventArgs e)
        {
            if (e.IsIntersecting)
            {
                await InvokeAsync(CallSlider);
            }
        }

        private async Task OnValueChanged(StringNumber? val)
        {
            if (Value == val)
            {
                return;
            }
            Value = val;
            await ValueChanged.InvokeAsync(val);
        }

        public bool IsReversed => RTL && Vertical;

        public MItemGroup? Instance => TabsBarRef as MItemGroup;

        public void RegisterTabItem(ITabItem tabItem)
        {
            tabItem.Value ??= _registeredTabItemsIndex++;

            if (TabItems.Any(item => item.Value != null && item.Value.Equals(tabItem.Value))) return;

            TabItems.Add(tabItem);
        }

        public void UnregisterTabItem(ITabItem tabItem)
        {
            TabItems.Remove(tabItem);
        }

        /// <summary>
        /// Re-render slider immediately. For the case of deleting tabs,
        /// it is recommended to use <see cref="CallSliderAfterRender"/>.
        /// </summary>
        [MasaApiPublicMethod]
        public async Task CallSlider()
        {
            if (HideSlider || _isFirstRender) return;

            var item = Instance?.Items.FirstOrDefault(item => item.InternalIsActive);
            await Js.InvokeVoidAsync(
                JsInteropConstants.UpdateTabSlider,
                _sliderWrapperRef,
                item?.Ref,
                SliderSize.TryGetNumber().number,
                Vertical,
                IsReversed);
        }

        private async Task OnResize()
        {
            if (IsDisposed)
            {
                return;
            }

            await CallSlider();
        }

        /// <summary>
        /// Re-render slider in <see cref="OnAfterRenderAsync"/>
        /// </summary>
        [MasaApiPublicMethod]
        public void CallSliderAfterRender()
        {
            NextTick(CallSlider);
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            MasaBlazor.RTLChanged -= MasaBlazorOnRTLChanged;
            await IntersectJSModule.UnobserveAsync(Ref);
            await ResizeJSModule.UnobserveAsync(Ref);
        }
    }
}