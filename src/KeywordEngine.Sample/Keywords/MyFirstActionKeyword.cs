using System;

namespace KeywordEngine.Test.Keywords;

internal class MyFirstActionKeyword : IActionKeyword
{
    private readonly string _message;
    public MyFirstActionKeyword(string message)
    {
        _message = message;
    }

    public Task<KeywordResponse> ExecuteAsync()
    {
        Console.WriteLine(_message);
        Console.WriteLine($"{nameof(MyFirstActionKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstActionKeyword)} keyword executed."
        });
    }
}
