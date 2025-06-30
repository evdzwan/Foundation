using Foundation.Commands;
using Foundation.Models;

namespace Foundation.Mutators;

sealed class ProductMutators : IMutator<ProductList, LoadProducts>,
                               IMutator<ProductList, LoadProductsResult>
{
    public ProductList On(ProductList model, LoadProducts command)
        => new(Loading: true, Products: []);

    public ProductList On(ProductList model, LoadProductsResult command)
        => new(Loading: false, Products: command.Products);
}
