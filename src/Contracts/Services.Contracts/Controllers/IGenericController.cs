using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts.Controllers
{
    public interface IGenericController<T> where T : IEntity
    {
        Task<ActionResult<T>> CreateEntity([FromBody] T entity);
        Task<IActionResult> DeleteEntityById(string id);
        Task<ActionResult<T>> GetEntityById(string id);
        Task<ActionResult<IEnumerable<T>>> GetEntities();
        Task<IActionResult> UpdateEntity([FromBody] T entity);
    }
}