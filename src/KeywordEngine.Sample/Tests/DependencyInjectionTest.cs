using KeywordEngine.Test.Helpers;

namespace KeywordEngine.Test.Tests;
public class DependencyInjectionTest
{
    private TestCaseRunner testRunner;

    public DependencyInjectionTest()
    {

        testRunner = new TestCaseRunner(DIModule.Startup(), new ConsoleResultPublisher());
    }



    [Test]
    public async Task Test_DI_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_dependencyinjection_keyword.json");
        await testRunner.ExecuteAsync(test);

    }

}
