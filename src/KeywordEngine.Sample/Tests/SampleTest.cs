
using KeywordEngine.Test.Helpers;

namespace KeywordEngine.Test.Tests;

[TestFixture]
public class Tests
{
    private TestCaseRunner testRunner; 

    [SetUp]
    public void Setup()
    {
        testRunner = TestRunnerFactory.CreateTestRunner();
    }

    [Test]
    [Category("UnitTest")]
    public async Task Test_With_Valid_Keyword_And_Parameters()
    {
        var test = TestDataHelper.GetTest("test_with_valid_keyword_and_parameters.json");

        await testRunner.ExecuteAsync(test);
    }

    [Test]
    [TestCase("test_with_valid_keyword_with_invalid_parameters_types.json")]
    [TestCase("test_with_valid_keyword_with_empty_parameters.json")]
    public async Task Test_With_Valid_Keyword_And_Invalid_Parameters(string fileName)
    {
        var test = TestDataHelper.GetTest(fileName);
        await testRunner.ExecuteAsync(test);
    }

    [Test]
    public async Task Test_Without_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_without_parameters_keyword.json");
        await testRunner.ExecuteAsync(test);
    }

    [Test]
    public async Task Test_Pimitive_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_primitive_data_keyword.json");

        await testRunner.ExecuteAsync(test);
    }

    [Test]
    public async Task Test_TestContext_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_testcontext_keyword.json");

        await testRunner.ExecuteAsync(test);
    }
}
