using KeywordEngine.Abstraction;

namespace KeywordEngine.Core;
public sealed class TestContext : ITestContext
{
    public IDictionary<string, object> Data { get; }
    public TestContext(IDictionary<string, object> data)
    {
        Data = data;
    }


}
