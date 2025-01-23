using System.Text.Json;
using KeywordEngine.Models;

namespace KeywordEngine.Adapters;
public abstract class TestCaseAdapterBase<TSource> : ITestCaseAdapter<TSource>
{
    public abstract TestCase ConvertToTestCase(TSource externalData);

    protected T Deserialize<T>(string serializedData)
    {
        return JsonSerializer.Deserialize<T>(serializedData)!;
    }
}

