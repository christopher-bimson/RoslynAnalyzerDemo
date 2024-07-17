using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo.Analyzers.Dates;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(LocalNowUsageCodeFix)), Shared]
public class LocalNowUsageCodeFix : CodeFixProvider
{
    public sealed override ImmutableArray<string> FixableDiagnosticIds
    {
        get { return ImmutableArray.Create(LocalNowUsageAnalyzer.Id); }
    }

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        
        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var declaration = root?.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<MemberAccessExpressionSyntax>().First();
        
        if (declaration == null)
            return;
        
        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Use DateTime.UtcNow",
                createChangedDocument: c => ReplaceWithDateTimeUtcNow(context.Document, declaration, c),
                equivalenceKey: "Use DateTime.UtcNow"),
            diagnostic);
    }

    private async Task<Document> ReplaceWithDateTimeUtcNow(Document document, 
        MemberAccessExpressionSyntax localNowExpression, CancellationToken cancellationToken)
    {
        var originalNode = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

        if (originalNode == null)
            return document;
        
        var utcNowExpression = SyntaxFactory.ParseExpression("DateTime.UtcNow").WithTriviaFrom(localNowExpression);
        var replacementNode = originalNode.ReplaceNode(localNowExpression, utcNowExpression);
        var newDocument = document.WithSyntaxRoot(replacementNode);
        return newDocument;
    }
}