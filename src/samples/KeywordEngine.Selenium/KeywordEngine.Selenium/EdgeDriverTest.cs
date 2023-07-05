using KeywordEngine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using KeywordEngine.Selenium.Keywords;
using KeywordEngine.Core;

namespace KeywordEngine.Selenium
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private IWebDriver _driver;
        TestCaseRunner testRunner;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
            testRunner = new TestCaseRunner(Module.Export(typeof(SearchKeyword).Assembly));
        }

        [TestMethod]
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
                        Index=1,
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

            var testContext = new Core.TestContext(new Dictionary<string, object>() { { nameof(IWebDriver), _driver } });
            var response = await testRunner.Execute(test, testContext);
            Assert.IsNotNull(response);

        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
