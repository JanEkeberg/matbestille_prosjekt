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

        // Sørger for at nye objekter ikke får samme ID som eksisterende objekter
        SikreUnikId(item, liste);

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

    // Sjekker om ID mangler eller allerede finnes, og lager ny ID hvis nødvendig
    private void SikreUnikId(T item, List<T> liste)
    {
        if (item == null) return;

        var idProperty = item.GetType()
            .GetProperties()
            .FirstOrDefault(p => p.Name.EndsWith("Id") && p.PropertyType == typeof(string));

        if (idProperty == null) return;

        string currentId = idProperty.GetValue(item)?.ToString() ?? "";

        bool manglerId = string.IsNullOrWhiteSpace(currentId);
        bool idFinnesFraFor = liste.Any(x => GetId(x) == currentId);

        if (!manglerId && !idFinnesFraFor)
            return;

        string prefix = FinnPrefix(idProperty.Name);

        int nesteNummer = liste
            .Select(GetId)
            .Where(id => id.StartsWith(prefix))
            .Select(id =>
            {
                string nummerDel = id.Substring(prefix.Length);
                return int.TryParse(nummerDel, out int nummer) ? nummer : 0;
            })
            .DefaultIfEmpty(0)
            .Max() + 1;

        string nyId = $"{prefix}{nesteNummer:D3}";

        var setter = idProperty.GetSetMethod(nonPublic: true);

        if (setter != null)
            setter.Invoke(item, new object[] { nyId });
    }

    // Velger riktig ID-prefix basert på property-navnet
    private string FinnPrefix(string idPropertyName)
    {
        return idPropertyName switch
        {
            "UserId" => "U",
            "ProductId" => "P",
            "OrderId" => "O",
            "InvoiceId" => "I",
            _ => "ID"
        };
    }
}