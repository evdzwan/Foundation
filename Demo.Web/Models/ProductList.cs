namespace Foundation.Models;

sealed record ProductList(bool Loading, Product[] Products) : ICreateNew<ProductList>
{
    public static ProductList CreateNew()
        => new(Loading: false, Products: []);
}
