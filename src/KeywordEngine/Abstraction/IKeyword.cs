
using KeywordEngine.Models;

namespace KeywordEngine.Abstraction;

public interface IKeyword
{
    Task<KeywordResponse> ExecuteAsync();
}
