using System;

namespace KeywordEngine.Test.Keywords;
public class DefaultConstructorKeyword : IActionKeyword
{
    public Task<KeywordResponse> ExecuteAsync()
    {
        Console.WriteLine($"{nameof(MyFirstActionKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstActionKeyword)} keyword executed."
        });
    }
}
