

namespace KeywordEngine.Models;

public sealed class TestStep
{
    public string Title { get; init; } = string.Empty;
    public string Keyword { get; init; } = string.Empty;
    public int Index { get; init; }
    public IEnumerable<Parameter>? Parameters { get; init; } 
}
