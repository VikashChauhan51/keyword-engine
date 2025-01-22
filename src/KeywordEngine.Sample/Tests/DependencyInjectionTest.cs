using KeywordEngine.Test.Helpers;

namespace KeywordEngine.Test.Tests;
public class DependencyInjectionTest
{
    private TestCaseRunner testRunner;

    private readonly IDependencyResolver dependencyResolver;
    public DependencyInjectionTest()
    {
        KeywordRegistry.KeywordMap () =>
        {
            return Module.Export(typeof(MyFirstActionKeyword).Assembly);
        }
        dependencyResolver = new DependencyResolver(DIModule.Startup());
    }

    [SetUp]
    public void Setup()
    {
        testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly), dependencyResolver, new ConsoleResultPublisher());
    }


    [Test]
    public async Task Test_DI_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_dependencyinjection_keyword.json");
        await testRunner.ExecuteAsync(test);

    }

}
