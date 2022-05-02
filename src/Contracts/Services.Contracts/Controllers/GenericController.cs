using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Services.Contracts.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenericController<T> : ControllerBase, IGenericController<T> where T : IEntity
    {
        protected IEntityRepository<T> _repository;
        protected ILogger<GenericController<T>> _logger;

        public GenericController(IEntityRepository<T> repository, ILogger<GenericController<T>> logger)
        {
            _repository = (IEntityRepository<T>)repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<T>> CreateEntity([FromBody] T entity)
        {
            await _repository.CreateEntity(entity);
            return CreatedAtRoute("GetProduct", new { id = entity.Id }, entity);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteEntityById(string id)
        {
            return Ok(await _repository.DeleteEntity(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetEntities()
        {
            var entity = await _repository.GetEntities();
            return Ok(entity);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<T>> GetEntityById(string id)
        {
            var entity = await _repository.GetEntity(id);
            if (entity == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEntity([FromBody] T entity)
        {
            return Ok(await _repository.UpdateEntity(entity));
        }
    }
}
