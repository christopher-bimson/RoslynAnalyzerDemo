using System.Collections;

namespace Demo.Analyzers.Tests.Dates.Generators;

public class DateTimeNowUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            using System;
            
            public class Bad
            {
              public DateTime Method()
              {
                return DateTime.Now;
              }
            }
            """,
            7,
            12,
            7,
            24
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}