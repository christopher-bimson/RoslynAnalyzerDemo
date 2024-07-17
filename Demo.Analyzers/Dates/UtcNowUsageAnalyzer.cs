using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Demo.Analyzers.Dates;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class UtcNowUsageAnalyzer : DiagnosticAnalyzer
{
    internal const string Id = "DEM004";

    private static readonly DiagnosticDescriptor Descriptor = 
        new DiagnosticDescriptor(Id, 
            (LocalizableString)"Detects usage of DateTime.UtcNow", 
            (LocalizableString)"Don't use DateTime.UtcNow. Use an injectable abstraction that can be replaced with a test double instead.", 
            "Usage", 
            DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description: (LocalizableString)"Warns if you are using DateTime.UtcNow.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Descriptor);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.SimpleMemberAccessExpression);
    }

    private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var memberAccessExpr = (MemberAccessExpressionSyntax)context.Node;

        if (!IsDateTimeUtcNow(context, memberAccessExpr)) 
            return;
        
        var diagnostic = Diagnostic.Create(Descriptor, memberAccessExpr.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }

    private static bool IsDateTimeUtcNow(SyntaxNodeAnalysisContext context, MemberAccessExpressionSyntax memberAccessExpr)
    {
        return memberAccessExpr.Name.Identifier.Text == "UtcNow" &&
               context.SemanticModel.GetTypeInfo(memberAccessExpr.Expression).Type?.ToDisplayString() == "System.DateTime";
    }
}