
namespace KeywordEngine.Models;

public sealed class TestCase
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public IEnumerable<TestStep>? Steps { get; init; }
}
