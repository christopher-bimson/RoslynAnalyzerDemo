using System.Collections;

namespace Demo.Analyzers.Tests.Dates.Generators;

public class NoNowUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            using System;
            
            public class BadForADifferentReason
            {
              public DateTime Method()
              {
                return DateTime.Now;
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