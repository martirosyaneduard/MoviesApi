using MoviesApi.Models;
using MoviesApi.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesApi.DataDB;
using Microsoft.EntityFrameworkCore;

namespace Services.ServiceFolder
{
    class DirectorService : IService<Director, PersonDto>
    {
        private MovieContext _context;
        public DirectorService(MovieContext context)
        {
            _context = context;
        }


        public async Task<Director> Add(PersonDto entity)
        {
            Director director = new Director()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Nationality = entity.Nationality,
                Birthday = entity.Birthday
            };

            _context?.Directors?.Add(director);
            await _context.SaveChangesAsync();

            return director;
        }

        public async Task Delete(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director is null)
                throw new ArgumentException();

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
        }

        public async IAsyncEnumerable<Director> GetAll()
        {
            var directors = _context.Directors
                .Include(a=>a.Movies)
                .AsNoTracking().AsAsyncEnumerable();

            await foreach (var director in directors)
            {
                yield return director;
            }
        }

        public async Task<Director> GetById(int id)
        {
            var director = await _context.Directors
                .Include(a => a.Movies)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (director is null)
                throw new ArgumentException();

            return director;
        }

        public async Task Update(PersonDto entity, int id)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(g => g.Id == id);
            if(director is null)
            {
                throw new ArgumentNullException();
            }

            director.FirstName = entity.FirstName;
            director.LastName = entity.LastName;
            director.Nationality = entity.Nationality;
            director.Birthday = entity.Birthday;

            await _context.SaveChangesAsync();
        }
    }
}
