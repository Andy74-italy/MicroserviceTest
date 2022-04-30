using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service1.API.Repositories;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service1.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Service1Controller<T> : ControllerBase where T : IEntity
    {
        private readonly EntityRepository<T> _repository;
        private readonly ILogger<Service1Controller<T>> _logger;

        public Service1Controller(EntityRepository<T> repository, ILogger<Service1Controller<T>> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<T>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<T>>> GetProducts()
        {
            var products = await _repository.GetEntities();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(T), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<T>> GetProductById(string id)
        {
            var product = await _repository.GetEntity(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(T), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<T>> CreateProduct([FromBody] T product)
        {
            await _repository.CreateEntity(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        //[ProducesResponseType(typeof(T), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] T product)
        {
            return Ok(await _repository.UpdateEntity(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        //[ProducesResponseType(typeof(T), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteEntity(id));
        }
    }
}
