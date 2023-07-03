

using System;

namespace KeywordEngine.Test.Keywords;
public class TestContextKeyword : IActionKeyword
{
    private readonly string message;
    private readonly ITestContext testContext;

    public TestContextKeyword(string message, ITestContext testContext)
    {
        this.message = message;
        this.testContext = testContext;

    }
    public Task<KeywordResponse> Execute()
    {
        Console.WriteLine(message);
        Console.WriteLine($"{nameof(TestContextKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(TestContextKeyword)} keyword executed."
        });
    }
}
