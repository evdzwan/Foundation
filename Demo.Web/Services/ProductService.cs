using Foundation.Models;
using System.Diagnostics.CodeAnalysis;

namespace Foundation.Services;

sealed class ProductService
{
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Demo")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Demo")]
    public async Task<Product[]> GetProducts(int skip, int take, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        return [.. Enumerable.Range(1, 1000)
                             .Skip(skip)
                             .Take(take)
                             .Select(i => new Product(i, $"Product {i}"))];
    }
}
