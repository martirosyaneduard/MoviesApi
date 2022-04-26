using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataTransferObject;
using MoviesApi.Models;
using Services.ServiceFolder;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IService<Actor, PersonDto> _service;
        private readonly ILogger<ActorController> _logger;

        public ActorController(IService<Actor, PersonDto> service, ILogger<ActorController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IAsyncEnumerable<Actor>))]
        public IActionResult GetAll()
        {
            var actor = _service.GetAll();
            return Ok(actor);
        }
        [HttpGet("{id}", Name = nameof(GetActorById))]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Director))]
        public async Task<IActionResult> GetActorById(int id)
        {
            Actor actor = await _service.GetById(id);
            return Ok(actor);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(PersonDto personDto)
        {
            Actor actor = await _service.Add(personDto);
            _logger.LogInformation("Added new actor");

            return CreatedAtAction(nameof(GetActorById), new { id = actor.Id }, actor);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, PersonDto personDto)
        {
            await _service.Update(personDto, id);
            _logger.LogInformation("Updated, {id}", id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            _logger.LogInformation("Deleted, {id}", id);
            return NoContent();
        }
    }
}
