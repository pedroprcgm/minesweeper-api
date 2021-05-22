using MineSweeper.Domain.Entities;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
