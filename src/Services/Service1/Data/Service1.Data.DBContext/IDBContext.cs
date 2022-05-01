using MongoDB.Driver;

namespace Service1.Data
{
    public interface IDBConnectionInfo
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
    public interface IDBContext<T>
    {
        IMongoCollection<T> GetEntity();
    }
}
