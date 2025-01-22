using Microsoft.Extensions.DependencyInjection;

namespace KeywordEngine.Test.Helpers;
public static class DIModule
{
    private static ServiceProvider? _serviceProvider;
    public static IDependencyResolver Startup()
    {
        if (_serviceProvider == null)
        {
            _serviceProvider = new ServiceCollection()
        .AddSingleton<IFooService, FooService>()
        .BuildServiceProvider();
            ImportKeywords();
        }
        return new DependencyResolver(_serviceProvider);
    }

    private static void ImportKeywords()
    {
        if (Module.NoKeywords)
        {
            Module.Import(typeof(MyFirstActionKeyword).Assembly);
        }
    }
}
