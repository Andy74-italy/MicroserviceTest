using MongoDB.Driver;
using Service1.Data;
using Service1.Entities;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service1.Repositories
{
    public class ProductRepository : IEntityRepository<Product>
    {
        private readonly IMongoCollection<Product> _contextProduct;

        public ProductRepository(IDBContext<Product> context)
        {
            _contextProduct = context.GetEntity() ?? throw new ArgumentNullException(nameof(context));
        }

        #region Interface implementation
        public async Task<IEnumerable<Product>> GetEntities()
        {
            return await _contextProduct
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<Product> GetEntity(string id)
        {
            return await _contextProduct
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task CreateEntity(Product product)
        {
            await _contextProduct.InsertOneAsync(product);
        }

        public async Task<bool> UpdateEntity(Product product)
        {
            var updateResult = await _contextProduct
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEntity(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _contextProduct
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        #endregion

        #region custom methods
        public async Task<IEnumerable<Product>> GetEntityByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _contextProduct
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetEntityByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _contextProduct
                            .Find(filter)
                            .ToListAsync();
        }
        #endregion
    }
}
