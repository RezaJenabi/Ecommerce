using System;
using System.Threading.Tasks;
using Infrastructure.Domain.BaseEntities;
using Infrastructure.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public abstract class ApiController<T> : Controller where T : BaseEntity, IAggregateRoot
    {
        private IRepository<T> _repository;

        public ApiController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Query()
        {
            return Ok(_repository.Table);
        }

        [HttpGet("{id}")]
        public IActionResult Find(Guid id)
        {
            var record = _repository.GetById(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T record)
        {
            await _repository.AddAsync(record);

            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] T record)
        {
            if (id != record.Id)
                return BadRequest();

            await _repository.UpdateAsync(record);
            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(_repository.GetById(id));
            return NoContent();
        }
    }
}
