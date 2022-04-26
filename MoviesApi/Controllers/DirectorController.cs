using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataTransferObject;
using MoviesApi.Models;
using Services.ServiceFolder;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IService<Director, PersonDto> _service;
        private readonly ILogger<DirectorController> _logger;

        public DirectorController(IService<Director, PersonDto> service, ILogger<DirectorController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IAsyncEnumerable<Director>))]
        public IActionResult GetAll()
        {
            var directors = _service.GetAll();
            return Ok(directors);
        }
        [HttpGet("{id}", Name = nameof(GetDirectorById))]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Director))]
        public async Task<IActionResult> GetDirectorById(int id)
        {
            Director director = await _service.GetById(id);
            return Ok(director);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(PersonDto personDto)
        {
            Director director = await _service.Add(personDto);
            _logger.LogInformation("Added new director");

            return CreatedAtAction(nameof(GetDirectorById), new { id = director.Id }, director);
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
