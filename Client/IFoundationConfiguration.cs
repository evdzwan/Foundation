using System.Reflection;

namespace Foundation;

public interface IFoundationConfiguration
{
    IFoundationConfiguration AddMiddleware(Type middlewareType);
    IFoundationConfiguration AddMiddleware<TMiddleware>() where TMiddleware : IMiddleware
        => AddMiddleware(typeof(TMiddleware));

    IFoundationConfiguration ScanTypes(IEnumerable<Type> types);
    IFoundationConfiguration ScanAssemblies(IEnumerable<Assembly> assemblies)
        => ScanTypes(assemblies.SelectMany(asm => asm.GetTypes()));
}
