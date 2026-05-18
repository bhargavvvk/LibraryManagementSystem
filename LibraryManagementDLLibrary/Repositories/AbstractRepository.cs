using LibraryManagementDLLibrary.Context;
using LibraryManagementDLLibrary.Interfaces;

namespace LibraryManagementDLLibrary.Repositories;

public abstract class AbstractRepository<K,T>: IRepository<K,T> where K: notnull where T: class
{
    protected LibraryContext context;
    protected AbstractRepository( LibraryContext context)
    {
        this.context = context;
    }
    public virtual T Create(T item)
    {
        context.Add(item);
        context.SaveChanges();
        return item;
    }
    public abstract T? Get(K key);
    public virtual List<T>? GetAll()
    {
        return context.Set<T>().ToList();
    }
    public T? Update(K key, T item)
    {
        var myitem = Get(key);
        if (myitem == null)
            throw new Exception($"No {typeof(T).Name} with id {key}");
        context.Update(item);
        context.SaveChanges();
        return item;
    }
}
