using KeywordEngine.Models;
using KeywordEngine.Sample.Keywords;
using NUnit.Framework;
using System.Collections.Generic;
using KeywordEngine.Core;
using System.Threading.Tasks;

namespace KeywordEngine.Sample
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task First_Test()
        {
            var test = new TestCase
            {
                Id = 1,
                Title = "Demo test",
                Steps = new List<TestStep>
                {
                    new TestStep
                    {
                        Title="first step",
                        Keyword=nameof(MyFirstActionKeyword),
                        Index=0,
                        Parameters=new List<Parameter>
                        {
                            new Parameter
                            {
                                Name="message",
                                Value="Hello"
                            }
                        }
                    },
                    new TestStep
                    {
                        Title="second step",
                        Keyword=nameof(MyFirstVerifyKeyword),
                        Index=1,
                        Parameters=new List<Parameter>()
                    }

                }

            };

            var testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly));
            var response = await testRunner.Execute(test);
            Assert.IsNotNull(response);
        }
    }
}
