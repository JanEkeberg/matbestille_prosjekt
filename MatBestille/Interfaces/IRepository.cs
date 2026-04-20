namespace MatBestille.Interfaces;

public interface IRepository<T>
{
    // hent alle objekter
    List<T> GetAll();
    
    //hent et objekt via ID
    T? GetById(string id);
    
    //legg til et nytt objekt
    void Add(T item);
    
    //oppdater eksisterende objekt
    void Update(T item);
    
    //slett objekt via iD 
    void Delete(string id); 

}