using MineSweeper.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Application.Interfaces
{
    public interface IGameAppService
    {
        Task<bool> CreateGame(GameViewModel game);

        Task<bool> VisitCell(Guid id, int row, int col);
    }
}
