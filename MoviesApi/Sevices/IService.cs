using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceFolder
{
    public interface IService<TEntity, TEntityDto>
    {
        IAsyncEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task Update(TEntityDto entity, int id);
        Task<TEntity> Add(TEntityDto entity);
        Task Delete(int id);
    }
}
