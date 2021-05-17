using MineSweeper.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(Guid id);

        Task<IEnumerable<T>> Get();

        Task<T> GetById(Guid id);
    }
}
