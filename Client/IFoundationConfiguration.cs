using Microsoft.Extensions.DependencyInjection;

namespace Foundation;

public interface IFoundationConfiguration
{
    internal IServiceCollection Services { get; }
}
