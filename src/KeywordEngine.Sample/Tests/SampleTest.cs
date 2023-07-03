
using System;
using KeywordEngine.Test.Helpers;
using Newtonsoft.Json.Converters;

namespace KeywordEngine.Test.Tests;

public class Tests
{
    private TestCaseRunner testRunner;

    [SetUp]
    public void Setup()
    {
        testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly));
    }

    [Test]
    public async Task Test_With_Valid_Keyword_And_Parameters()
    {
        var test = TestDataHelper.GetTest("test_with_valid_keyword_and_parameters.json");

        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response);
        Console.WriteLine(json);
    }

    [Test]
    [TestCase("test_with_valid_keyword_with_invalid_parameters_types.json")]
    [TestCase("test_with_valid_keyword_with_empty_parameters.json")]
    public async Task Test_With_Valid_Keyword_And_Invalid_Parameters(string fileName)
    {
        var test = TestDataHelper.GetTest(fileName);
        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response, new StringEnumConverter());
        System.Console.WriteLine(json);
    }

    [Test]
    public async Task Test_Without_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_without_parameters_keyword.json");

        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response);
        Console.WriteLine(json);
    }

    [Test]
    public async Task Test_Pimitive_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_primitive_data_keyword.json");

        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response);
        Console.WriteLine(json);
    }

    [Test]
    public async Task Test_TestContext_Parameters_Keyword()
    {
        var test = TestDataHelper.GetTest("test_testcontext_keyword.json");

        var response = await testRunner.Execute(test);
        Assert.IsNotNull(response);
        var json = JsonConvert.SerializeObject(response);
        Console.WriteLine(json);
    }
}
