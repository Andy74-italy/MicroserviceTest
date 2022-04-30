using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Services.Contracts.Data;

namespace Service1.API.Data
{
    public class DBContext : IDBContext
    {
        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        }

        public IMongoDatabase database { get; set; }

        public IMongoCollection<T> GetEntity<T>() where T : IEntity
        { 
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}
