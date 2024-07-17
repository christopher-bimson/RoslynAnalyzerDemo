using System.Collections;

namespace Demo.Analyzers.Tests.Dates.Generators;

public class LocalNowCodeFixCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            using System;

            public class SimpleScenario
            {
              public DateTime Method()
              {
                return DateTime.Now;
              }
            }
            """,
            7,
            12,
            """
            using System;

            public class SimpleScenario
            {
              public DateTime Method()
              {
                return DateTime.UtcNow;
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