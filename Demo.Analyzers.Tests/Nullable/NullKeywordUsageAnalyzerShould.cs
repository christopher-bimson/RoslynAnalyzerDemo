using Demo.Analyzers.Nullable;
using Demo.Analyzers.Tests.Nullable.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Demo.Analyzers.Tests.Nullable;

public class NullKeywordUsageAnalyzerShould
{
    [Theory]
    [ClassData(typeof(NullUsageCaseGenerator))]
    public async Task Warn_When_The_Null_Keyword_Is_Used(string code,
        int expectedStartLine, int expectedStartColumn, int expectedEndLine,
        int expectedEndColumn)
    {
        var expectedDiagnostic = new DiagnosticResult("DEM001", DiagnosticSeverity.Warning)
            .WithMessage("Don't use null. Use an Option type like https://github.com/nlkl/Optional or https://github.com/mcintyre321/OneOf.")
            .WithSpan(expectedStartLine, expectedStartColumn, 
                expectedEndLine, expectedEndColumn); 

        await CSharpAnalyzerVerifier<NullKeywordUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code, expectedDiagnostic);
    }

    [Theory]
    [ClassData(typeof(NoNullUsageCaseGenerator))]
    public async Task Not_Warn_If_The_Null_Keyword_Is_Not_Used(string code)
    {   
        await CSharpAnalyzerVerifier<NullKeywordUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code);
    }
}