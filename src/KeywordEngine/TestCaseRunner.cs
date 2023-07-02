using KeywordEngine.Models;

namespace KeywordEngine
{
    public class TestCaseRunner
    {

        private readonly KeywordEngine _keywordEngine;
        public TestCaseRunner(IDictionary<string, Type> keywordMap)
        {
            _keywordEngine = new KeywordEngine(keywordMap);
        }

        public async Task<TestResult> Execute(TestCase test)
        {
            var result = new List<TestStepResult>();
            if (test?.Steps?.Any() ?? false)
            {
                foreach (var step in test.Steps)
                {
                    var response = await _keywordEngine.Invoke(step.Keyword, step.Parameters ?? new List<Parameter>());
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
            }
            return test is not null ? new TestResult
            {
                TestId = test.Id,
                TestTitle = test.Title,
                Steps = result
            } : new TestResult();
        }
    }
}
