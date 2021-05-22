using MineSweeper.Application.ViewModels;
using System.Threading.Tasks;

namespace MineSweeper.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<bool> CreateUser(UserViewModel user);

    }
}
