using KeywordEngine.Abstraction;
using KeywordEngine.Models;
using System.Text.Json;

namespace KeywordEngine.Core;
public sealed class ConsoleResultPublisher : ITestResultPublisher
{
    public Task PublishTestResultAsync(TestResult testResult)
    {
        Console.WriteLine(JsonSerializer.Serialize(testResult));
        return Task.CompletedTask;
    }
}
