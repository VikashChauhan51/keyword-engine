# KeywordEngine

The [KeywordEngine](https://www.nuget.org/packages/KeywordEngine) is a keyword driven framework execution engine for automation test cases.
It can easily integrate with any C# unit testing frameworks like **Nunit**,**XUnit**, and **MSTest**.

## At a glance:
- Compatible with .NET **Core 6+**.
- Support any C# unit testing frameworks like **Nunit**,**XUnit**, and **MSTest**.
- Doesn't depend on other packages (No dependencies beyond standard base libraries).
- Map parameters to scalar types, including `Enums`, `Guid`,`datetimeoffset` and **Nullable** scalar types, `Enums`,`datetimeoffset` and `Guid`.
- Support addition keyword data/parameters with the help of `ITestContext` interface.
- Automatically ignore unused and additional provided parameters.
- Support custom **DependencyInjection** to resolve the keyword dependencies with the help of `IDependencyResolver` interface.
- Default parameters parser has `InvariantCulture`.

## Quick Start Example:

```C#

using System;
using KeywordEngine.Core;
using KeywordEngine.Test.Keywords;
using KeywordEngine.Abstraction;
using KeywordEngine.Models;

// Create keywords

internal class MyFirstActionKeyword : IActionKeyword
{
    private readonly string _message;
    public MyFirstActionKeyword(string message)
    {
        _message = message;
    }

    public Task<KeywordResponse> Execute()
    {
        Console.WriteLine(_message);
        Console.WriteLine($"{nameof(MyFirstActionKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstActionKeyword)} keyword executed."
        });
    }
}

internal class MyFirstVerifyKeyword : IVerifyKeyword
{
    public MyFirstVerifyKeyword()
    {

    }


    public Task<KeywordResponse> Execute()
    {
        Console.WriteLine($"{nameof(MyFirstVerifyKeyword)} keyword executed.");

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(MyFirstVerifyKeyword)} keyword executed."
        });
    }
}

// Create test steps data

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

// Import all keywords
var testRunner = new TestCaseRunner(Module.Export(typeof(MyFirstActionKeyword).Assembly));
// Eexecute test
var response = await testRunner.Execute(test);

```

## Other examples:
For more examples and use cases please check [test](https://github.com/VikashChauhan51/keyword-engine/tree/master/src/KeywordEngine.Sample/Tests).
