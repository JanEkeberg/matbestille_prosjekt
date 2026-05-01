using MatBestille.Interfaces;

namespace MatBestille.Tests.Helper;

public class FakeRepository<T> : IRepository<T>
{
    private readonly List<T> _items = new();

    // Returns all stored test objects from memory.
    public List<T> GetAll()
    {
        return _items;
    }

    // Finds one stored test object by its Id property.
    public T? GetById(string id)
    {
        return _items.FirstOrDefault(item => GetId(item) == id);
    }

    // Adds one test object to the in-memory list.
    public void Add(T item)
    {
        _items.Add(item);
    }

    // Replaces an existing test object with the same Id.
    public void Update(T item)
    {
        var id = GetId(item);
        var index = _items.FindIndex(existingItem => GetId(existingItem) == id);

        if (index >= 0)
        {
            _items[index] = item;
        }
    }

    // Removes one test object from memory by Id.
    public void Delete(string id)
    {
        _items.RemoveAll(item => GetId(item) == id);
    }

    // Reads the first property ending with Id from the object.
    private static string GetId(T item)
    {
        var idProperty = item!
            .GetType()
            .GetProperties()
            .FirstOrDefault(property => property.Name.EndsWith("Id"));

        return idProperty?.GetValue(item)?.ToString() ?? string.Empty;
    }
}
