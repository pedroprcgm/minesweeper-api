using System;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Facades
{
    public interface IAuthFacade
    {
        string GenerateToken(Guid userId, string userName);

        Task<object> GetLoggedUser();
    }
}
