using System.Collections.Generic;

namespace Edi.Service.Interfaces
{
    public interface IEntityService<T>
    {
        void Create(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
    }
}