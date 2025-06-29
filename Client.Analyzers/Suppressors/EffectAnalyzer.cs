using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Foundation.Suppressors;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
sealed class EffectAnalyzer : DiagnosticAnalyzer
{
    const string AttributeName = "EffectAttribute";

    static readonly DiagnosticDescriptor ParameterCountRule = new("FNDD001", "Effects must have exactly 2 parameters", "Reducers must have exactly 2 parameters (state, action)", category: "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);
    static readonly DiagnosticDescriptor ReturnTypeRule = new("FNDD002", "Effects must have return void or Task", "Effects must have return void or Task", category: "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);
    static readonly DiagnosticDescriptor DispatcherParameterRule = new("FNDD003", "Parameter must be of type IDispatcher", "Parameter must be of type IDispatcher", category: "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [ParameterCountRule, ReturnTypeRule, DispatcherParameterRule];

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
            else if (method.ReturnType is not { MetadataName: "Void" or "Task" })
            {
                var diagnostic = Diagnostic.Create(ReturnTypeRule, method.Locations[0]);
                context.ReportDiagnostic(diagnostic);
            }
            else if (method.Parameters[1].Type is not { MetadataName: "IDispatcher" })
            {
                var diagnostic = Diagnostic.Create(DispatcherParameterRule, method.Parameters[1].Locations[0]);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
