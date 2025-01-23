namespace KeywordEngine.Core;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class KeywordNameAttribute : Attribute
{
    public string Name { get; }

    public KeywordNameAttribute(string name)
    {
        Name = name;
    }
}
