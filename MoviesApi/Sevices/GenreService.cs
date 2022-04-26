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
    class GenreService :IService<Genre , GenreDto>
    {
        private MovieContext _context;
        public GenreService(MovieContext context)
        {
            _context = context;
        }


        public async Task<Genre> Add(GenreDto entity)
        {
            var genre = new Genre()
            {
                Name = entity.Name
            };

            _context?.Genres?.Add(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre is null)
                throw new ArgumentException();

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async IAsyncEnumerable<Genre> GetAll()
        {
            var genres = _context.Genres
                .AsNoTracking()
                .Include(g=>g.Movies)
                .AsAsyncEnumerable();

            await foreach (var genre in genres)
            {
                yield return genre;
            }
        }

        public async Task<Genre> GetById(int id)
        {
            var genre = await _context.Genres
                .AsNoTracking()
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre is null)
                throw new ArgumentException();

            return genre;
        }

        public async Task Update(GenreDto entity, int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if(genre is null)
            {
                throw new ArgumentNullException();
            }

            genre.Name = entity.Name;

            await _context.SaveChangesAsync();
        }
    }
}
