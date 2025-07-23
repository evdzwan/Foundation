using Foundation.Protocol;
using System.Collections.Concurrent;

namespace Foundation.Collections;

sealed class AsyncCollection<TItem>(Func<Page, CancellationToken, Task<TItem[]>> getView) : IAsyncCollection<TItem>
{
    readonly ConcurrentDictionary<int, Task<TItem[]>> Windows = [];
    const int WindowSize = 50;

    public bool Complete { get; private set; }

    public int Count
    {
        get
        {
            if (Windows.IsEmpty)
            {
                return 0;
            }

            var maxWindowKey = Windows.Keys.Max();
            return maxWindowKey * WindowSize + Windows[maxWindowKey].Result.Length + (Complete ? 0 : WindowSize);
        }
    }

    public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        for (var i = 0; ; i++)
        {
            var items = await GetView(new(Skip: i * WindowSize, Take: WindowSize), cancellationToken);
            if (items.Length == 0)
            {
                yield break;
            }

            foreach (var item in items)
            {
                yield return item;
            }
        }
    }

    public Task<TItem[]> GetValue(CancellationToken cancellationToken = default)
        => this.ToArray(cancellationToken);

    public async Task<TItem[]> GetView(Page page, CancellationToken cancellationToken = default)
    {
        var skip = page.Skip;
        var take = page.Take;

        var windowStart = (int)Math.Floor(skip / (WindowSize * 1d));
        var windowCount = (int)Math.Ceiling((skip + take) / (WindowSize * 1d)) - windowStart;

        var items = new List<TItem>();
        foreach (var i in Enumerable.Range(windowStart, windowCount))
        {
            var viewItems = await Windows.GetOrAdd(i, j => getView(page with { Skip = j * WindowSize, Take = WindowSize }, cancellationToken));
            items.AddRange(viewItems);

            if (viewItems.Length < WindowSize)
            {
                Complete = true;
                break;
            }
        }

        return [.. items.Skip(skip - windowStart * WindowSize).Take(take)];
    }

    public void Reset()
    {
        Complete = false;
        Windows.Clear();
    }
}
