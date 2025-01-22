using KeywordEngine.Core;
using KeywordEngine.Playwright.Keywords;

namespace KeywordEngine.Playwright;
public static class TestRunnerFactory
{
    private static TestCaseRunner? _testCaseRunner;

    public static TestCaseRunner CreateTestRunner()
    {
        if (_testCaseRunner == null)
        {


            if (Module.NoKeywords)
            {
                Module.Import(typeof(SearchKeyword).Assembly);
            }

            _testCaseRunner = new TestCaseRunner(testResultPublisher: new ConsoleResultPublisher());
        }

        return _testCaseRunner;
    }
}
