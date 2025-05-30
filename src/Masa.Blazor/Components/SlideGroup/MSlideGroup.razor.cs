﻿using Masa.Blazor.JSModules;
using Masa.Blazor.Mixins;

namespace Masa.Blazor
{
    public partial class MSlideGroup : MItemGroup
    {
        public MSlideGroup()
        {
            GroupType = GroupType.SlideGroup;
        }

        [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject] protected IResizeJSModule ResizeJSModule { get; set; } = null!;

        [Parameter] public bool CenterActive { get; set; }

        [Parameter] public StringBoolean? ShowArrows { get; set; }

        [Parameter] public string? NextIcon { get; set; }

        [Parameter] public RenderFragment? NextContent { get; set; }

        [Parameter] public string? PrevIcon { get; set; }

        [Parameter] public RenderFragment? PrevContent { get; set; }

        private int _prevItemsLength;
        private StringNumber? _prevInternalValue;
        private bool _prevIsOverflowing;
        private CancellationTokenSource? _cts;
        private bool _firstRendered;

        protected bool RTL => MasaBlazor.RTL;

        protected bool IsMobile { get; set; }

        protected ElementReference WrapperRef { get; set; }

        protected ElementReference ContentRef { get; set; }

        protected double WrapperWidth { get; set; }

        protected double ContentWidth { get; set; }

        protected double ScrollOffset { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.MobileChanged += MasaBlazorOnMobileChanged;
            IsMobile = MasaBlazor.Breakpoint.Mobile;
        }

        private async void MasaBlazorOnMobileChanged(object? sender, MobileChangedEventArgs e)
        {
            IsMobile = e.Mobile;
            await InvokeStateHasChangedAsync();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ActiveClass ??= "m-slide-item--active";
            NextIcon ??= "$next";
            PrevIcon ??= "$prev";

            if (MasaBlazor.RTL)
            {
                (NextIcon, PrevIcon) = (PrevIcon, NextIcon);
            }
        }

        private static Block _block = new("m-slide-group");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat([
                _modifierBuilder.Add(IsOverflowing)
                    .Add(HasAffixes)
                    .Build()
            ]);
        }

        private string GetContentClass()
        {
            return string.Join(" ", BuildContentClass());
        }

        protected virtual IEnumerable<string> BuildContentClass()
        {
            yield return _block.Element("content").Name;
        }

        protected void UpdateScrollOffset(double val)
        {
            ScrollOffset = val;
            
            if (RTL)
            {
                val = -val;
            }

            var scroll = val <= 0
                ? Bias(-val)
                : val > ContentWidth - WrapperWidth
                    ? -(ContentWidth - WrapperWidth) + Bias(ContentWidth - WrapperWidth - val)
                    : -val;

            if (RTL)
            {
                scroll = -scroll;
            }

            if (WrapperRef.Context != null)
            {
                _ = Js.ScrollTo(WrapperRef, 0, -scroll).ConfigureAwait(false);
            }
        }

        private double Bias(double val)
        {
            var c = 0.501;
            var x = Math.Abs(val);
            return Math.Sign(val) * (x / ((1 / c - 2) * (1 - x) + 1));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _firstRendered = true;
                await ResizeJSModule.ObserverAsync(Ref, OnResize);
            }

            if (_firstRendered)
            {
                var needSetWidths = false;
                StringNumber? value = null;
                
                if (_prevItemsLength != Items.Count)
                {
                    _prevItemsLength = Items.Count;
                    value = Value;
                    needSetWidths = true;
                }

                if (_prevInternalValue != InternalValue)
                {
                    _prevInternalValue = InternalValue;
                    value = InternalValue;
                    needSetWidths = !ToggleButUnselect;
                }

                if (_prevIsOverflowing != IsOverflowing)
                {
                    _prevIsOverflowing = IsOverflowing;
                    // Make sure the value of WrapperWidth is after IsOverflowing takes effect.
                    StateHasChanged();

                    await GetWidths(true);
                    value = Value;
                    needSetWidths = true;
                }

                if (needSetWidths)
                {
                    await SetWidths(value);
                }
            }

