using System.Reflection;

namespace Foundation;

public interface IFoundationConfiguration
{
    IFoundationConfiguration Scan(IEnumerable<Type> types);
    IFoundationConfiguration Scan(IEnumerable<Assembly> assemblies)
        => Scan(assemblies.SelectMany(asm => asm.GetTypes()));
}
