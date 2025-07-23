using Microsoft.CodeAnalysis;

namespace Foundation.Scripting;

sealed class ScriptBuilder(INamedTypeSymbol scriptType, string scriptPath) : Builder
{
    public override void BuildSource(SourceProductionContext context)
    {
        context.AddSource($"{scriptType.ContainingNamespace.Name}/{scriptType.Name}.g.cs", $$"""
        #nullable enable

        {{string.Join("\r\n", GetAndSortUsings(scriptType, ["Foundation.Scripting", "Microsoft.JSInterop"]))}}

        namespace {{scriptType.ContainingNamespace}};

        /// <remarks>
        /// Interop for <c>{{scriptPath}}</c>
        /// </remarks>
        {{GetAccessibilityName(scriptType)}} partial class {{scriptType.Name}}(IJSRuntime jsRuntime) : Script(@"{{scriptPath}}", jsRuntime)
        {
        {{string.Join("\r\n\r\n", GetMethods(scriptType))}}
        }

        """);
    }

    static IEnumerable<string> GetMethods(ITypeSymbol type)
        => type.GetMembers()
               .OfType<IMethodSymbol>()
               .Where(method => method.IsPartialDefinition)
               .Select(GetMethod);

    static string GetMethod(IMethodSymbol method)
    {
        var cancellationTokenParameterName = method.Parameters.SingleOrDefault(p => p is { Type.Name: "CancellationToken" })?.Name;
        var jsMethodName = $"{method.Name[0].ToString().ToLowerInvariant()}{method.Name.Substring(1)}";

        return $$"""
            {{GetAccessibilityName(method)}} partial {{GetTypeName(method.ReturnType)}} {{GetMethodDeclaration(method)}}({{string.Join(", ", GetParameters(method))}}){{GetMethodConstraints(method)}}
                => {{GetMethodInvocation(method)}}("{{jsMethodName}}", [{{string.Join(", ", GetParameterInvocations(method))}}]{{(cancellationTokenParameterName is not null ? $", {cancellationTokenParameterName}" : null)}});
        """;
    }

    static string GetMethodConstraints(IMethodSymbol method) => string.Concat(method.TypeParameters.Select(typeParameter =>
    {
        var constraints = new List<string>();
        if (typeParameter.HasReferenceTypeConstraint)
            constraints.Add("class");

        if (typeParameter.HasValueTypeConstraint)
            constraints.Add("struct");

        if (typeParameter.HasConstructorConstraint)
            constraints.Add("new()");

        foreach (var constraintType in typeParameter.ConstraintTypes)
        {
            constraints.Add(GetTypeName(constraintType));
        }

        return $" where {typeParameter.Name} : {string.Join(", ", constraints)}";
    }));

    static string GetMethodDeclaration(IMethodSymbol method)
        => $"{method.Name}{(method.TypeArguments.Length > 0 ? $"<{string.Join(", ", method.TypeArguments.Select(GetTypeName))}>" : null)}";

    static string GetMethodInvocation(IMethodSymbol method) => method.ReturnType switch
    {
        INamedTypeSymbol { IsGenericType: true } namedType => $"Invoke<{string.Join(", ", namedType.TypeArguments.Select(GetTypeName))}>",
        _ => "Invoke"
    };

    static IEnumerable<string> GetParameters(IMethodSymbol method)
        => method.Parameters.Select(GetParameter);

    static string GetParameter(IParameterSymbol parameter)
        => $"{GetTypeName(parameter.Type)} {parameter.Name}";

    static IEnumerable<string> GetParameterInvocations(IMethodSymbol method)
        => method.Parameters.Where(parameter => parameter is { Type.Name: not "CancellationToken" })
                            .Select(GetParameterInvocation);

    static string GetParameterInvocation(IParameterSymbol parameter)
        => parameter.Name;
}
