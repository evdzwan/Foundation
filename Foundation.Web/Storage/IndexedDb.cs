using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Foundation.Storage;

public interface IDbStore<TItem>
{
}

public interface IDbBuilder
{
    IDbStoreBuilder<TItem> AddStore<TItem>(Expression<Func<TItem, object?>> keySelector, Action<IDbStoreBuilder<TItem>> configure);
    IDbStoreBuilder<TItem> AddStore<TItem>(string name, Expression<Func<TItem, object?>> keySelector, Action<IDbStoreBuilder<TItem>> configure);
}

public interface IDbStoreBuilder<TItem>
{
    IDbStoreBuilder<TItem> AddIndex(Expression<Func<TItem, object?>> keySelector, bool unique);
    IDbStoreBuilder<TItem> AddIndex(string name, Expression<Func<TItem, object?>> keySelector, bool unique);
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
        db.AddStore<User>(user => user.Id, store =>
        {
            store.AddIndex(user => user.Name, unique: false)
                 .AddIndex(user => user.Email, unique: true);
        });
    });

    public static void Use(IServiceProvider sp)
    {
    }

    record User(Guid Id, string Name, string Email);
}
