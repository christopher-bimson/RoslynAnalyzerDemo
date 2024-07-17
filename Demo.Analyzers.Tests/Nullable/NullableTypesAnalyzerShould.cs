using Demo.Analyzers.Nullable;
using Demo.Analyzers.Tests.Nullable.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Demo.Analyzers.Tests.Nullable;

public class NullableTypesAnalyzerShould
{
    [Theory]
    [ClassData(typeof(NullableTypeUsageCaseGenerator))]
    public async Task Warn_When_Nullable_Types_Are_Used(string code,
        int expectedStartLine, int expectedStartColumn, int expectedEndLine,
        int expectedEndColumn)
    {
        var expectedDiagnostic = new DiagnosticResult("DEM002", DiagnosticSeverity.Warning)
            .WithMessage("Don't use nullable types. Use an Option type like https://github.com/nlkl/Optional or https://github.com/mcintyre321/OneOf.")
            .WithSpan(expectedStartLine, expectedStartColumn, 
                expectedEndLine, expectedEndColumn); 

        await CSharpAnalyzerVerifier<NullableTypesUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code, expectedDiagnostic);
    }

    [Theory]
    [ClassData(typeof(SensibleTypeUsageCaseGenerator))]
    public async Task Not_Warn_If_We_Are_Smart_And_Not_Using_Nullable_Types(string code)
    {
        await CSharpAnalyzerVerifier<NullableTypesUsageAnalyzer, XUnitVerifier>
            .VerifyAnalyzerAsync(code);
    }
}