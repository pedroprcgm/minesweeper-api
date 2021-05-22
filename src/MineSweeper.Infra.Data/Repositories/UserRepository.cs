using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using MineSweeper.Infra.Repositories.Base;

namespace MineSweeper.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMineSweeperContext context) : base(context)
        {
        }
    }
}
