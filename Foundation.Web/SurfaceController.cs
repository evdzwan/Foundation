using Microsoft.AspNetCore.Components;

namespace Foundation;

public sealed class SurfaceController : IDisposable
{
    event Action<RenderFragment>? FragmentAdded;
    event Action<RenderFragment>? FragmentRemoved;

    public IDisposable AddFragment(RenderFragment fragment)
    {
        FragmentAdded?.Invoke(fragment);
        return new Disposable(() => RemoveFragment(fragment));
    }

    public void RemoveFragment(RenderFragment fragment)
        => FragmentRemoved?.Invoke(fragment);

    internal IDisposable Subscribe(Action<RenderFragment> fragmentAdded, Action<RenderFragment> fragmentRemoved)
    {
        FragmentAdded += fragmentAdded;
        FragmentRemoved += fragmentRemoved;

        return new Disposable(() =>
        {
            FragmentAdded -= fragmentAdded;
            FragmentRemoved -= fragmentRemoved;
        });
    }

    void IDisposable.Dispose()
    {
        FragmentAdded = null;
        FragmentRemoved = null;
    }
}
