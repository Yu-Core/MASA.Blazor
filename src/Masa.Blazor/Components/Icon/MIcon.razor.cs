﻿using Masa.Blazor.Components.Transition;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MIcon : ThemeComponentBase
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public RenderFragment? ChildContent { get; set; }

        // TODO: Rename to TransitionIf in the next major version
        [Parameter] public bool If { get; set; } = true;

        [Parameter] public bool TransitionShow { get; set; } = true;

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public Icon? Icon { get; set; }

        [Parameter] public bool Left { get; set; }

        [Parameter] public bool Right { get; set; }

        [Parameter] public StringNumber? Size { get; set; }

        [Parameter] [MasaApiParameter("i")] public string? Tag { get; set; } = "i";

        [Parameter] public Dictionary<string, object>? SvgAttributes { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public bool OnClickPreventDefault { get; set; }

        [Parameter] public bool OnClickStopPropagation { get; set; }

        [Parameter] public bool OnMouseupPreventDefault { get; set; }

        [Parameter] public bool OnMouseupStopPropagation { get; set; }

        /// <summary>
        /// 36px
        /// </summary>
        [Parameter]
        public bool Large { get; set; }

        /// <summary>
        /// 16px
        /// </summary>
        [Parameter]
        public bool Small { get; set; }

        /// <summary>
        /// 40px
        /// </summary>
        [Parameter]
        public bool XLarge { get; set; }

        /// <summary>
        /// 12px
        /// </summary>
        [Parameter]
        public bool XSmall { get; set; }

        private static Regex _reg1 = new(@"^[mzlhvcsqta]\s*[-+.0-9][^mlhvzcsqta]+", RegexOptions.IgnoreCase);
        private static Regex _reg2 = new(@"[\dz]$", RegexOptions.IgnoreCase);

        private static readonly Dictionary<string, object> s_defaultSvgAttrs = new()
        {
            { "viewBox", "0 0 24 24" },
            { "role", "img" },
            { "aria-hidden", "true" }
        };

        private readonly Dictionary<string, string> _sizeMap = new()
        {
            { nameof(XSmall), "12px" },
            { nameof(Small), "16px" },
            { "Default", "24px" },
            { nameof(Medium), "28px" },
            { nameof(Large), "36px" },
            { nameof(XLarge), "40px" },
        };

        private bool _clickEventRegistered;
        private long _clickEventId;

        private string? _iconCss;
        private bool _transitionValue;
        private ConditionType _transitionConditionType = ConditionType.None;

        public bool Medium => false;

        /// <summary>
        /// Icon from ChildContent
        /// </summary>
        protected string? IconContent { get; set; }

        private Icon? ComputedIcon { get; set; }

        private Dictionary<string, object> SvgAttrs
        {
            get
            {
                if (SvgAttributes is null) return s_defaultSvgAttrs;

                var attrs = new Dictionary<string, object>(SvgAttributes);

                foreach (var (k, v) in s_defaultSvgAttrs)
                {
                    attrs.TryAdd(k, v);
                }

                return attrs;
            }
        }

        protected void InitIcon()
        {
            Icon? icon;

            if (Icon is { IsSvg: true })
            {
                ComputedIcon = Icon;
                return;
            }

            var iconText = Icon is { IsT0: true } ? Icon.AsT0 : ChildContent?.GetTextContent();
            IconContent = iconText;

            if (string.IsNullOrWhiteSpace(iconText))
            {
                return;
            }

            if (iconText.StartsWith('$'))
            {
                icon = MasaBlazor.Icons.Aliases.GetIconOrDefault(iconText);
                
                // If the icon is not found, return
                if (icon is null)
                {
                    return;
                }
            }
            else
            {
                icon = IsSvgPath(iconText) ? new SvgPath(iconText) : iconText;
            }

            if (icon!.IsSvg)
            {
                ComputedIcon = icon;
            }
            else
            {
                (ComputedIcon, _iconCss) = ResolveIcon(icon.AsT0);
            }
        }

        private (string? icon, string? css) ResolveIcon(string cssIcon)
        {
            var defaultAliases = MasaBlazor.Icons.Aliases;

            var splits = cssIcon.Split(":");
            var icon = splits[0];

            if (splits.Length == 2)
            {
                defaultAliases = splits[0] switch
                {
                    "mdi" => DefaultIconAliases.MaterialDesignIcons,
                    "md" => DefaultIconAliases.MaterialDesign,
                    "fa6" => DefaultIconAliases.FontAwesome6,
                    "fa" => DefaultIconAliases.FontAwesome,
                    "fa4" => DefaultIconAliases.FontAwesome4,
                    _ => defaultAliases
                };
                icon = splits[1];
            }

            return (defaultAliases.ContentFormatter?.Invoke(icon), defaultAliases.CssFormatter?.Invoke(icon));
        }

        public string? GetSize()
        {
            var sizes = new Dictionary<string, bool>()
            {
                { nameof(XSmall), XSmall },
                { nameof(Small), Small },
                { nameof(Medium), Medium },
                { nameof(Large), Large },
                { nameof(XLarge), XLarge },
            };

            var key = sizes.FirstOrDefault(item => item.Value).Key;

            if (key != null && _sizeMap.TryGetValue(key, out var px))
            {
                return px;
            }

            return Size?.ToUnit();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            InitIcon();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (OnClick.HasDelegate)
            {
                Tag = "button";
            }

            if (IsDirtyParameter(nameof(If)))
            {
                _transitionValue = If;
                _transitionConditionType = ConditionType.If;
            }
            else if(IsDirtyParameter(nameof(TransitionShow)))
            {
                _transitionValue = TransitionShow;
                _transitionConditionType = ConditionType.Show;
            }
            else
            {
                _transitionValue = true;
                _transitionConditionType = ConditionType.None;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!_clickEventRegistered)
            {
                await TryRegisterClickEvent();
            }
        }

        protected override async Task OnElementReferenceChangedAsync()
        {
            await TryRegisterClickEvent();
        }

        public async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        private static Block _block = new("m-icon");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder.Add("link", OnClick.HasDelegate)
                .Add(Dense)
                .Add(Left)
                .Add(Disabled)
                .Add(Right)
                .AddTheme(ComputedTheme)
                .AddTextColor(Color)
                .AddClass(_iconCss, ComputedIcon is { IsSvg: false })
                .Build();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            var builder = StyleBuilder.Create().AddTextColor(Color);
            var fontSize = GetSize();
            if (fontSize != null)
            {
                builder.Add("font-size", fontSize);
            }

            return builder.GenerateCssStyles();
        }

        private async Task TryRegisterClickEvent()
        {
            if (Ref.Context is not null && OnClick.HasDelegate)
            {
                _clickEventRegistered = true;

                _clickEventId = await Js.AddHtmlElementEventListener<MouseEventArgs>(Ref, "click",
                    HandleOnClick, false,
                    new EventListenerExtras
                    {
                        PreventDefault = OnClickPreventDefault,
                        StopPropagation = OnClickStopPropagation
                    });
            }
        }

        /// <summary>
        /// Check if the string is a valid svg path
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSvgPath(string str)
        {
            return _reg1.Match(str).Success && _reg2.Match(str).Success && str.Length > 4;
        }

        protected override ValueTask DisposeAsyncCore()
        {
            if (_clickEventId != 0)
            {
                _clickEventRegistered = false;
                _ = Js.RemoveHtmlElementEventListener(_clickEventId);
            }

            return base.DisposeAsyncCore();
        }
    }
}