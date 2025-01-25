# KeywordEngine

[![NuGet Version](https://img.shields.io/nuget/v/KeywordEngine.svg?style=flat-square)](https://www.nuget.org/packages/KeywordEngine/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/KeywordEngine.svg?style=flat-square)](https://www.nuget.org/packages/KeywordEngine/)
[![Build Status](https://github.com/VikashChauhan51/keyword-engine/actions/workflows/build.yml/badge.svg)](https://github.com/VikashChauhan51/keyword-engine/actions)
[![License](https://img.shields.io/github/license/VikashChauhan51/keyword-engine.svg?style=flat-square)](https://github.com/VikashChauhan51/keyword-engine/blob/main/LICENSE)

The [KeywordEngine](https://www.nuget.org/packages/KeywordEngine) is a keyword-driven framework execution engine for automating test cases.
It can easily integrate with any C# unit testing frameworks like **NUnit**, **XUnit**, and **MSTest**.

  ## Installation

You can install the KeywordEngine package via NuGet:

```shell
dotnet add package KeywordEngine
```

Or you can use the NuGet Package Manager:

```shell
Install-Package KeywordEngine
```

## At a Glance:
- Compatible with .NET **Core 6+**.
- Supports any C# unit testing frameworks like **NUnit**, **XUnit**, and **MSTest**.
- No external dependencies beyond standard base libraries.
- Maps parameters to scalar types, including `Enums`, `Guid`, `DateTimeOffset`, and nullable scalar types.
- Supports additional keyword data/parameters using the `ITestContext` interface.
- Automatically ignores unused and additional provided parameters.
- Supports custom **DependencyInjection** to resolve keyword dependencies via the `IDependencyResolver` interface.
- Uses `InvariantCulture` for default parameter parsing.

## Quick Start Example:

```csharp
using System;
using KeywordEngine.Core;
using KeywordEngine.Test.Keywords;
using KeywordEngine.Abstraction;
using KeywordEngine.Models;

// Define keywords
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

// Create test steps
var test = new TestCase
{
    Id = 1,
    Title = "Demo test",
    Steps = new List<TestStep>
    {
        new TestStep
        {
            Title = "First Step",
            Keyword = nameof(MyFirstActionKeyword),
            Index = 0,
            Parameters = new List<Parameter>
            {
                new Parameter
                {
                    Name = "message",
                    Value = "Hello"
                }
            }
        },
        new TestStep
        {
            Title = "Second Step",
            Keyword = nameof(MyFirstVerifyKeyword),
            Index = 1,
            Parameters = new List<Parameter>()
        }
    }
};

// Initialize dependency injection and import keywords
var testRunner = TestRunnerFactory.CreateTestRunner();

// Execute test
await testRunner.ExecuteAsync(test);
```

## Enhanced Keyword and Test Runner Workflow

### Centralized Dependency Injection
The framework now supports centralized dependency injection using `DependencyResolverFactory`. You don't need to create dependency injection manually for every test case. Keywords and dependencies are automatically resolved.

### Simplified Test Runner Creation
The `TestRunnerFactory` simplifies test runner creation. It handles:
- **Keyword Importing**: Automatically imports keywords if not already done.
- **Dependency Injection**: Uses a singleton `IDependencyResolver` instance.
- **Result Publishing**: Provides a default `ConsoleResultPublisher` or allows custom publishers.

#### Updated `TestRunnerFactory` Example
```csharp
public static class TestRunnerFactory
{
    private static TestCaseRunner? _testCaseRunner;

    public static TestCaseRunner CreateTestRunner()
    {
        if (_testCaseRunner == null)
        {
            var dependencyResolver = DependencyResolverFactory.CreateResolver();

            if (Module.NoKeywords)
            {
                Module.Import(typeof(MyFirstActionKeyword).Assembly);
            }

            _testCaseRunner = new TestCaseRunner(
                dependencyResolver: dependencyResolver,
                testResultPublisher: new ConsoleResultPublisher());
        }

        return _testCaseRunner;
    }
}
```

### Custom Result Publisher
You can define a custom result publisher by implementing the `ITestResultPublisher` interface. For example:

```csharp
public class CustomResultPublisher : ITestResultPublisher
{
    public Task PublishTestResultAsync(TestResult testResult)
    {
        // Custom publishing logic, e.g., save to a database or log
        Console.WriteLine($"Publishing test result: {testResult.TestTitle}");
        return Task.CompletedTask;
    }
}
```

### Using a Custom Result Publisher

```csharp
var testRunner = new TestCaseRunner(
    testResultPublisher: new CustomResultPublisher()
);
```

### Benefits of the New Flow
1. **Ease of Use**:
   - No manual keyword imports or dependency injection setup.
   - Centralized and reusable DI logic.

2. **Modular and Extensible**:
   - Easily extendable with custom publishers or additional keyword assemblies.

3. **Robust Execution**:
   - Centralized exception handling and result publishing ensure consistent test execution.

## Other Examples:
For more examples and use cases, please check [Tests](https://github.com/VikashChauhan51/keyword-engine/tree/master/src/KeywordEngine.Sample/Tests) and [Samples](https://github.com/VikashChauhan51/keyword-engine/tree/master/src/samples/).

