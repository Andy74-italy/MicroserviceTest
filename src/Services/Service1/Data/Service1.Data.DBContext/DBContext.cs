using MongoDB.Driver;

namespace Service1.Data
{
    public class DBContext<T> : IDBContext<T>
    {
        IMongoDatabase database;
        public DBContext(IDBConnectionInfo connectionInfo)
        {
            var client = new MongoClient(connectionInfo.ConnectionString);
            database = client.GetDatabase(connectionInfo.DatabaseName);
        }

        public IMongoCollection<T> GetEntity()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }

        // public IMongoCollection<Product> Products { get; }
    }
}
