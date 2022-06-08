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

        public void Execute(TestCase test)
        {
            foreach (var step in test.Steps)
            {
                _keywordEngine.Invoke(step.Keyword, step.Parameters);
            }
        }
    }
}
