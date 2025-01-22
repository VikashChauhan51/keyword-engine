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
        // Iterate through each provided assembly and process its keywords
        foreach (var assembly in assemblies)
        {
            var keywords = ScanAssemblyForKeywords(assembly);

            // Add the keywords from this assembly to the global cache
            AddKeywordsToCache(assembly, keywords);
        }
    }

    /// <summary>
    /// Scans a single assembly for keywords and resolves duplicates using IKeywordPrifix.
    /// </summary>
    private static IDictionary<string, Type> ScanAssemblyForKeywords(Assembly assembly)
    {
        // Find prefix type, if it exists
        var prefixType = assembly.GetTypes().SingleOrDefault(
            x => typeof(IKeywordPrifix).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        // Find all keyword types in the assembly
        var keywords = assembly.GetTypes()
            .Where(x => typeof(IKeyword).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        // If a prefix type is found, apply it to the keyword names
        if (prefixType is not null)
        {
            var prefix = ((IKeywordPrifix)Activator.CreateInstance(prefixType)!).Get();
            return keywords.ToDictionary(k => $"{prefix}.{k.Name}", k => k);
        }

        // If no prefix, just return keyword names as is
        return keywords.ToDictionary(k => k.Name, k => k);
    }

    /// <summary>
    /// Adds keywords from an assembly to the cache. Ensures no duplicates are present.
    /// </summary>
    private static void AddKeywordsToCache(Assembly assembly, IDictionary<string, Type> keywords)
    {
        // Iterate through the keywords and add to the global cache
        foreach (var keyword in keywords)
        {
            if (_keywordCache.ContainsKey(keyword.Key))
            {
                throw new InvalidOperationException(
                    $"Duplicate keyword detected: '{keyword.Key}' in assembly '{assembly.GetName().Name}'. " +
                    "To resolve this, implement the IKeywordPrifix interface to provide unique prefixes for keywords.");
            }
            _keywordCache.Add(keyword.Key, keyword.Value);
        }
    }
}

