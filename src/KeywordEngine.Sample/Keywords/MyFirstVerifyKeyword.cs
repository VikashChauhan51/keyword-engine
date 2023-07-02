using System;

namespace KeywordEngine.Test.Keywords;

internal class MyFirstVerifyKeyword : IVerifyKeyword
{
    public MyFirstVerifyKeyword()
    {

    }


    public Task<KeywordResponse> Execute()
    {
        Console.WriteLine($"{nameof(MyFirstVerifyKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstVerifyKeyword)} keyword executed."
        });
    }
}
