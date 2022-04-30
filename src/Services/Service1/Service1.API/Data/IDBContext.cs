using MongoDB.Driver;
using Services.Contracts.Data;

namespace Service1.API.Data
{
    public interface IDBContext
    {
        IMongoCollection<T> GetEntity<T>() where T : IEntity;
    }
}
