using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Demo.Analyzers.Nullable;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class NullKeywordUsageAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Descriptor = 
        new DiagnosticDescriptor("DEM001", 
            (LocalizableString)"Flags the usage of the null keyword", 
            (LocalizableString)"Don't use null. Use an Option type like https://github.com/nlkl/Optional or https://github.com/mcintyre321/OneOf.", 
            "Usage", 
            DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description: (LocalizableString)"Flags the usage of the null keyword.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Descriptor);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.NullLiteralExpression);
    }

    private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var nullLiteral = context.Node;
        var diagnostic = Diagnostic.Create(Descriptor, nullLiteral.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}