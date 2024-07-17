using System.Collections;

namespace Demo.Analyzers.Tests.Nullable.Generators;

public class NullableTypeUsageCaseGenerator : IEnumerable<object[]>
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
            using System;

            public class Bad
            {
              private DateTime? DoB;
            }
            """,
            5,
            11,
            5,
            20
        };
        
        yield return new object[]
        {
            """
            public class Bad
            {
              public int? MaybeANumberWhoKnows()
              {
                 return null;
              }
            }
            """,
            3,
            10,
            3,
            14
        };
        
        yield return new object[]
        {
            """
            public class Bad
            {
              private string? ooh_a_nullable_value_type;
            }
            """,
            3,
            11,
            3,
            18
        };
        
        yield return new object[]
        {
            """
            public class Bad
            {
              public void DoAThing()
              {
                double? thing = 0.0;
              }
            }
            """,
            5,
            5,
            5,
            12
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}