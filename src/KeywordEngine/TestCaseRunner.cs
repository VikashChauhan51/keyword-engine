using KeywordEngine.Abstraction;
using KeywordEngine.Core;
using KeywordEngine.Models;

namespace KeywordEngine;
public sealed class TestCaseRunner
{

    private readonly KeywordEngine _keywordEngine;
    private readonly ITestResultPublisher? testResultPublisher;

    public TestCaseRunner(IDependencyResolver? dependencyResolver = null, ITestResultPublisher? testResultPublisher = null)
    {
        _keywordEngine = new KeywordEngine(dependencyResolver);
        this.testResultPublisher = testResultPublisher;
    }

    public async Task ExecuteAsync(TestCase test, ITestContext? testContext = null)
    {
        var result = new List<TestStepResult>();

        if (test?.Steps?.Any() ?? false)
        {
            foreach (var step in test.Steps.OrderBy(x => x.Index))
            {
                try
                {

                    var response = await _keywordEngine.ExecuteAsync(
                        step.Keyword,
                        step.Parameters ?? new List<Parameter>(),
                        testContext ?? new TestContext(new Dictionary<string, object>()));

                    if (response != null)
                    {
                        result.Add(new TestStepResult
                        {
                            Title = step.Title,
                            Keyword = step.Keyword,
                            Parameters = step.Parameters,
                            Result = response
                        });
                    }
                }
                catch (Exception ex)
                {
                    result.Add(new TestStepResult
                    {
                        Title = step.Title,
                        Keyword = step.Keyword,
                        Parameters = step.Parameters,
                        Result = new KeywordResponse
                        {
                            Message = ex.Message,
                            Status = ResponseStatus.Failed
                        }
                    });

                    await PublishResult(test, result);
                    throw;
                }

            }
            await PublishResult(test, result);
        }
    }

    private async Task PublishResult(TestCase test, IEnumerable<TestStepResult> stepsData)
    {
        if (testResultPublisher != null)
        {
            await testResultPublisher!.PublishTestResultAsync(test is not null ? new TestResult
            {
                TestId = test.Id,
                TestTitle = test.Title,
                Steps = stepsData
            } : new TestResult());

        }
    }

}
