

namespace KeywordEngine.Exceptions;
public class DependencyNotFoundException : Exception
{
    public DependencyNotFoundException(string name, string type, string keyword) : base($"Not able to resolve {name} parameter of {type} type for {keyword} keyword.")
    {

    }
}
