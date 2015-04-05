using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edi.Service.Interfaces
{
    public interface IEntityService<T>
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}