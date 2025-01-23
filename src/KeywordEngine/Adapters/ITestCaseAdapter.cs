using KeywordEngine.Models;

namespace KeywordEngine.Adapters;
public interface ITestCaseAdapter<in TSource>
{
    TestCase ConvertToTestCase(TSource externalData);
}

