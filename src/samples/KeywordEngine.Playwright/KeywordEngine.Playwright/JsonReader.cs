using Newtonsoft.Json;

namespace KeywordEngine.Playwright;
public static class JsonReader
{
    public static T Read<T>(string filePath)
    {
        var text = FileReader.Read(filePath);
        return JsonConvert.DeserializeObject<T>(text);
    }

    public static async Task<T> ReadAsync<T>(string filePath)
    {
        var text = await FileReader.ReadAsync(filePath);
        return JsonConvert.DeserializeObject<T>(text);
    }
}