            ToggleButUnselect = false;
        }

        public async Task SetWidths(StringNumber? selectedValue = null, bool getWidthsForce = false)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            await RunTaskInMicrosecondsAsync(async () =>
            {
                await GetWidths(getWidthsForce);
                IsOverflowing = WrapperWidth + 1 < ContentWidth;
                await ScrollToView(selectedValue);
                StateHasChanged();
            }, 16, _cts.Token);
        }

        private async Task GetWidths(bool force = false)
        {
            if (WrapperWidth == 0 || force)
            {
                WrapperWidth = await Js.InvokeAsync<double>(JsInteropConstants.GetProp, WrapperRef, "clientWidth");
            }

            if (ContentWidth == 0 || force)
            {
                ContentWidth = await Js.InvokeAsync<double>(JsInteropConstants.GetProp, ContentRef, "clientWidth");
            }
        }

        public bool IsOverflowing { get; protected set; }

        public bool HasAffixes
        {
            get
            {
                var hasAffixes = !IsMobile && (IsOverflowing || Math.Abs(ScrollOffset) > 0);

                if (ShowArrows is null) return hasAffixes;

                return ShowArrows.Match(
                    str =>
                    {
                        return str switch
                        {
                            "always" => true, // Always show arrows on desktop & mobile
                            "desktop" => !IsMobile, // Always show arrows on desktop
                            "mobile" => IsMobile ||
                                        (IsOverflowing ||
                                         Math.Abs(ScrollOffset) > 0), // Show arrows on mobile when overflowing.
                            _ => hasAffixes
                        };
                    },
                    @bool =>
                    {
                        return @bool switch
                        {
                            true => IsOverflowing || Math.Abs(ScrollOffset) > 0, // Always show on mobile
                            _ => hasAffixes
                        };
                    });
            }
        }

        public bool HasNext => HasAffixes && (ContentWidth > Math.Abs(ScrollOffset) + WrapperWidth);

        public bool HasPrev => HasAffixes && ScrollOffset != 0;

        private async Task HandleOnAffixClick(string direction)
        {
            await ScrollTo(direction);
        }

        protected async Task ScrollToView(StringNumber? selectedValue)
        {
            if (selectedValue == null && Items.Any())
            {
                var lastItemRef = Items[^1].Ref;
                if (lastItemRef.Context == null) return;

                var lastItemPosition =
                    await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, lastItemRef);
                var wrapperPosition =
                    await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, WrapperRef);

                if ((RTL && wrapperPosition.Right < lastItemPosition.Right) ||
                    (!RTL && wrapperPosition.Left > lastItemPosition.Left))
                {
                    await ScrollTo("prev");
                }
            }

            if (selectedValue == null) return;

            var selectedItem = Items.FirstOrDefault(item => item.Value == selectedValue);
            if (selectedItem?.Ref.Context == null) return;

            if (Items.FindIndex(u => u.Value == selectedValue) == 0 || (!CenterActive && !IsOverflowing))
            {
                UpdateScrollOffset(0);
            }
            else if (CenterActive)
            {
                UpdateScrollOffset(await CalculateCenteredOffset(selectedItem.Ref, WrapperWidth, ContentWidth, RTL));
            }
            else if (IsOverflowing)
            {
                UpdateScrollOffset(await CalculateUpdatedOffset(selectedItem.Ref, WrapperWidth, ContentWidth, RTL, ScrollOffset));
            }
        }

        protected async Task<double> CalculateUpdatedOffset(ElementReference selected, double wrapperWidth,
            double contentWidth, bool rtl,
            double currentScrollOffset)
        {
            var selectedDomInfo =
                await Js.InvokeAsync<Masa.Blazor.JSInterop.Element>(JsInteropConstants.GetDomInfo, selected);
            var clientWidth = selectedDomInfo.ClientWidth;
            var offsetLeft =
                rtl ? (contentWidth - selectedDomInfo.OffsetLeft - clientWidth) : selectedDomInfo.OffsetLeft;

            if (rtl)
            {
                currentScrollOffset = -currentScrollOffset;
            }

            var totalWidth = wrapperWidth + currentScrollOffset;
            var itemOffset = clientWidth + offsetLeft;
            var additionalOffset = clientWidth * 0.4;

            if (offsetLeft <= currentScrollOffset)
            {
                currentScrollOffset = Math.Max(offsetLeft - additionalOffset, 0);
            }
            else if (totalWidth <= itemOffset)
            {
                currentScrollOffset = Math.Min(currentScrollOffset - (totalWidth - itemOffset - additionalOffset),
                    contentWidth - wrapperWidth);
            }

            return rtl ? -currentScrollOffset : currentScrollOffset;
        }

        protected async Task<double> CalculateCenteredOffset(ElementReference selected, double wrapperWidth,
            double contentWidth, bool rtl)
        {
            var selectedDomInfo =
                await Js.InvokeAsync<Masa.Blazor.JSInterop.Element>(JsInteropConstants.GetDomInfo, selected);
            var offsetLeft = selectedDomInfo.OffsetLeft;
            var clientWidth = selectedDomInfo.ClientWidth;

            if (rtl)
            {
                var offsetCentered = contentWidth - offsetLeft - clientWidth / 2 - wrapperWidth / 2;
                return -Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
            else
            {
                var offsetCentered = offsetLeft + clientWidth / 2 - wrapperWidth / 2;
                return Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
        }

        protected async Task ScrollTo(string direction)
        {
            await GetWidths();
            UpdateScrollOffset(CalculateNewOffset(direction, WrapperWidth, ContentWidth, RTL, ScrollOffset));
        }

        protected static double CalculateNewOffset(string direction, double wrapperWidth, double contentWidth, bool rtl,
            double currentScrollOffset)
        {
            var sign = rtl ? -1 : 1;

            var newAbsoluteOffset = sign * currentScrollOffset + (direction == "prev" ? -1 : 1) * wrapperWidth;

            return sign * Math.Max(Math.Min(newAbsoluteOffset, contentWidth - wrapperWidth), 0);
        }

        protected async Task OnResize()
        {
            if (IsDisposed)
            {
                return;
            }

            await SetWidths(getWidthsForce: true);
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            MasaBlazor.MobileChanged -= MasaBlazorOnMobileChanged;
            await ResizeJSModule.UnobserveAsync(Ref);
        }
    }
}