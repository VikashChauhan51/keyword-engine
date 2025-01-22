using System;

namespace KeywordEngine.Test.Keywords;
public class PrimitiveParameterActionKeyword : IActionKeyword
{
    private readonly int age;
    private readonly double amount;
    private readonly DateTimeOffset date;
    public PrimitiveParameterActionKeyword(int age, double amount, DateTimeOffset date)
    {
        this.age = age;
        this.amount = amount;
        this.date = date;
    }
    public Task<KeywordResponse> ExecuteAsync()
    {
        Console.WriteLine($"age: {age} ammount: {amount} on date: {date}");
        Console.WriteLine($"{nameof(PrimitiveParameterActionKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(PrimitiveParameterActionKeyword)} keyword executed."
        });
    }
}
