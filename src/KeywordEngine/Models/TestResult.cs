
namespace KeywordEngine.Models;
public sealed class TestResult
{
    public int TestId { get; init; }
    public string TestTitle { get; init; } = string.Empty;
    public IEnumerable<TestStepResult>? Steps { get; init; }
}
