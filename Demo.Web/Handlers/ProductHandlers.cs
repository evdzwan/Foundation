using Foundation.Commands;
using Foundation.Services;

namespace Foundation.Handlers;

sealed class ProductHandlers(ProductService service) : ICommandHandler<LoadProducts>
{
    public async Task On(LoadProducts command, ICommandDispatcher dispatcher, CancellationToken cancellationToken)
        => dispatcher.Dispatch(new LoadProductsResult(await service.GetProducts(command.Skip, command.Take, cancellationToken)));
}
