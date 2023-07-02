
using KeywordEngine.Abstraction;

namespace KeywordEngine.Models;
public sealed class KeywordResponse
{
    public ResponseStatus Status { get; init; }
    public string Message { get; init; } = string.Empty;
}
