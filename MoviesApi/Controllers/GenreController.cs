using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataTransferObject;
using MoviesApi.Models;
using Services.ServiceFolder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IService<Genre, GenreDto> _service;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IService<Genre, GenreDto> service, ILogger<GenreController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IAsyncEnumerable<Genre>))]
        public IActionResult GetAll()
        {
            var genres = _service.GetAll();
            return Ok(genres);
        }
        [HttpGet("{id}", Name = nameof(GetGenreById))]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Genre))]
        public async Task<IActionResult> GetGenreById(int id)
        {
            Genre genre = await _service.GetById(id);
            return Ok(genre);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(GenreDto genreDto)
        {
            Genre genre = await _service.Add(genreDto);
            _logger.LogInformation("Added new Customer");

            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, GenreDto genreDto)
        {
            await _service.Update(genreDto, id);
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
