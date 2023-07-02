
namespace KeywordEngine.Abstraction;
public interface IDependencyResolver
{
    public object GetService(Type serviceType);
}
