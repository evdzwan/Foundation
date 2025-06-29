using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Foundation.Suppressors;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
sealed class ReducerAnalyzer : DiagnosticAnalyzer
{
    const string AttributeName = "ReducerAttribute";

    static readonly DiagnosticDescriptor ParameterCountRule = new("FNDD004", "Reducers must have exactly 2 parameters", "Reducers must have exactly 2 parameters (state, action)", category: "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);
    static readonly DiagnosticDescriptor ReturnTypeRule = new("FNDD005", "Reducers must have a return type that matches the first parameter type", "Reducers must have a return type that matches the first parameter type (action)", category: "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [ParameterCountRule, ReturnTypeRule];

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
    }

    void AnalyzeMethod(SymbolAnalysisContext context)
    {
        var method = (IMethodSymbol)context.Symbol;
        if (method.GetAttributes().Any(attr => attr is { AttributeClass.Name: AttributeName }))
        {
            if (method.Parameters.Length != 2)
            {
                var diagnostic = Diagnostic.Create(ParameterCountRule, method.Locations[0]);
                context.ReportDiagnostic(diagnostic);
            }
            else if (!method.ReturnType.Equals(method.Parameters[0].Type, SymbolEqualityComparer.Default))
            {
                var diagnostic = Diagnostic.Create(ReturnTypeRule, method.Locations[0]);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
