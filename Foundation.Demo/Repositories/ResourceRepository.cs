using Foundation.Components;
using System.Collections.Concurrent;

namespace Foundation.Repositories;

static class ResourceRepository
{
    readonly static ConcurrentDictionary<string, Task<string?>> Resources = new(StringComparer.OrdinalIgnoreCase);

    public static Task<string?> GetResource(string resource, CancellationToken cancellationToken = default)
        => Resources.GetOrAdd(resource, resource => LoadResource(resource, cancellationToken));

    static async Task<string?> LoadResource(string resource, CancellationToken cancellationToken)
    {
        var streamName = $"Foundation.Examples.{resource}".Replace('\\', '.');
        await using var stream = typeof(Example).Assembly.GetManifestResourceStream(streamName);
        if (stream is { CanRead: true })
        {
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync(cancellationToken);
        }

        return null;
    }
}
