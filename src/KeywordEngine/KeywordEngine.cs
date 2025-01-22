using KeywordEngine.Abstraction;
using KeywordEngine.Core;
using KeywordEngine.Models;

namespace KeywordEngine;

public sealed class KeywordEngine
{
    private readonly IDependencyResolver? _dependencyResolver;

    public KeywordEngine(IDependencyResolver? dependencyResolver = null)
    {
        _dependencyResolver = dependencyResolver;
    }

    /// <summary>
    /// Execute a keyword by its name with the provided parameters and context.
    /// </summary>
    /// <param name="keywordName">The name of the keyword to invoke.</param>
    /// <param name="parameters">The parameters to pass to the keyword.</param>
    /// <param name="testContext">The context for the test execution.</param>
    /// <returns>A <see cref="KeywordResponse"/> indicating the result of the execution.</returns>
    public async Task<KeywordResponse> ExecuteAsync(string keywordName, IEnumerable<Parameter> parameters, ITestContext testContext)
    {
        if (string.IsNullOrWhiteSpace(keywordName))
        {
            throw new ArgumentException("Keyword name must not be null or empty.", nameof(keywordName));
        }
        // Use the dynamically fetched keyword map from the Module class
        var keywordMap = Module.Keywords;

        if (!keywordMap.TryGetValue(keywordName, out var keywordType))
        {
            return new KeywordResponse
            {
                Status = ResponseStatus.None,
                Message = $"Keyword '{keywordName}' not found."
            };
        }

        try
        {
            // Map parameters to the keyword's constructor
            var arguments = ParameterMapper.Map(keywordType, parameters, testContext, _dependencyResolver);

            // Create an instance of the keyword and execute it
            var keywordInstance = (IKeyword)Activator.CreateInstance(keywordType, arguments)!;
            return await keywordInstance.ExecuteAsync();
        }
        catch (Exception ex)
        {
            return new KeywordResponse
            {
                Status = ResponseStatus.Failed,
                Message = $"Error executing keyword '{keywordName}': {ex.Message}"
            };
        }
    }
}

