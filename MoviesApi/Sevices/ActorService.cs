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
    class ActorService : IService<Actor, PersonDto>
    {
        private MovieContext _context;
        public ActorService(MovieContext context)
        {
            _context = context;
        }


        public async Task<Actor> Add(PersonDto entity)
        {
            var actor = new Actor()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Nationality = entity.Nationality,
                Birthday = entity.Birthday
            };

            _context?.Actors?.Add(actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        public async Task Delete(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor is null)
                throw new ArgumentException();

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }

        public async IAsyncEnumerable<Actor> GetAll()
        {
            var actors = _context.Actors
                .AsNoTracking()
                .Include(a=>a.Movies)
                .AsAsyncEnumerable();

            await foreach (var actor in actors)
            {
                yield return actor;
            }
        }

        public async Task<Actor> GetById(int id)
        {
            var actor = await _context.Actors
                .AsNoTracking()
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (actor is null)
                throw new ArgumentException();

            return actor;
        }

        public async Task Update(PersonDto entity, int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(g => g.Id == id);
            if(actor is null)
            {
                throw new ArgumentNullException();
            }

            actor.FirstName = entity.FirstName;
            actor.LastName = entity.LastName;
            actor.Nationality = entity.Nationality;
            actor.Birthday = entity.Birthday;

            await _context.SaveChangesAsync();
        }
    }
}
