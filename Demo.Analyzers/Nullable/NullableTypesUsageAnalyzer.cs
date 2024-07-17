using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Demo.Analyzers.Nullable;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class NullableTypesUsageAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Descriptor = 
        new DiagnosticDescriptor("DEM002", 
            (LocalizableString)"Flags the usage of nullable types", 
            (LocalizableString)"Don't use nullable types. Use an Option type like https://github.com/nlkl/Optional or https://github.com/mcintyre321/OneOf.", 
            "Usage", 
            DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description: (LocalizableString)"Flags the usage of nullable types.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Descriptor);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.NullableType);
    }

    private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var nullLiteral = context.Node;
        var diagnostic = Diagnostic.Create(Descriptor, nullLiteral.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}