using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service1.Entities;
using Service1.Repositories;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Service1Controller : ControllerBase, IGenericController<Product>
    {
        private readonly ProductRepository _repository;
        private readonly ILogger<Service1Controller> _logger;

        public Service1Controller(IEntityRepository<Product> repository, ILogger<Service1Controller> logger)
        {
            _repository = (ProductRepository)repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetEntities()
        {
            var products = await _repository.GetEntities();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetEntityById(string id)
        {
            var product = await _repository.GetEntity(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Category(string category)
        {
            var products = await _repository.GetEntityByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateEntity([FromBody] Product product)
        {
            await _repository.CreateEntity(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEntity([FromBody] Product product)
        {
            return Ok(await _repository.UpdateEntity(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEntityById(string id)
        {
            return Ok(await _repository.DeleteEntity(id));
        }
    }
}
