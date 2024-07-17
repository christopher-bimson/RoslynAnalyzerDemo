using Demo.Analyzers.Dates;
using Demo.Analyzers.Tests.Dates.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Demo.Analyzers.Tests.Dates;

public class LocalNowUsageAnalyzerShould
{
    [Theory]
    [ClassData(typeof(DateTimeNowUsageCaseGenerator))]
    public async Task Warn_When_DateTime_Dot_Now_Is_Used(string code,
        int expectedStartLine, int expectedStartColumn, int expectedEndLine,
        int expectedEndColumn)
    {
        var expectedDiagnostic = new DiagnosticResult("DEM003", DiagnosticSeverity.Warning)
            .WithMessage("Don't use DateTime.Now. Use DateTime.UtcNow instead.")
            .WithSpan(expectedStartLine, expectedStartColumn, 
                expectedEndLine, expectedEndColumn); 

        await CSharpAnalyzerVerifier<LocalNowUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code, expectedDiagnostic);
    }

    [Theory]
    [ClassData(typeof(NoDateTimeNowUsageCaseGenerator))]
    public async Task Not_Warn_If_DateTime_Dot_Now_Is_Not_Used(string code)
    {
        await CSharpAnalyzerVerifier<LocalNowUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code);
    }
}