using Microsoft.CodeAnalysis;

namespace Foundation;

abstract class Builder
{
    public abstract void BuildSource(SourceProductionContext context);

    protected static string? GetAccessibilityName(ISymbol symbol) => $"{symbol.DeclaredAccessibility switch
    {
        Accessibility.Private => "private",
        Accessibility.Protected => "protected",
        Accessibility.Internal => "internal",
        Accessibility.ProtectedAndInternal => "private protected",
        Accessibility.ProtectedOrInternal => "protected internal",
        Accessibility.Public => "public",
        _ => null
    }}{(symbol.IsSealed ? " sealed" : null)}";

    protected static string GetTypeName(ITypeSymbol type) => type switch
    {
        { NullableAnnotation: NullableAnnotation.Annotated } => $"{GetTypeName(type.WithNullableAnnotation(NullableAnnotation.None))}?",
        INamedTypeSymbol { IsGenericType: true, Name: "Nullable" } nullableType => GetTypeName(nullableType.TypeArguments[0]),
        INamedTypeSymbol { IsGenericType: true } genericType => $"{genericType.Name}<{string.Join(", ", genericType.TypeArguments.Select(GetTypeName))}>",
        IArrayTypeSymbol arrayType => $"{GetTypeName(arrayType.ElementType)}[]",
        { Name: "Boolean" } => "bool",
        { Name: "Byte" } => "byte",
        { Name: "Char" } => "char",
        { Name: "Decimal" } => "decimal",
        { Name: "Double" } => "double",
        { Name: "Int16" } => "short",
        { Name: "Int32" } => "int",
        { Name: "Int64" } => "long",
        { Name: "Object" } => "object",
        { Name: "SByte" } => "sbyte",
        { Name: "Single" } => "float",
        { Name: "String" } => "string",
        { Name: "UInt16" } => "ushort",
        { Name: "UInt32" } => "uint",
        { Name: "UInt64" } => "ulong",
        { Name: "Void" } => "void",
        _ => type.Name
    };

    protected static INamespaceSymbol[] GetNamespaces(ITypeSymbol type) => type switch
    {
        INamedTypeSymbol { IsGenericType: true } genericType => [genericType.ContainingNamespace, .. genericType.TypeArguments.SelectMany(GetNamespaces)],
        IArrayTypeSymbol arrayType => [arrayType.ElementType.ContainingNamespace],
        _ => [type.ContainingNamespace]
    };

    protected static string[] GetAndSortUsings(ITypeSymbol type, string[]? additionalNamespaces)
    {
        var propertyNamespaces = type.GetMembers()
                                     .OfType<IPropertySymbol>()
                                     .SelectMany(property => GetNamespaces(property.Type));

        var methodNamespaces = type.GetMembers()
                                   .OfType<IMethodSymbol>()
                                   .SelectMany(method => GetNamespaces(method.ReturnType).Concat(method.Parameters.SelectMany(parameter => GetNamespaces(parameter.Type))));

        return [.. propertyNamespaces.Concat(methodNamespaces)
                                     .OfType<INamespaceSymbol>()
                                     .Select(ns => ns.ToDisplayString())
                                     .Concat(additionalNamespaces ?? [])
                                     .Select(ns => $"using {ns};")
                                     .OrderBy(ns => ns)
                                     .Distinct()];
    }
}
