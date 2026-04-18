namespace DefaultNamespace;

public interface IRepository
{
    // hent alle objekter
    List<t> GetAll();
    
    //hent et objekt via ID
    T? GetById(string id);
    
    //legg til et nytt objekt
    void Add(T item);
    
    //oppdater eksisterende objekt
    void Update(T item);
    
    //slett objekt via iD 
    void Delete(string id); 

}