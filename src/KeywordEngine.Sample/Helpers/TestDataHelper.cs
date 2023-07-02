

using System;
using System.IO;

namespace KeywordEngine.Test.Helpers;
public static class TestDataHelper
{

    public static TestCase GetTest(string fileName)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
        using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader r = new StreamReader(fs);
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<TestCase>(json);
    }
}
