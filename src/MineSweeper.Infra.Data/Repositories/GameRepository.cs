using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using MineSweeper.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineSweeper.Infra.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(IMineSweeperContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Game>> GetByUserId(Guid userId)
        {
            return (await DbSet.FindAsync(Builders<Game>.Filter.Eq(f => f.UserId, userId))).ToEnumerable();
        }
    }
}
