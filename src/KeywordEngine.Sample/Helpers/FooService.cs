

namespace KeywordEngine.Test.Helpers;
public interface IFooService
{
    void DoThing(string message);
}

public class FooService : IFooService
{

    public void DoThing(string message)
    {
        System.Console.WriteLine($"Doing the thing {message}");
    }
}
