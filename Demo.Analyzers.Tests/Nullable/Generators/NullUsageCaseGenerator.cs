using System.Collections;

namespace Demo.Analyzers.Tests.Nullable.Generators;

public class NullUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        // Test data structure
        //
        // Sample Code
        // Start Line
        // Start Column
        // End Line
        // End Column
        
        yield return new object[]
        {
            """
            public class Bad
            {
              public string Method()
              {
                string result = null;
                return result;
              }
            }
            """,
            5,
            21,
            5,
            25
        };
        
        yield return new object[]
        {
            """
            public class Bad
            {
              public string Method()
              {
                return null;
              }
            }
            """,
            5,
            12,
            5,
            16
        };
        
        yield return new object[]
        {
            """
            public class Bad
            {
              private string _field = null;
                
              public string Method()
              {
                return string.Empty;
              }
            }
            """,
            3,
            27,
            3,
            31
        };
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}