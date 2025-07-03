using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Foundation.Scripting;

[Generator]
sealed class ScriptGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var scriptTypes = context.SyntaxProvider.ForAttributeWithMetadataName("Foundation.Scripting.ScriptAttribute",
            (node, _) => node is ClassDeclarationSyntax classDeclaration && classDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)),
            (ctx, _) => (INamedTypeSymbol)ctx.TargetSymbol);

        context.RegisterSourceOutput(scriptTypes, (ctx, scriptType) =>
        {
            var scriptPath = scriptType.GetAttributes()
                                       .Single(attr => attr is { AttributeClass.Name: "Script" or "ScriptAttribute" })
                                       .ConstructorArguments[0].Value as string;

            if (scriptPath is { Length: > 0 })
            {
                var scriptComponentBuilder = new ScriptBuilder(scriptType, scriptPath);
                scriptComponentBuilder.BuildSource(ctx);
            }
        });
    }
}
