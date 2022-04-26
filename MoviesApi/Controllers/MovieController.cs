using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataTransferObject;
using MoviesApi.Models;
using Services.ServiceFolder;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IService<Movie, MovieDto> _service;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IService<Movie, MovieDto> service, ILogger<MovieController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(IAsyncEnumerable<Movie>))]
        public IActionResult GetAll()
        {
            var movies = _service.GetAll();
            return Ok(movies);
        }
        [HttpGet("{id}", Name = nameof(GetMovieById))]
        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Movie))]
        public async Task<IActionResult> GetMovieById(int id)
        {
            Movie movie = await _service.GetById(id);
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(MovieDto movieDto)
        {
            Movie movie = await _service.Add(movieDto);
            _logger.LogInformation("Added new movie");

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, MovieDto movieDto)
        {
            await _service.Update(movieDto, id);
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
