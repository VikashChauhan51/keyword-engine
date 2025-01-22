using KeywordEngine.Models;

namespace KeywordEngine.Abstraction;
public interface  ITestResultPublisher
{
    Task PublishTestResultAsync(TestResult testResult);
}
