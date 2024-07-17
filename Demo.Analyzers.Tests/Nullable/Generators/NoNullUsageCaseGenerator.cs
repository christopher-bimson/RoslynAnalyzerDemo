using System.Collections;

namespace Demo.Analyzers.Tests.Nullable.Generators;

public class NoNullUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            public class Good
            {
              public string Method()
              {
                string result = string.Empty;
                return result;
              }
            }
            """
        };
        
        yield return new object[]
        {
            """
            public class GoodButWithAnEvilMethodName
            {
              public string NullMethod()
              {
                string result = string.Empty;
                return result;
              }
            }
            """
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}