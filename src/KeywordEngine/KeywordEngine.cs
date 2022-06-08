using KeywordEngine.Abstraction;
using KeywordEngine.Core;
using KeywordEngine.Models;

namespace KeywordEngine;

public class KeywordEngine
{
    private readonly IDictionary<string,Type> _keywordMap;
    public KeywordEngine(IDictionary<string, Type> keywordMap)
    {
        _keywordMap = keywordMap;
    }

    public void Invoke(string keywordName, IEnumerable<Parameter> parameters)
    {

        if (_keywordMap.TryGetValue(keywordName, out var keywordType))
        {
            var arguments = ParameterMapper.Map(keywordType, parameters);
            var keyword = (IKeyword)Activator.CreateInstance(keywordType, arguments)!;
            keyword.Execute();  
        }
    }
}
