using Newtonsoft.Json;

namespace ProjectManagementSystem.External.Extensions;

public static class Jfr
{
    public static void SaveData<T>(this List<T> data)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText($"{typeof(T).Name}{nameof(Jfr)}.json", json);
    }

    public static List<T> LoadData<T>()
    {
        if (File.Exists($"{typeof(T).Name}{nameof(Jfr)}.json") == false)
            SaveData<T>([]);
        var json = File.ReadAllText($"{typeof(T).Name}{nameof(Jfr)}.json");
        return JsonConvert.DeserializeObject<List<T>>(json) ?? [];
    }
}