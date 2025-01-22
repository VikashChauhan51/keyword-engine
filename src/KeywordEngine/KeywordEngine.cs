using KeywordEngine.Abstraction;
using KeywordEngine.Core;
using KeywordEngine.Models;

namespace KeywordEngine;

public sealed class KeywordEngine
{
    private readonly IDictionary<string, Type> _keywordMap;
    private readonly IDependencyResolver? dependencyResolver;
    public KeywordEngine(IDictionary<string, Type> keywordMap, IDependencyResolver? dependencyResolver = null)
    {
        _keywordMap = keywordMap ?? throw new ArgumentNullException(nameof(keywordMap));
        this.dependencyResolver = dependencyResolver;
    }

    public async Task<KeywordResponse> Invoke(string keywordName, IEnumerable<Parameter> parameters, ITestContext testContext)
    {
        if (!_keywordMap.TryGetValue(keywordName, out var keywordType))
        {
            return new KeywordResponse { Status = ResponseStatus.None, Message = $"Keyword '{keywordName}' not found." };
        }

        var arguments = ParameterMapper.Map(keywordType, parameters, testContext, dependencyResolver);
        var keyword = (IKeyword)Activator.CreateInstance(keywordType, arguments)!;
        return await keyword.ExecuteAsync();
    }
}
