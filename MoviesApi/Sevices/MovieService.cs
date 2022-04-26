using Microsoft.EntityFrameworkCore;
using MoviesApi.DataDB;
using MoviesApi.DataTransferObject;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceFolder
{
    class MovieService : IService<Movie, MovieDto>
    {
        private readonly MovieContext _context;

        public MovieService(MovieContext context)
        {
            this._context = context;
        }

        public async Task<Movie> Add(MovieDto entity)
        {

            var movie = await CreateMovie(entity);
            movie.Title = entity.Title;
            movie.Rating = entity.Rating;
            movie.RealesedYear = entity.RealesedYear;
            movie.Length = entity.Length;
            movie.DirectorId = entity.DirectorId;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task Delete(int id)
        {
            var movie = await GetById(id);
            _context.Movies.Remove(movie);  

            await _context.SaveChangesAsync();  
        }

        public async IAsyncEnumerable<Movie> GetAll()
        {
            var movies = _context.Movies
                .AsNoTracking()
                .Include(m => m.Actors)
                .Include(m => m.Genres)
                .AsAsyncEnumerable();

            await foreach(var movie in movies)
            {
                yield return movie;
            }

        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .Include(m => m.Actors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(x=>x.Id==id);
            if (movie is null)
            {
                throw new ArgumentException("Id is null");
            }

            return movie;
        }

        public async Task Update(MovieDto entity, int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(g => g.Id == id);

            if (movie is null)
            {
                throw new ArgumentNullException();
            }

            movie = await CreateMovie(entity); 
            movie.Title = entity.Title;
            movie.Length = entity.Length;
            movie.Rating = entity.Rating;
            movie.RealesedYear = entity.RealesedYear; 

            await _context.SaveChangesAsync();
        }



        private async Task<Movie> CreateMovie(MovieDto entity)
        {
            List<Genre> genres = new();

            foreach(var genreId in entity.GenresIds)
            {
                var genre = await _context.Genres.FindAsync(genreId);

                if (genre is null)
                    continue;

                genres.Add(genre);
            }

            List<Actor> actors = new();
            foreach (var actorId in entity.ActorsIds)
            {
                var actor = await _context.Actors.FindAsync(actorId);

                if (actor is null)
                    continue;

                actors.Add(actor);
            }

            Movie movie = new Movie();
            movie.Genres = genres;
            movie.Actors = actors;

            return movie;
        }
    }
}
