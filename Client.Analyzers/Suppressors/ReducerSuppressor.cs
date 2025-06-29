using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Foundation.Suppressors;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
sealed class ReducerSuppressor : DiagnosticSuppressor
{
    const string AttributeName = "ReducerAttribute";

    static readonly SuppressionDescriptor SuppressIDE0060 = new("FNDS002", "IDE0060", "Effects and Reducers can have unused parameters");

    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions { get; } = [SuppressIDE0060];

    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (var diagnostic in context.ReportedDiagnostics.Where(d => d.Id == SuppressIDE0060.SuppressedDiagnosticId))
        {
            if (diagnostic.Location.SourceTree is { } syntaxTree)
            {
                var model = new Lazy<SemanticModel>(() => context.GetSemanticModel(syntaxTree));
                var root = syntaxTree.GetRoot(context.CancellationToken);

                var node = root.FindNode(diagnostic.Location.SourceSpan);
                if (node.FirstAncestorOrSelf<ParameterSyntax>() is { } parameterSyntax)
                {
                    if (parameterSyntax.Parent?.Parent is MethodDeclarationSyntax method)
                    {
                        var symbol = model.Value.GetDeclaredSymbol(method, context.CancellationToken);
                        if (symbol?.GetAttributes().Any(attr => attr is { AttributeClass.Name: AttributeName }) is true)
                        {
                            context.ReportSuppression(Suppression.Create(SuppressIDE0060, diagnostic));
                        }
                    }
                }
            }
        }
    }
}
