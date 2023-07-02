

using KeywordEngine.Abstraction;

namespace KeywordEngine.Models;
public sealed class TestStepResult
{
    public string Title { get; init; } = string.Empty;
    public string Keyword { get; init; } = string.Empty;
    public IEnumerable<Parameter>? Parameters { get; init; }
    public KeywordResponse Result { get; init; } = new KeywordResponse { Status = ResponseStatus.None, Message = string.Empty };
}
