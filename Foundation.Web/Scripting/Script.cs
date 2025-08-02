using Microsoft.JSInterop;
using System.Diagnostics;

namespace Foundation.Scripting;

public abstract class Script(string path, IJSRuntime jsRuntime) : IAsyncDisposable
{
    IJSObjectReference? Module;

    public bool Loaded => Module is not null;

    internal async ValueTask<IJSObjectReference?> GetModuleOrDefault()
    {
        try
        {
            await LoadScript();
            return Module;
        }
        catch (JSException) { return null; }
    }

    protected async ValueTask Invoke(string identifier, object?[] args, CancellationToken cancellationToken = default)
    {
        await LoadScript(cancellationToken);
        if (Module is { } module)
        {
            try
            {
                await module.InvokeVoidAsync(identifier, cancellationToken, args);
            }
            catch (JSDisconnectedException) { }
        }
    }

    protected async ValueTask<TResult> Invoke<TResult>(string identifier, object?[] args, CancellationToken cancellationToken = default)
    {
        await LoadScript(cancellationToken);
        if (Module is { } module)
        {
            try
            {
                return await module.InvokeAsync<TResult>(identifier, cancellationToken, args);
            }
            catch (JSDisconnectedException) { }
        }

        return default!;
    }

    async ValueTask LoadScript(CancellationToken cancellationToken = default)
    {
        try
        {
            if (jsRuntime.GetType().GetProperty("IsInitialized") is { CanRead: true } property && property.GetValue(jsRuntime) is true)
            {
                Module ??= await jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, [path]);
            }
        }
        catch (JSDisconnectedException) { }
    }

    ValueTask IAsyncDisposable.DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return DisposeModule();
    }

    [DebuggerNonUserCode]
    async ValueTask DisposeModule()
    {
        if (Module is { } module)
        {
            try
            {
                await module.DisposeAsync();
            }
            catch { }

            Module = null;
        }
    }
}
