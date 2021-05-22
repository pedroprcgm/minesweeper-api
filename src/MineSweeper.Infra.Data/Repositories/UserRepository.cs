using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using MineSweeper.Infra.Repositories.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MineSweeper.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMineSweeperContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await DbSet.FindAsync(Builders<User>.Filter.Eq("Email", email));
            return user.SingleOrDefault();
        }
    }
}
