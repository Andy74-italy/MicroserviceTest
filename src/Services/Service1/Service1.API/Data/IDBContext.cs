using MongoDB.Driver;
using Service1.API.Entities;

namespace Service1.API.Data
{
    public interface IDBContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
