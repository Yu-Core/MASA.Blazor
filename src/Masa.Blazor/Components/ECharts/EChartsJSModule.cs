﻿namespace Masa.Blazor;

public class EChartsJSModule : JSModule
{
    public EChartsJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/wrappers/echarts.js")
    {
    }

    public async ValueTask<IEChartsJSObjectReferenceProxy?> Init(
        ElementReference el,
        string? theme,
        EChartsInitOptions options,
        IEChartsJsCallbacks owner
    )
    {
        try
        {
            var obj = await InvokeAsync<IJSObjectReference?>("init", el, theme, options);
            return new EChartsJSObjectReferenceProxy(obj!, owner);
        }
        catch (JSException e)
        {
            if (e.ForCannotCreateFromNullOrUndefined())
            {
                return null;
            }

            throw;
        }
    }
}
