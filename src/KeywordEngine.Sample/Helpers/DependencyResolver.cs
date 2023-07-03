
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KeywordEngine.Test.Helpers;
public class DependencyResolver: IDependencyResolver
{
    private readonly ServiceProvider serviceProvider;

    public DependencyResolver(ServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    public object GetService(Type serviceType) => serviceProvider.GetService(serviceType) ?? throw new ArgumentOutOfRangeException(nameof(serviceType));

}
