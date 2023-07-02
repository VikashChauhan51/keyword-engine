using System;

namespace KeywordEngine.Test.Keywords;
public class DefaultConstructorKeyword : IActionKeyword
{
    public Task<KeywordResponse> Execute()
    {
        Console.WriteLine($"{nameof(MyFirstActionKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstActionKeyword)} keyword executed."
        });
    }
}
