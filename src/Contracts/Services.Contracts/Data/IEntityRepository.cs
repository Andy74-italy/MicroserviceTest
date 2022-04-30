using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts.Data
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetEntities();
        Task<T> GetEntity(string id);
        Task CreateEntity(T entity);
        Task<bool> UpdateEntity(T entity);
        Task<bool> DeleteEntity(string id);
    }
}
