using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Demo.Analyzers.Dates;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class LocalNowUsageAnalyzer : DiagnosticAnalyzer
{
    internal const string Id = "DEM003";

    private static readonly DiagnosticDescriptor Descriptor = 
        new DiagnosticDescriptor(Id, 
            (LocalizableString)"Detects usage of DateTime.Now", 
            (LocalizableString)"Don't use DateTime.Now. Use DateTime.UtcNow instead.", 
            "Usage", 
            DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description: (LocalizableString)"Warns if you are using DateTime.Now.");

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

        if (!IsDateTimeNow(context, memberAccessExpr)) 
            return;
        
        var diagnostic = Diagnostic.Create(Descriptor, memberAccessExpr.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }

    private static bool IsDateTimeNow(SyntaxNodeAnalysisContext context, MemberAccessExpressionSyntax memberAccessExpr)
    {
        return memberAccessExpr.Name.Identifier.Text == "Now" &&
               context.SemanticModel.GetTypeInfo(memberAccessExpr.Expression).Type?.ToDisplayString() == "System.DateTime";
    }
}