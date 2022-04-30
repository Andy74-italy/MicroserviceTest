using MongoDB.Driver;

namespace Service1.Data
{
    public interface IDBContext<T>
    {
        IMongoCollection<T> GetEntity();
    }
}
