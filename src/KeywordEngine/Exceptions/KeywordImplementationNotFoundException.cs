
namespace KeywordEngine.Exceptions;

public class KeywordImplementationNotFoundException: Exception
{
    public KeywordImplementationNotFoundException(string keyword):base($"Keyword implementation for {keyword} was not found.")
    {

    }
}
