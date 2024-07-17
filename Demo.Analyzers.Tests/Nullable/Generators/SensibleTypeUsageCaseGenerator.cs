using System.Collections;

namespace Demo.Analyzers.Tests.Nullable.Generators;

public class SensibleTypeUsageCaseGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            """
            using System;

            public class Bad
            {
              private DateTime dob;
              private string name;
            }
            """
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}