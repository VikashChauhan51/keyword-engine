
namespace KeywordEngine.Models;

public class TestCase
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public IEnumerable<TestStep>? Steps { get; init; }
}
