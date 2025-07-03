using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

sealed class FoundationConfiguration(IServiceCollection services) : IFoundationConfiguration
{
    public IServiceCollection Services { get; } = services;
}
