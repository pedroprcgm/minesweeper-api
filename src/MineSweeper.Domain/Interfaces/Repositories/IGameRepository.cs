using MineSweeper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetByUserId(Guid userId);
    }
}
