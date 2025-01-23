using KeywordEngine.Models;

namespace KeywordEngine.Core;
public class TestCaseBuilder
{
    private string _id = string.Empty;
    private string _title = string.Empty;
    private readonly List<TestStep> _steps = new();

    public TestCaseBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public TestCaseBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public TestStepBuilder AddStep()
    {
        return new TestStepBuilder(this);
    }

    public TestCase Build()
    {
        return new TestCase
        {
            Id = _id,
            Title = _title,
            Steps = _steps
        };
    }

    internal void AddBuiltStep(TestStep step)
    {
        _steps.Add(step);
    }
}

public class TestStepBuilder
{
    private readonly TestCaseBuilder _parentBuilder;
    private string _title = string.Empty;
    private string _keyword = string.Empty;
    private int _index;
    private readonly List<Parameter> _parameters = new();

    public TestStepBuilder(TestCaseBuilder parentBuilder)
    {
        _parentBuilder = parentBuilder;
    }

    public TestStepBuilder WithName(string title)
    {
        _title = title;
        return this;
    }

    public TestStepBuilder WithKeyword(string keyword)
    {
        _keyword = keyword;
        return this;
    }

    public TestStepBuilder WithIndex(int index)
    {
        _index = index;
        return this;
    }

    public ParameterBuilder AddParameter()
    {
        return new ParameterBuilder(this);
    }

    internal void AddBuiltParameter(Parameter parameter)
    {
        _parameters.Add(parameter);
    }

    public TestCaseBuilder Done()
    {
        _parentBuilder.AddBuiltStep(new TestStep
        {
            Title = _title,
            Keyword = _keyword,
            Index = _index,
            Parameters = _parameters
        });
        return _parentBuilder;
    }
}

public class ParameterBuilder
{
    private readonly TestStepBuilder _parentBuilder;
    private string _name = string.Empty;
    private string _value = string.Empty;

    public ParameterBuilder(TestStepBuilder parentBuilder)
    {
        _parentBuilder = parentBuilder;
    }

    public ParameterBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ParameterBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public TestStepBuilder Done()
    {
        _parentBuilder.AddBuiltParameter(new Parameter
        {
            Name = _name,
            Value = _value
        });
        return _parentBuilder;
    }
}

