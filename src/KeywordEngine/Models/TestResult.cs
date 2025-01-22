
namespace KeywordEngine.Models;
public sealed class TestResult
{
    public string TestId { get; init; } = string.Empty;
    public string TestTitle { get; init; } = string.Empty;
    public IEnumerable<TestStepResult>? Steps { get; init; }
}
