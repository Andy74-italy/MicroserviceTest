using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service1.Entities;
using Service1.Repositories;
using Services.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Services.Contracts.Controllers;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Service1Controller : GenericController<Product>
    {
        public Service1Controller(IEntityRepository<Product> repository, ILogger<Service1Controller> logger)
            : base(repository, logger)
        {
            _repository = (ProductRepository)repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Category(string category)
        {
            var products = await ((ProductRepository)_repository).GetEntityByCategory(category);
            return Ok(products);
        }
    }
}
