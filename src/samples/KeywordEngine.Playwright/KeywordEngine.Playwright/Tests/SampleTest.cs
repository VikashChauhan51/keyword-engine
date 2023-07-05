
using KeywordEngine.Core;
using KeywordEngine.Models;
using KeywordEngine.Playwright.Keywords;
using KeywordEngine.Playwright.Pages;
using TestDatContext = KeywordEngine.Core.TestContext;

namespace KeywordEngine.Playwright.Tests;
[Parallelizable(ParallelScope.Self)]
public class SampleTest : TestBase
{
    TestCaseRunner testRunner;
    public SampleTest()
    {
        testRunner = new TestCaseRunner(Module.Export(typeof(SearchKeyword).Assembly));
    }


    [Test]
    public async Task SampleBingSearch()
    {

        var test = new TestCase
        {
            Id = 1,
            Title = "search on bing",
            Steps = new List<TestStep>
                {
                    new TestStep
                    {
                        Title="search step",
                        Keyword=nameof(SearchKeyword),
                        Index=0,
                        Parameters=new List<Parameter>
                        {
                            new Parameter
                            {
                                Name="text",
                                Value="keyword engine nuget"
                            }
                        }
                    },
                    new TestStep
                    {
                        Title="search step",
                        Keyword=nameof(SearchKeyword),
                        Index=2,
                        Parameters=new List<Parameter>
                        {
                            new Parameter
                            {
                                Name="text",
                                Value="keyword engine nuget by vikash chauhan"
                            }
                        }
                    }
                }

        };

        var testContext = new TestDatContext(new Dictionary<string, object>() { { nameof(IPage), Page } });
        var response = await testRunner.Execute(test, testContext);
        response.Should().NotBeNull();

    }
}
