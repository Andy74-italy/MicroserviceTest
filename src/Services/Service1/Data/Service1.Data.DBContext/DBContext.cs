using MongoDB.Driver;

namespace Service1.Data
{
    public class DBContext<T> : IDBContext<T>
    {
        IMongoDatabase database;
        public DBContext(string ConnectionString, string DatabaseName)
        {
            var client = new MongoClient(ConnectionString);
            database = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> GetEntity()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }

        // public IMongoCollection<Product> Products { get; }
    }
}
