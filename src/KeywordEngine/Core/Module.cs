using KeywordEngine.Abstraction;
using System.Reflection;

namespace KeywordEngine.Core;

public static class Module
{

    public static IDictionary<string, Type> Export(Assembly assembly)
    {
        var prifixType = assembly.GetTypes().SingleOrDefault(x => typeof(IKeywordPrifix).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        var keywords = assembly.GetTypes().Where(x => typeof(IKeyword).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        if (prifixType is not null)
        {
            var prifix = (IKeywordPrifix)Activator.CreateInstance(prifixType)!;
            return keywords.ToDictionary(k => $"{prifix.Get()}.{k.Name}", k => k);
        }
        return keywords.ToDictionary(k => k.Name, k => k);
    }
}
