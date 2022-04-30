using MongoDB.Driver;
using Service1.API.Data;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service1.API.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : IEntity
    {
        private readonly IDBContext _context;

        public EntityRepository(IDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<T>> GetEntities()
        {
            return await _context
                            .GetEntity<T>()
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<T> GetEntity(object id)
        {
            return await _context
                           .GetEntity<T>()
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task CreateEntity(T product)
        {
            await _context.GetEntity<T>().InsertOneAsync(product);
        }

        public async Task<bool> UpdateEntity(T product)
        {
            var updateResult = await _context
                                        .GetEntity<T>()
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .GetEntity<T>()
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
