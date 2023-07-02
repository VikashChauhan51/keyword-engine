
using KeywordEngine.Test.Helpers;
using Newtonsoft.Json.Converters;

namespace KeywordEngine.Test.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test_With_Valid_Keyword_And_Parameters()
    {
        var test = TestDataHelper.GetTest("test_with_valid_keyword_and_parameters.json");

        var testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly));
        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response);
    }

    [Test]
    [TestCase("test_with_valid_keyword_with_invalid_parameters_types.json")]
    [TestCase("test_with_valid_keyword_with_empty_parameters.json")]
    public async Task Test_With_Valid_Keyword_And_Invalid_Parameters(string fileName)
    {
        var test = TestDataHelper.GetTest(fileName);

        var testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly));
        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response, new StringEnumConverter());
        System.Console.WriteLine(json);
    }
}
