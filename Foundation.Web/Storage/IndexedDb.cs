using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Foundation.Storage;

public interface IDbStore<TItem> : IAsyncQueryable<TItem>
{
    string Name { get; }

    ValueTask AddAsync(TItem item);
    ValueTask DeleteAsync(object key);
    ValueTask<TItem?> FindAsync(object key);
    ValueTask UpdateAsync(object key, TItem item);
}

public interface IDbBuilder
{
    IDbStoreBuilder<TItem> AddStore<TItem>(Expression<Func<TItem, object>> keySelector, Action<IDbStoreBuilder<TItem>> configure, bool autoIncrement = false);
    IDbStoreBuilder<TItem> AddStore<TItem>(string name, Expression<Func<TItem, object>> keySelector, Action<IDbStoreBuilder<TItem>> configure, bool autoIncrement = false);
}

public interface IDbStoreBuilder<TItem>
{
    IDbStoreBuilder<TItem> AddIndex(Expression<Func<TItem, object>> keySelector, bool unique);
    IDbStoreBuilder<TItem> AddIndex(string name, Expression<Func<TItem, object>> keySelector, bool unique);
    IDbStoreBuilder<TItem> OnInitialize(Action<IDbStore<TItem>, IServiceProvider> callback);
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIndexedDb(this IServiceCollection @this, Action<IDbBuilder> configure)
        => throw new NotImplementedException();
}

static class Tester
{
    public static void Register(IServiceCollection services) => services.AddIndexedDb(db =>
    {
        db.AddStore<Customer>(customer => customer.Id, store =>
        {
            store.AddIndex(customer => customer.Name, unique: false)
                 .AddIndex(customer => customer.Email, unique: true);
        });

        db.AddStore<Product>(product => product.Id, store =>
        {
            store.AddIndex(product => product.Name, unique: false);
        }, autoIncrement: true);
    });

    public static async Task Use(IServiceProvider sp)
    {
        var customers = sp.GetRequiredService<IDbStore<Customer>>();
        await foreach (var customer in customers.Where(c => c.Name.Contains("Henk")))
        {
            Console.WriteLine(customer);
        }

        var products = sp.GetRequiredService<IDbStore<Product>>();
        await products.AddAsync(new() { Name = "Coffee machine" });
        if (await products.LastOrDefaultAsync() is { } machine)
        {
            machine.Name = "Espresso machine";
            await products.UpdateAsync(key: 1, machine);
        }
    }

    record Customer(Guid Id, string Name, string Email);
    record Product
    {
        public int Id { get; set; } // auto-increment
        public required string Name { get; set; }
    }
}
