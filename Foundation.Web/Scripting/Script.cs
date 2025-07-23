using Microsoft.JSInterop;

namespace Foundation.Scripting;

public abstract class Script(string path, IJSRuntime jsRuntime) : IAsyncDisposable
{
    IJSObjectReference? Module;

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
            Module ??= await jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, [path]);
        }
        catch (JSDisconnectedException) { }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        GC.SuppressFinalize(this);
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
