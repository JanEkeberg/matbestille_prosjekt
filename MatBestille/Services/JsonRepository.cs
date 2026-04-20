using MatBestille.Interfaces;
using System.Text.Json;

namespace MatBestille.Services;

public class JsonRepository<T> : IRepository<T>
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public JsonRepository(string filePath)
    {
        _filePath = filePath;
        // Opprett tom fil hvis den ikke finnes
        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");
    }

    public List<T> GetAll()
    {
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json, _options)
               ?? new List<T>();
    }

    public T? GetById(string id)
    {
        return GetAll().FirstOrDefault(
            item => GetId(item) == id);
    }

    public void Add(T item)
    {
        var liste = GetAll();
        liste.Add(item);
        Lagre(liste);
    }

    public void Update(T item)
    {
        var liste = GetAll();
        int idx = liste.FindIndex(
            x => GetId(x) == GetId(item));
        if (idx >= 0) liste[idx] = item;
        Lagre(liste);
    }

    public void Delete(string id)
    {
        var liste = GetAll();
        liste.RemoveAll(x => GetId(x) == id);
        Lagre(liste);
    }

    private void Lagre(List<T> liste)
    {
        string json = JsonSerializer.Serialize(liste, _options);
        File.WriteAllText(_filePath, json);
    }

    // Henter ID-feltet via referanse 
    private string GetId(T item)
    {
        if (item == null) return "";

        // Prøv først å finne property som slutter på "Id"
        var idProperty = item.GetType()
            .GetProperties()
            .FirstOrDefault(p => p.Name.EndsWith("Id"));

        return idProperty?.GetValue(item)?.ToString() ?? "";
    }
}

