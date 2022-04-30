using Services.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service1.API.Repositories
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task CreateEntity(T product);
        Task<bool> DeleteEntity(object id);
        Task<IEnumerable<T>> GetEntities();
        Task<T> GetEntity(object id);
        Task<bool> UpdateEntity(T product);
    }
}