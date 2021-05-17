using MineSweeper.Application.ViewModels;
using System.Threading.Tasks;

namespace MineSweeper.Application.Interfaces
{
    public interface IGameAppService
    {
        Task CreateGame(GameViewModel game);
    }
}
