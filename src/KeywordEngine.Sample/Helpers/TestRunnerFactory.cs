namespace KeywordEngine.Test.Helpers;
public static class TestRunnerFactory
{
    public static TestCaseRunner? testCaseRunner = null;
    public static TestCaseRunner CreateTestRunner()
    {
        if (testCaseRunner == null)
        {
            // Import keywords if not already done
            if (Module.NoKeywords)
            {
                Module.Import(typeof(MyFirstActionKeyword).Assembly);
            }
            testCaseRunner = new TestCaseRunner(testResultPublisher: new ConsoleResultPublisher());
        }

        return testCaseRunner;
    }
}

