using Microsoft.Extensions.DependencyInjection;

namespace KeywordEngine.Test.Helpers;
public static class DIModule
{

    public static ServiceProvider Startup()
    {
        var serviceProvider = new ServiceCollection()
    .AddSingleton<IFooService, FooService>()
    .BuildServiceProvider();

        return serviceProvider;
    }
}
