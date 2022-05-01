using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Service1.API.Entities;

namespace Service1.API.Data
{
    public class DBContext : IDBContext
    {
        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        }

        public IMongoCollection<Product> Products { get; }
    }
}
