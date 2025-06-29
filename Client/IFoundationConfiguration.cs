using System.Reflection;

namespace Foundation;

public interface IFoundationConfiguration
{
    IFoundationConfiguration AddMiddleware(Type middlewareType);
    IFoundationConfiguration AddMiddleware<TMiddleware>() where TMiddleware : IMiddleware
        => AddMiddleware(typeof(TMiddleware));

    IFoundationConfiguration Scan(IEnumerable<Type> types);
    IFoundationConfiguration Scan(IEnumerable<Assembly> assemblies)
        => Scan(assemblies.SelectMany(asm => asm.GetTypes()));
}
