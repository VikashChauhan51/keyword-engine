using KeywordEngine.Abstraction;
using System.Reflection;

namespace KeywordEngine.Core;

public static class Module
{
    private static readonly Dictionary<string, Type> _keywordCache = new();
    public static bool NoKeywords => _keywordCache.Count == 0;

    /// <summary>
    /// Returns the cached keywords for the provided assemblies.
    /// If no cached keywords exist, they will be scanned and cached.
    /// </summary>
    public static IDictionary<string, Type> Keywords
    {
        get
        {
            if (_keywordCache.Count == 0)
            {
                throw new InvalidOperationException("No keywords have been loaded. Ensure you have called the Import method.");
            }
            return _keywordCache;
        }
    }

    /// <summary>
    /// Scans the provided assemblies for keyword classes and caches them. If duplicates are detected, it throws an exception.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan for keywords.</param>
    public static void Import(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var keywords = ScanAssemblyForKeywords(assembly);
            AddKeywordsToCache(assembly, keywords);
        }
    }

    /// <summary>
    /// Scans a single assembly for keywords and resolves duplicates using IKeywordPrifix.
    /// </summary>
    private static Dictionary<string, Type> ScanAssemblyForKeywords(Assembly assembly)
    {
        // Find prefix type, if it exists
        var prefix = assembly.GetTypes()
        .Where(x => typeof(IKeywordPrifix).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        .Select(x => ((IKeywordPrifix)Activator.CreateInstance(x)!).Get())
        .FirstOrDefault();

        // Find all keyword types in the assembly
        var keywords = assembly.GetTypes()
            .Where(x => typeof(IKeyword).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);


        var keywordDict = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        foreach (var keyword in keywords)
        {
            var attribute = keyword.GetCustomAttribute<KeywordNameAttribute>();
            var name = attribute?.Name ?? keyword.Name;
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                name = $"{prefix}.{name}";
            }
            if (!keywordDict.TryAdd(name, keyword))
            {
                throw new InvalidOperationException($"Duplicate keyword detected: '{name}' in assembly '{assembly.GetName()}'.");
            }
        }

        return keywordDict;
    }

    /// <summary>
    /// Adds keywords from an assembly to the cache. Ensures no duplicates are present.
    /// </summary>
    private static void AddKeywordsToCache(Assembly assembly, Dictionary<string, Type> keywords)
    {
        foreach (var keyword in keywords)
        {
            if (_keywordCache.ContainsKey(keyword.Key))
            {
                throw new InvalidOperationException(
                    $"Duplicate keyword detected: '{keyword.Key}' in assembly '{assembly.GetName()}'. " +
                    "To resolve this, implement the IKeywordPrifix interface to provide unique prefixes for keywords.");
            }
            _keywordCache.Add(keyword.Key, keyword.Value);
        }
    }
}

