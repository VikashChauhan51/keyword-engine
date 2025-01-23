using KeywordEngine.Core;
using KeywordEngine.Models;

namespace KeywordEngine.Adapters;
public class JsonTestCaseAdapter : TestCaseAdapterBase<string>
{
    public override TestCase ConvertToTestCase(string externalData)
    {
        var testCaseData = Deserialize<dynamic>(externalData);

        var builder = new TestCaseBuilder()
            .WithId(testCaseData.Id)
            .WithTitle(testCaseData.Title);

        foreach (var stepData in testCaseData.Steps)
        {
            var stepBuilder = builder.AddStep()
                    .WithName(stepData.Title)
                    .WithKeyword(stepData.Keyword)
                    .WithIndex(stepData.Index);

            if (stepData.Parameters != null)
            {
                foreach (var parameter in stepData.Parameters)
                {
                    stepBuilder.AddParameter()
                               .WithName(parameter.Name)
                               .WithValue(parameter.Value)
                               .Done();
                }
            }
            stepBuilder.Done();
        }

        return builder.Build();
    }
}
