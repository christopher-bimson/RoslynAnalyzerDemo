using Demo.Analyzers.Dates;
using Demo.Analyzers.Tests.Dates.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Demo.Analyzers.Tests.Dates;

public class UtcNowUsageAnalyzerShould
{
    [Theory]
    [ClassData(typeof(UtcNowUsageCaseGenerator))]
    public async Task Warn_When_DateTime_Dot_UtcNow_Is_Used(string code,
        int expectedStartLine, int expectedStartColumn, int expectedEndLine,
        int expectedEndColumn)
    {
        var expectedDiagnostic = new DiagnosticResult("DEM004", DiagnosticSeverity.Warning)
            .WithMessage("Don't use DateTime.UtcNow. Use an injectable abstraction that can be replaced with a test double instead.")
            .WithSpan(expectedStartLine, expectedStartColumn, 
                expectedEndLine, expectedEndColumn); 

        await CSharpAnalyzerVerifier<UtcNowUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code, expectedDiagnostic);
    }

    [Theory]
    [ClassData(typeof(NoNowUsageCaseGenerator))]
    public async Task Not_Warn_If_DateTime_Dot_UtcNow_Is_Not_Used(string code)
    {
        await CSharpAnalyzerVerifier<UtcNowUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code);
    }
}