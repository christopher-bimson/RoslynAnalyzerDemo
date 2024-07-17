using System.Collections;

namespace Demo.Analyzers.Tests.Dates.Generators;

public class NoDateTimeNowUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            using System;

            public class Good
            {
              public DateTime Method()
              {
                return DateTime.UtcNow;
              }
            }
            """,
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}